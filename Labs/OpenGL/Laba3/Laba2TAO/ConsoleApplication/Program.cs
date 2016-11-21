﻿using Tao.FreeGlut;
using Tao.OpenGl;
using System.Drawing;
using System.Drawing.Imaging;

namespace ConsoleApplication
{
    internal class Program
    {
        private static double eye_x, eye_y, eye_z, scale, aX, aY, aZ, angle;
        public static int[] texture = new int[6];
        static Glu.GLUquadric quadratic;

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
            LoadTexture();
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glShadeModel(Gl.GL_SMOOTH);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            float[] light= new float[3] {1, 0.5F, 1};
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

        static void OnDisplay()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();
            Glu.gluLookAt(eye_x, eye_y, eye_z, 0, -10, 0, 1, 0, 0);
            Gl.glRotated(aX, 1, 0, 0);
            Gl.glRotated(aY, 0, 1, 0);
            Gl.glRotated(aZ, 0, 0, 1);
            Gl.glTranslated(1.5, 0, 0);


            //основание
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[0]);
            Gl.glTranslated(-1.5, 0, 0);
            Glu.gluSphere(quadratic, 1.1, 50, 50);
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


            Glut.glutSwapBuffers();
        }

        static void OnReshape(int w, int h)
            {
                Gl.glMatrixMode(Gl.GL_PROJECTION);
                Gl.glLoadIdentity();
                Gl.glViewport(0, 0, w, h);
                Glu.gluPerspective(40, w / h, 1, 100);
                Gl.glMatrixMode(Gl.GL_MODELVIEW);
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

