package com.example.ndktest;


import android.os.AsyncTask;

import org.opencv.core.MatOfKeyPoint;

public class TaskAsync extends AsyncTask<Params, Void, MatOfKeyPoint> {

    @Override
    protected void onPreExecute() {
        super.onPreExecute();
    }

    @Override
    protected MatOfKeyPoint doInBackground(Params... params) {
        params[0].featureDetector.detect(params[0].inputFrame, params[0].SceneKeyPoints);
        return params[0].SceneKeyPoints;
    }

    @Override
    protected void onPostExecute(MatOfKeyPoint result) {
        super.onPostExecute(result);
    }
}
