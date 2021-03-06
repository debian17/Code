﻿using Tao.FreeGlut;
using Tao.OpenGl;

namespace ConsoleApplication
{
    internal class Program
    {
        private static double eye_x, eye_y, eye_z, scale, aX, aY, aZ, angle;

        static void Init_graphics()
            {
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            float[] light= new float[3] {1, 0.5F, 1};
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light);
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glClearColor(1, 1, 1, 1);

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

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            
            //Glu.gluQuadricTexture();

            //основание
            Gl.glTranslated(-1.5, 0, 0);
            Glut.glutSolidSphere(1.1, 50, 50);

            //тело
            Gl.glTranslated(1.6, 0, 0);
            Glut.glutSolidSphere(0.75, 50, 50);

            //голова
            Gl.glTranslated(1.1, 0, 0);
            Glut.glutSolidSphere(0.55, 50, 50);

            //правая рука
            Gl.glTranslated(-1, 0, 0);
            Gl.glRotated(20, 0, 1, 0);
            Glut.glutSolidCylinder(0.05, 2.5, 50, 50);

            //левая рука
            Gl.glRotated(-140, 0, -1, 0);
            Glut.glutSolidCylinder(0.05, 2.5, 50, 50);

            //шляпа
            Gl.glTranslated(-1.3, 0, 0.47);
            Gl.glRotated(290, 0, 1, 0);
            Glut.glutSolidCone(0.5, 0.75, 50, 50);

            //нос
            Gl.glTranslated(0, 0.3, -0.4);
            Gl.glRotated(275, 1, 0, 0);
            Glut.glutSolidCone(0.07, 0.75, 50, 50);

            //правый глаз
            Gl.glTranslated(-0.2, -0.25, 0.15);
            Glut.glutSolidSphere(0.05, 50, 50);

            //левый глаз
            Gl.glTranslated(0.4, 0, 0);
            Glut.glutSolidSphere(0.05, 50, 50);

            //рот
            Gl.glTranslated(-0.47, 0.4, 0.0175);
            Gl.glRotated(90, 0, 1, 0);
            Glut.glutSolidCylinder(0.05, 0.5, 50, 50);

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


