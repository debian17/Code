﻿using System;
using System.Collections.Generic;
using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace ConsoleApplication
{
    internal class Program
    {
            static void init_graphics()
            {
                Gl.glEnable(Gl.GL_LIGHTING);
                Gl.glEnable(Gl.GL_LIGHT0);
                float[] light_pos = new float[3] {1, 0.5F, 1};
                Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light_pos);
                Gl.glEnable(Gl.GL_DEPTH_TEST);
                Gl.glClearColor(1, 1, 1, 1);
            }

            static void on_display()
            {
                Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
                Gl.glLoadIdentity();
                Glu.gluLookAt(
                    -4, 0, 4,
                    0, 0, 0,
                    1, 0, 0);
                Glut.glutSolidCone(0.5,2,40,1);
                Glut.glutSwapBuffers();
            }

            static void on_reshape(int w, int h)
            {
                Gl.glMatrixMode(Gl.GL_PROJECTION);
                Gl.glLoadIdentity();
                Gl.glViewport(0, 0, w, h);
                Glu.gluPerspective(40, w / h, 1, 100);
                Gl.glMatrixMode(Gl.GL_MODELVIEW);
            }

        public static void Main()
        {
            Glut.glutInit();
            Glut.glutInitWindowSize(400, 400);
            Glut.glutCreateWindow("Tao Example");
            init_graphics();
            Glut.glutReshapeFunc(on_reshape);
            Glut.glutDisplayFunc(on_display);



            Glut.glutMainLoop();
        }
    }
}

