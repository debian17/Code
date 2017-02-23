package com.example.ndktest;

import android.content.pm.ActivityInfo;
import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.os.Handler;
import android.os.HandlerThread;
import android.os.Looper;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.SurfaceView;
import android.view.WindowManager;
import android.widget.TextView;
import org.opencv.android.CameraBridgeViewBase;
import org.opencv.android.CameraGLSurfaceView;
import org.opencv.android.OpenCVLoader;
import org.opencv.android.Utils;
import org.opencv.core.DMatch;
import org.opencv.core.KeyPoint;
import org.opencv.core.Mat;
import org.opencv.core.MatOfDMatch;
import org.opencv.core.MatOfKeyPoint;
import org.opencv.core.Scalar;
import org.opencv.features2d.DescriptorExtractor;
import org.opencv.features2d.DescriptorMatcher;
import org.opencv.features2d.FeatureDetector;
import org.opencv.features2d.Features2d;
import org.opencv.imgproc.Imgproc;
import java.util.LinkedList;
import java.util.List;

public class MainActivity extends AppCompatActivity implements CameraBridgeViewBase.CvCameraViewListener2 {
    // Used to load the 'native-lib' library on application startup.
    static {
        System.loadLibrary("native-lib");
        //System.loadLibrary("nonfree");
        System.loadLibrary("opencv_java3");
    }

    private String LOG = "MY_LOG INFO";

    private FeatureDetector featureDetector;
    private CameraBridgeViewBase mOpenCvCameraView;
    private CameraGLSurfaceView cameraGLSurfaceView;
    private TextView textView;
    private Handler handler;
    private int i = 0;
    private Mat Mark;
    private Mat outputImage;
    private MatOfKeyPoint SceneKeyPoints;
    private MatOfKeyPoint MarkKeyPoints;
    private Scalar keypointColor;
    private Mat CurFrame;
    private MatOfKeyPoint MarkDescriptor;
    private MatOfKeyPoint SceneDescriptor;
    private DescriptorExtractor descriptorExtractor;
    private HandlerThread mBackgroundThread;
    private Handler mBackgroundHandler;
    private TaskAsync taskAsync;
    private Params params;
    private DescriptorMatcher descriptorMatcher;
    private List<MatOfDMatch> matches;
    private float nndrRatio = 0.5f;
    private LinkedList<DMatch> goodMatchesList;
    private Bitmap tempbitmap;
    private Drawable tempdrawable;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
        getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
        setContentView(R.layout.activity_main);
        setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE);
        mOpenCvCameraView = (CameraBridgeViewBase) findViewById(R.id.view);
        mOpenCvCameraView.setVisibility(SurfaceView.VISIBLE);
        mOpenCvCameraView.setCvCameraViewListener(this);
        mOpenCvCameraView.setMaxFrameSize(1200,1600);
        //mOpenCvCameraView.enableFpsMeter();

        handler = new Handler(Looper.getMainLooper());

        featureDetector = FeatureDetector.create(FeatureDetector.ORB);
        descriptorExtractor = DescriptorExtractor.create(DescriptorExtractor.ORB);
        descriptorMatcher = DescriptorMatcher.create(DescriptorMatcher.BRUTEFORCE_HAMMING);

        CurFrame = new Mat();
        MarkDescriptor = new MatOfKeyPoint();
        SceneDescriptor = new MatOfKeyPoint();
        keypointColor = new Scalar(255, 0, 0);
        SceneKeyPoints = new MatOfKeyPoint();
        MarkKeyPoints = new MatOfKeyPoint();


        //Mark = Imgcodecs.imread("book.jpg");
        Mark = new Mat();
        tempdrawable = getResources().getDrawable(R.drawable.test);
        tempbitmap = ((BitmapDrawable) tempdrawable).getBitmap();
        Utils.bitmapToMat(tempbitmap, Mark);

        if(Mark.empty()){
            Log.d("MARK_ONFO", "BAD NEWS MATHERFUCKA");
            finish();
        }
        else{
            Log.d("MARK_ONFO", "NOT EMPTY");
        }

        //int res = getResources().getIdentifier("book.jpg", "drawable", this.getPackageName());
        //Log.d("RESULT = ", Integer.toString(res));

        matches = new LinkedList<MatOfDMatch>();
        goodMatchesList = new LinkedList<DMatch>();
        //DMatch[] dmatcharray = new DMatch[];

        //params = new Params();

        //верно
        featureDetector.detect(Mark, MarkKeyPoints);

        if(MarkKeyPoints.empty()){
            Log.d("MARKKEYPOINTS", "BAD NEWS MATHERFUCKA");
            finish();
        }

        Log.d("FD_BOOK", Integer.toString(MarkKeyPoints.toArray().length));

        descriptorExtractor.compute(Mark, MarkKeyPoints, MarkDescriptor);
        if(MarkDescriptor.empty()){
            Log.d("MARKDESCRIPTOR", "BAD NEWS MATHERFUCKA");
            finish();
        }

        Log.d("DE_BOOK", Long.toString(MarkDescriptor.elemSize()));

        // Create the matrix for output image.
        //outputImage = new Mat(Mark.rows(), Mark.cols(), Imgproc.COLOR_RGBA2RGB);
    }

    private Runnable DoThings = new Runnable()
    {
        public void run()
        {
        }
    };

    protected void startBackgroundThread() {
        mBackgroundThread = new HandlerThread("Camera Background");
        mBackgroundThread.start();
        mBackgroundHandler = new Handler(mBackgroundThread.getLooper());
    }

    protected void stopBackgroundThread() {
        mBackgroundThread.quitSafely();
        try {
            mBackgroundThread.join();
            mBackgroundThread = null;
            mBackgroundHandler = null;
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onPause()
    {
        //stopBackgroundThread();
        super.onPause();
        if (mOpenCvCameraView != null){
            mOpenCvCameraView.disableView();
        }
    }

    @Override
    public void onResume()
    {
        super.onResume();
        OpenCVLoader.initDebug();
        mOpenCvCameraView.enableView();
        //startBackgroundThread();
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        if (mOpenCvCameraView != null){
            mOpenCvCameraView.disableView();
        }
    }

    @Override
    public void onCameraViewStarted(int width, int height) {
    }

    @Override
    public void onCameraViewStopped() {
    }

    @Override
    public Mat onCameraFrame(CameraBridgeViewBase.CvCameraViewFrame inputFrame) {
        //CurFrame = inputFrame.rgba();

        //Runnable r = new JThread(featureDetector,inputFrame.rgba(), SceneKeyPoints);
        //new Thread(r).start();

        /*params.featureDetector = featureDetector;
        params.inputFrame = inputFrame.rgba();
        params.SceneKeyPoints = SceneKeyPoints;
        taskAsync = new TaskAsync();
        taskAsync.execute(params);
        //taskAsync.execute(params);*/

        //SceneKeyPoints.release();
        //верно
        featureDetector.detect(inputFrame.gray(), SceneKeyPoints);
        //detect_keyPoints = SceneKeyPoints.toArray();
        //Log.d("FD", Integer.toString(SceneKeyPoints.toArray().length));

        //SceneDescriptor.release();
        descriptorExtractor.compute(inputFrame.gray(), SceneKeyPoints, SceneDescriptor);
        //Log.d("DE", Long.toString(SceneDescriptor.elemSize()));

        //handler.post(r);
        //Imgproc.cvtColor(CurFrame, CurFrame, Imgproc.COLOR_RGBA2RGB);

        //matches.clear();
        //descriptorMatcher.knnMatch(MarkDescriptor, matches, 2);
        descriptorMatcher.knnMatch(MarkDescriptor, SceneDescriptor, matches, 2);
        //descriptorMatcher.knnMatch(Mat,Mat,List<MatOfDMatch>,int,);

        //Log.d("DM", Integer.toString(matches.size()));

        for (int i = 0; i < matches.size(); i++) {
            MatOfDMatch matofDMatch = matches.get(i);
            DMatch[] dmatcharray = matofDMatch.toArray();
            DMatch m1 = dmatcharray[0];
            DMatch m2 = dmatcharray[1];

            //Log.d(LOG, "PROCESSING POINT");

            if (m1.distance <= m2.distance * nndrRatio) {
                goodMatchesList.addLast(m1);
                //Log.d(LOG, "GOOD POINT++");
            }
        }

        if(goodMatchesList.size() >= 3){
            Log.d("GOOD NEWS", "OBJECT FOUND!");
        }

        //Features2d.drawKeypoints(CurFrame, SceneKeyPoints, CurFrame, keypointColor, 0);

        return inputFrame.rgba();
    }
}
