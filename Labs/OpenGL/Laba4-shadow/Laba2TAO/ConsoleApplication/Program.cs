using Tao.FreeGlut;
using Tao.OpenGl;
using System.Drawing;
using System.Drawing.Imaging;

namespace ConsoleApplication
{
    internal class Program
    {
        private static double eye_x, eye_y, eye_z, scale, aX, aY, aZ, angle;
        public static int[] texture = new int[6];
        static float[] light = new float[3] { 1, 0.5F, 1 };
        // depth buffer
        private static float[] depthLight;
        private static float[] depthView;

        static Glu.GLUquadric quadratic;

        private static void Shadows()
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
            Glu.gluLookAt(p[0], p[1], p[2], 0.0, 0.0, 0.0, 1.0, 0.0, 0.0);
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
            localDepthView = new float[1000 * 1000];
            localDepthLight = new float[1000 * 1000];
            
            localDepthView = depthView;
            localDepthLight = depthLight;
            //System.Console.WriteLine(localDepthView.Length);
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


        public static Bitmap LoadPicture(string filename)
        {
            return new Bitmap(filename);
        }

        static void LoadTexture()
        {
            Bitmap[] textureImg = new Bitmap[6];
            textureImg[0] = LoadPicture("s.bmp");
            textureImg[1] = LoadPicture("hand.bmp");
            textureImg[2] = LoadPicture("heat.bmp");
            textureImg[3] = LoadPicture("eye.bmp");
            textureImg[4] = LoadPicture("nose.bmp");
            textureImg[5] = LoadPicture("rot.bmp");
            Gl.glGenTextures(6, texture);
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

        }

        static void Init_graphics()
            {
            //LoadTexture();
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glShadeModel(Gl.GL_SMOOTH);
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
            }

        static void Render()
        {

            Gl.glRotated(90, 0, 1, 0);
            Glu.gluDisk(quadratic, 0, 15, 50, 50);
            Gl.glRotated(-90, 0, 1, 0);
            Gl.glTranslated(3, 0, 0);
            //основание
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[0]);
            Gl.glTranslated(-1.5, 0, 0);
            Glu.gluSphere(quadratic, 1.1, 50, 50);
            //Glut.glutSolidSphere(1.1, 50, 50);

            //тело
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[0]);
            Gl.glPushMatrix();
            Gl.glTranslated(1.6, 0, 0);
            Glu.gluSphere(quadratic, 0.75, 50, 50);
            Gl.glPopMatrix();
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
            Glu.gluCylinder(quadratic, 0.07, 0, 0.75, 50, 50);
            //Glut.glutSolidCone(0.07, 0.75, 50, 50);

            //правый глаз
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[3]);
            Gl.glTranslated(-0.2, -0.25, 0.15);
            Glu.gluSphere(quadratic, 0.05, 50, 50);
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
            Glu.gluCylinder(quadratic, 0.05, 0.05, 0.5, 50, 50);
            //Glut.glutSolidCylinder(0.05, 0.5, 50, 50);

            //правый диск
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[1]);
            Glu.gluDisk(quadratic, 0, 0.05, 50, 50);

            //левый диск
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[1]);
            Gl.glTranslated(0, 0, 0.5);
            Glu.gluDisk(quadratic, 0, 0.05, 50, 50);

        }

        static void OnDisplay()
        {
            
            int[] buffer = new int[1];
            float[] p = light;
            // get the current color buffer being drawn to
            Gl.glGetIntegerv(Gl.GL_DRAW_BUFFER, buffer);
            
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glDrawBuffer(Gl.GL_NONE);
            Gl.glLoadIdentity();
            Glu.gluLookAt(eye_x, eye_y, eye_z, 0, -10, 0, 1, 0, 0);
            Gl.glRotated(aX, 1, 0, 0);
            Gl.glRotated(aY, 0, 1, 0);
            Gl.glRotated(aZ, 0, 0, 1);
            Glu.gluLookAt(p[0], p[1], p[2], 0.0, 0.0, 0.0, 1.0, 0.0, 0.0);
            Render();

            
            // save the depth buffer
            Gl.glReadPixels(0, 0, 1000, 1000, Gl.GL_DEPTH_COMPONENT, Gl.GL_FLOAT, depthLight);
            Gl.glDrawBuffer(buffer[0]);
            Gl.glClear(Gl.GL_DEPTH_BUFFER_BIT);
            Render();
            Shadows();
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


