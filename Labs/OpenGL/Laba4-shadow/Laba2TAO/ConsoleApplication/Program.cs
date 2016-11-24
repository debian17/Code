using Tao.FreeGlut;
using Tao.OpenGl;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

namespace ConsoleApplication
{
    internal class Program
    {
        public static double eye_x, eye_y, eye_z, scale, aX, aY, aZ, angle;
        public static int[] texture = new int[7];
        public static float[] light = new float[4] { 10F, 10F, 1, 1 };
        public static float[] fshadowMatrix = new float[16];
        public static float[] plane = { 0, 1, 0, 0 };
        public static float[] depthLight;
        public static float[] depthView;
        public static Glu.GLUquadric quadratic;

        public static Bitmap LoadPicture(string filename)
        {
            return new Bitmap(filename);
        }

        public static void SetShadowMatrix(float[] fDestMat, float[] fLightPos, float[] fPlane)
        {
            float dot;

            // dot product of plane and light position
            dot = fPlane[0] * fLightPos[0] +
                    fPlane[1] * fLightPos[1] +
                    fPlane[1] * fLightPos[2] +
                    fPlane[3] * fLightPos[3];

            // first column
            fDestMat[0] = dot - fLightPos[0] * fPlane[0];
            fDestMat[4] = 0.0f - fLightPos[0] * fPlane[1];
            fDestMat[8] = 0.0f - fLightPos[0] * fPlane[2];
            fDestMat[12] = 0.0f - fLightPos[0] * fPlane[3];

            // second column
            fDestMat[1] = 0.0f - fLightPos[1] * fPlane[0];
            fDestMat[5] = dot - fLightPos[1] * fPlane[1];
            fDestMat[9] = 0.0f - fLightPos[1] * fPlane[2];
            fDestMat[13] = 0.0f - fLightPos[1] * fPlane[3];

            // third column
            fDestMat[2] = 0.0f - fLightPos[2] * fPlane[0];
            fDestMat[6] = 0.0f - fLightPos[2] * fPlane[1];
            fDestMat[10] = dot - fLightPos[2] * fPlane[2];
            fDestMat[14] = 0.0f - fLightPos[2] * fPlane[3];

            // fourth column
            fDestMat[3] = 0.0f - fLightPos[3] * fPlane[0];
            fDestMat[7] = 0.0f - fLightPos[3] * fPlane[1];
            fDestMat[11] = 0.0f - fLightPos[3] * fPlane[2];
            fDestMat[15] = dot - fLightPos[3] * fPlane[3];
        }

        public static void Shadow()
        {
            double[] modelviewMatrix = new double[16];
            double[] projectionMatrix = new double[16];
            int[] viewport = new int[4];
            double objX, objY, objZ;
            float depth;
            float[] p = light;
            float[] localDepthView, localDepthLight;
            double[] modelviewLight = new double[16];
            double winX, winY, winZ;
            int ix, iy;
            double depth_2;
            int x, y;

            // color of pixels in shadow
            int[] pixel = { 0x7f7f7f7f };
            Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);

            // get the modelview, project, and viewport
            Gl.glGetDoublev(Gl.GL_MODELVIEW_MATRIX, modelviewMatrix);
            Gl.glGetDoublev(Gl.GL_PROJECTION_MATRIX, projectionMatrix);
            Gl.glGetIntegerv(Gl.GL_VIEWPORT, viewport);

            // get the transformation from light view
            Gl.glPushMatrix();
            Gl.glLoadIdentity();
            Glu.gluLookAt(p[0], p[1], p[2], 0.0, 0.0, 0.0, 0.0, 1.0, 0.0);
            Gl.glGetDoublev(Gl.GL_MODELVIEW_MATRIX, modelviewLight);
            Gl.glPopMatrix();

            // set the project matrix to orthographic
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glPushMatrix();
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(0.0, (float)1000, 0.0, (float)1000);

            // set the modelview matrix to identity
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glPushMatrix();
            Gl.glLoadIdentity();

            // get the current depth buffer
            Gl.glReadPixels(0, 0, 1000, 1000, Gl.GL_DEPTH_COMPONENT, Gl.GL_FLOAT, depthView);

            // get pointers to the depth buffers
            localDepthView = depthView;
            localDepthLight = depthLight;

            int i = 0;
            // go through every pixel in frame buffer
            for (y = 0; y < 1000; y++)
            {
                for (x = 0; x < 1000; x++)
                {
                    // depth at pixel
                    depth = localDepthView[i++];

                    // on the far plane of frustum - don't calculate
                    if (depth > 0.99)
                    {
                        continue;
                    }

                    // get world coordinate from x, y, depth
                    Glu.gluUnProject(x, y, (double)depth, modelviewMatrix, projectionMatrix, viewport, out objX, out objY, out objZ);

                    // get light view screen coordinate and depth
                    Glu.gluProject(objX, objY, objZ, modelviewLight, projectionMatrix, viewport, out winX, out winY, out winZ);

                    ix = (int)(winX + 0.5);
                    iy = (int)(winY + 0.5);

                    // make sure within the screen
                    if (ix >= 1000 || iy >= 1000 || ix < 0 || iy < 0)
                    {
                        continue;
                    }

                    // get the depth value from the light
                    depth_2 = (double)depthLight[iy * 1000 + ix];

                    // is something between the light and the pixel?
                    if ((winZ - depth_2) > 0.01)
                    {
                        Gl.glRasterPos2i(x, y);
                        Gl.glDrawPixels(1, 1, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, pixel);
                    }
                }
            }

            // restore modelview transformation
            Gl.glPopMatrix();

            // restore projection
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glPopMatrix();

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
        }

        static void LoadTexture()
        {
            Bitmap[] textureImg = new Bitmap[7];
            textureImg[0] = LoadPicture("s.bmp");
            textureImg[1] = LoadPicture("hand.bmp");
            textureImg[2] = LoadPicture("heat.bmp");
            textureImg[3] = LoadPicture("eye.bmp");
            textureImg[4] = LoadPicture("nose.bmp");
            textureImg[5] = LoadPicture("rot.bmp");
            textureImg[6] = LoadPicture("w.bmp");

            Gl.glGenTextures(7, texture);
            textureImg[0].RotateFlip(RotateFlipType.RotateNoneFlipY);

            Rectangle rectangle = new Rectangle(0, 0, textureImg[0].Width, textureImg[0].Height);
            BitmapData bitmapData = textureImg[0].LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[0]);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_NEAREST);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB8, textureImg[0].Width, textureImg[0].Height, 0, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, bitmapData.Scan0);
            textureImg[0].UnlockBits(bitmapData);
            textureImg[0].Dispose();

            rectangle = new Rectangle(0, 0, textureImg[1].Width, textureImg[1].Height);
            bitmapData = textureImg[1].LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[1]);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_NEAREST);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB8, textureImg[1].Width, textureImg[1].Height, 0, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, bitmapData.Scan0);
            textureImg[1].UnlockBits(bitmapData);
            textureImg[1].Dispose();

            rectangle = new Rectangle(0, 0, textureImg[2].Width, textureImg[2].Height);
            bitmapData = textureImg[2].LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[2]);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_NEAREST);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB8, textureImg[2].Width, textureImg[2].Height, 0, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, bitmapData.Scan0);
            textureImg[2].UnlockBits(bitmapData);
            textureImg[2].Dispose();

            rectangle = new Rectangle(0, 0, textureImg[3].Width, textureImg[3].Height);
            bitmapData = textureImg[3].LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[3]);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_NEAREST);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB8, textureImg[3].Width, textureImg[3].Height, 0, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, bitmapData.Scan0);
            textureImg[3].UnlockBits(bitmapData);
            textureImg[3].Dispose();

            rectangle = new Rectangle(0, 0, textureImg[4].Width, textureImg[4].Height);
            bitmapData = textureImg[4].LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[4]);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_NEAREST);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB8, textureImg[4].Width, textureImg[4].Height, 0, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, bitmapData.Scan0);
            textureImg[4].UnlockBits(bitmapData);
            textureImg[4].Dispose();

            rectangle = new Rectangle(0, 0, textureImg[5].Width, textureImg[5].Height);
            bitmapData = textureImg[5].LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[5]);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_NEAREST);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB8, textureImg[5].Width, textureImg[5].Height, 0, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, bitmapData.Scan0);
            textureImg[5].UnlockBits(bitmapData);
            textureImg[5].Dispose();

            rectangle = new Rectangle(0, 0, textureImg[6].Width, textureImg[6].Height);
            bitmapData = textureImg[6].LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[6]);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_NEAREST);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB8, textureImg[6].Width, textureImg[6].Height, 0, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, bitmapData.Scan0);
            textureImg[6].UnlockBits(bitmapData);
            textureImg[6].Dispose();

        }

        static void Init_graphics()
            {
            LoadTexture();
            
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glShadeModel(Gl.GL_SMOOTH);
            Gl.glFrontFace(Gl.GL_CCW);
            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
            Gl.glLineWidth(2.0f);
            Gl.glLightModeli(Gl.GL_FRONT, Gl.GL_TRUE);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light);
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glClearColor(1, 1, 1, 1);

            quadratic = Glu.gluNewQuadric();
            Glu.gluQuadricNormals(quadratic, Glu.GLU_SMOOTH);
            Glu.gluQuadricTexture(quadratic, Gl.GL_TRUE);

            eye_x = 3;
            eye_y = 10;
            eye_z = 0;
            scale = 0.1;
            angle = 2;
            
            SetShadowMatrix(fshadowMatrix,light,plane);
            }

        static void OnDisplay()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();
            Glu.gluLookAt(eye_x, eye_y, eye_z, 0, -10, 0, 1, 0, 0);

            //Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[6]);
            Gl.glRotated(90, 0, 1, 0);
            Gl.glTranslated(0, 0, -1);
            Glu.gluDisk(quadratic, 0, 20, 50, 50);
            Gl.glRotated(-90, 0, 1, 0);

            Gl.glPushMatrix();
            Gl.glRotated(aX, 1, 0, 0);
            Gl.glRotated(aY, 0, 1, 0);
            Gl.glRotated(aZ, 0, 0, 1);
            Gl.glTranslated(2.5, 0, 0);

            //основание
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[0]);
            Gl.glTranslated(-1.5, 0, 0);
            Glu.gluSphere(quadratic, 1.1, 50, 50);
            //Gl.glPopMatrix();
            //Glut.glutSolidSphere(1.1, 50, 50);

            //тело
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[0]);
            Gl.glTranslated(1.6, 0, 0);
            Glu.gluSphere(quadratic, 0.75, 50, 50);
            //Glut.glutSolidSphere(0.75, 50, 50);

            //голова
            Gl.glTranslated(1.1, 0, 0);
            Glu.gluSphere(quadratic, 0.55, 50, 50);
            //Glut.glutSolidSphere(0.55, 50, 50);

            //правая рука
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[1]);
            Gl.glTranslated(-1, 0, 0);
            Gl.glRotated(20, 0, 1, 0);
            Glu.gluCylinder(quadratic, 0.05, 0.05, 2.5, 50, 50);
            //Glut.glutSolidCylinder(0.05, 2.5, 50, 50);

            //левая рука
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[1]);
            Gl.glRotated(-140, 0, -1, 0);
            Glu.gluCylinder(quadratic, 0.05, 0.05, 2.5, 50, 50);
            //Glut.glutSolidCylinder(0.05, 2.5, 50, 50);

            //шляпа
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[2]);
            Gl.glTranslated(-1.3, 0, 0.47);
            Gl.glRotated(290, 0, 1, 0);
            Glu.gluCylinder(quadratic, 0.5, 0, 0.75, 50, 50);
            //Glut.glutSolidCone(0.5, 0.75, 50, 50);

            //нос
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[4]);
            Gl.glTranslated(0, 0.3, -0.4);
            Gl.glRotated(275, 1, 0, 0);
            Glu.gluCylinder(quadratic, 0.07,0,0.75, 50, 50);
            //Glut.glutSolidCone(0.07, 0.75, 50, 50);

            //правый глаз
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[3]);
            Gl.glTranslated(-0.2, -0.25, 0.15);
            Glu.gluSphere(quadratic,0.05, 50, 50);
            //Glut.glutSolidSphere(0.05, 50, 50);

            //левый глаз
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[3]);
            Gl.glTranslated(0.4, 0, 0);
            Glu.gluSphere(quadratic, 0.05, 50, 50);
            //Glut.glutSolidSphere(0.05, 50, 50);

            //рот
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[1]);
            Gl.glTranslated(-0.47, 0.4, 0.0175);
            Gl.glRotated(90, 0, 1, 0);
            Glu.gluCylinder(quadratic, 0.05,0.05, 0.5, 50, 50);
            //Glut.glutSolidCylinder(0.05, 0.5, 50, 50);

            //правый диск
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[1]);
            Glu.gluDisk(quadratic, 0, 0.05, 50, 50);

            //левый диск
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[1]);
            Gl.glTranslated(0, 0, 0.5);
            Glu.gluDisk(quadratic, 0, 0.05, 50, 50);

            ////диск основания
            //Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[6]);
            //Gl.glTranslated(3, 4, 0);
            //Gl.glRotated(90, 1, 0, 0);
            //Glu.gluDisk(quadratic, 0, 10, 50, 50);


            //Thread t = new Thread(Shadow);
            //t.Start();
            Shadow();
            Gl.glPopMatrix();
            Gl.glMultMatrixf(fshadowMatrix);
            Gl.glFlush();
            Glut.glutSwapBuffers();
        }

        static void OnReshape(int w, int h)
        {
                Gl.glMatrixMode(Gl.GL_PROJECTION);
                Gl.glLoadIdentity();
                Gl.glViewport(0, 0, w, h);
                Glu.gluPerspective(40, w / h, 1, 100);
                Gl.glMatrixMode(Gl.GL_MODELVIEW);
                depthLight = new float[1000 * 1000];
                depthView = new float[1000 * 1000];
        }

        static void UpdateKeys(byte key, int x, int y)
        {
            if (key == 'e')
            {
                aY -= angle;
            }

            if (key == 'q')
            {
                aY +=angle;
            }

            if (key == 'w')
            {
                aZ += angle;
            }

            if (key == 's')
            {
                aZ -= angle;
            }

            if (key == 'd')
            {
                aX -= angle;
            }

            if (key == 'a')
            {
                aX += angle;
            }

            if (key == 'n')
            {
                eye_y += scale;
            }

            if (key == 'j')
            {
                eye_y -= scale;
            }

        }

        static void Update(int val)
        {
            OnDisplay();
            Glut.glutTimerFunc(20, Update, 1);
        }

        public static void Main()
        {
            Glut.glutInit();
            Glut.glutInitWindowSize(1000, 1000);
            Glut.glutCreateWindow("Snow_Man");
            Init_graphics();
            Glut.glutDisplayFunc(OnDisplay);
            Glut.glutReshapeFunc(OnReshape);
            Glut.glutTimerFunc(20, Update, 1);
            Glut.glutKeyboardFunc(UpdateKeys);
            Glut.glutMainLoop();
        }
    }
}


