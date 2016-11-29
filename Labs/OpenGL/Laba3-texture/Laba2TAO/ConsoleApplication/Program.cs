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
        static Glu.GLUquadric quadratic;


        void gltMakeShadowMatrix(GLTVector3 vPoints[3], GLTVector4 vLightPos, GLTMatrix destMat)
        {
            Gl.glVertex4f(1, 1, 1, 1);
            GLfloat dot;
            gltGetPlaneEquation(vPoints[0], vPoints[1], vPoints[2], vPlaneEquation);
            // Вычисляет скалярное произведение направляющего вектора плоскости
            // и вектора положения источника света
            dot = vPlaneEquation[0] * vLightPos[0] +
                vPlaneEquation[1] * vLightPos[1] +
                vPlaneEquation[2] * vLightPos[2] +
                vPlaneEquation[3] * vLightPos[3];
            // Формируем матрицу проекции
            // Первый столбец
            destMat[0] = dot - vLightPos[0] * vPlaneEquation[0];
            destMat[4] = 0.0f - vLightPos[0] * vPlaneEquation[1];
            destMat[8] = 0.0f - vLightPos[0] * vPlaneEquation[2];
            destMat[12] = 0.0f - vLightPos[0] * vPlaneEquation[3];
            // Второй столбец
            destMat[1] = 0.0f - vLightPos[1] * vPlaneEquation[0];
            destMat[5] = dot - vLightPos[1] * vPlaneEquation[1];
            destMat[9] = 0.0f - vLightPos[1] * vPlaneEquation[2];
            destMat[13] = 0.0f - vLightPos[1] * vPlaneEquation[3];
            // Третий столбец
            destMat[2] = 0.0f - vLightPos[2] * vPlaneEquation[0];
            destMat[6] = 0.0f - vLightPos[2] * vPlaneEquation[1];
            destMat[10] = dot - vLightPos[2] * vPlaneEquation[2];
            destMat[14] = 0.0f - vLightPos[2] * vPlaneEquation[3];
            // Четвертый столбец
            destMat[3] = 0.0f - vLightPos[3] * vPlaneEquation[0];
            destMat[7] = 0.0f - vLightPos[3] * vPlaneEquation[1];
            destMat[11] = 0.0f - vLightPos[3] * vPlaneEquation[2];
            destMat[15] = dot - vLightPos[3] * vPlaneEquation[3];
        }
        //**********************************************************
        // Умножить вектор на скаляр
         static void gltScaleVector(GLTVector3 vVector, const GLfloat fScale)
        {

            vVector[0] *= fScale; vVector[1] *= fScale; vVector[2] *= fScale;
        }
    //**********************************************************
    GLfloat gltGetVectorLengthSqrd(const GLTVector3 vVector)
    {
        return (vVector[0] * vVector[0]) + (vVector[1] * vVector[1]) + (vVector[2] * vVector[2]);
    }

    GLfloat gltGetVectorLength(const GLTVector3 vVector)
    {
        return (GLfloat)sqrt(gltGetVectorLengthSqrd(vVector));
    }
    //**********************************************************
    // Привести вектор к единичной длине (нормировать)
    void gltNormalizeVector(GLTVector3 vNormal)
    {
        GLfloat fLength = 1.0f / gltGetVectorLength(vNormal);
        gltScaleVector(vNormal, fLength);
    }
    // Вычесть один вектор из другого
    void gltSubtractVectors(const GLTVector3 vFirst, const GLTVector3 vSecond, GLTVector3 vResult)
    {
        vResult[0] = vFirst[0] - vSecond[0];
        vResult[1] = vFirst[1] - vSecond[1];
        vResult[2] = vFirst[2] - vSecond[2];
    }

    //**********************************************************
    // Вычислить векторное произведение двух векторов
    void gltVectorCrossProduct(const GLTVector3 vU, const GLTVector3 vV, GLTVector3 vResult)
    {
        vResult[0] = vU[1] * vV[2] - vV[1] * vU[2];
        vResult[1] = -vU[0] * vV[2] + vV[0] * vU[2];
        vResult[2] = vU[0] * vV[1] - vV[0] * vU[1];
    }

    // Вычислить нормаль по трем точкам
    void gltGetNormalVector(const GLTVector3 vP1, const GLTVector3 vP2, const GLTVector3 vP3, GLTVector3 vNormal)
    {
        GLTVector3 vV1, vV2;
        gltSubtractVectors(vP2, vP1, vV1);
        gltSubtractVectors(vP3, vP1, vV2);

        gltVectorCrossProduct(vV1, vV2, vNormal);
        gltNormalizeVector(vNormal);
    }

    //**********************************************************
    // Возвращает коэффициенты уравнения плоскости по трем точкам
    static void gltGetPlaneEquation(Vector3 vPoint1, GLTVector3 vPoint2, GLTVector3 vPoint3, GLTVector3 vPlane)
    {
        // Вычислить вектор нормали
        gltGetNormalVector(vPoint1, vPoint2, vPoint3, vPlane);

        vPlane[3] = -(vPlane[0] * vPoint3[0] + vPlane[1] * vPoint3[1] + vPlane[2] * vPoint3[2]);
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


