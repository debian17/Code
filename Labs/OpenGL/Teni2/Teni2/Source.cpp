#include <stdlib.h>
#include <glut.h>
#include <glaux.h>
#include <cmath>

using namespace std;

GLfloat WinWid = 640.0f;
GLfloat WinHei = 480.0f;
GLfloat Anglex = 0.0f, Angley = 0.0f, Anglez = 0.0f;
GLfloat X = 0.0f, Y = 0.0f, Z = 0.0f, sX = 1.0f, sY = 1.0f, sZ = 1.0f, yRot, xRot;
GLfloat lx = -0.0, ly = 0.0;
GLfloat ambientLight[] = { 0.3f, 0.3f, 0.3f, 1.0f };// Фоновый слабобелый
GLfloat diffuseLight[] = { 1.0f, 1.0f, 1.0f, 1.0f };// Рассеяный среднебелый
GLfloat light_position[] = { -200, WinHei / 2, -30.0, 0.0 };// Расположение источника
GLfloat  specular[] = { 1.0f, 1.0f, 1.0f, 1.0f };
GLfloat  spotDir[] = { 0.0f, 0.0f, -1.0f };

typedef GLfloat GLTVector3[3];
typedef GLfloat GLTVector4[4];
typedef GLfloat GLTMatrix[16];

void gltMakeShadowMatrix(GLTVector3 vPoints[3], GLTVector4 vLightPos, GLTMatrix destMat);
void gltGetPlaneEquation(GLTVector3 vPoint1, GLTVector3 vPoint2, GLTVector3 vPoint3, GLTVector3 vPlane);
GLTMatrix shadowMat;
GLuint	texture[6];

GLvoid LoadGLTextures()
{
	// Загрузка картинки
	AUX_RGBImageRec *texture1 = auxDIBImageLoadA("C:\\GitHub\\Labs\\OpenGL\\Teni2\\img\\snow.bmp"),
		*texture2 = auxDIBImageLoadA("C:\\GitHub\\Labs\\OpenGL\\Teni2\\img\\heat.bmp"),
		*texture3 = auxDIBImageLoadA("C:\\GitHub\\Labs\\OpenGL\\Teni2\\img\\nose.bmp"),
		*texture4 = auxDIBImageLoadA("C:\\GitHub\\Labs\\OpenGL\\Teni2\\img\\eye.bmp"),
		*texture5 = auxDIBImageLoadA("C:\\GitHub\\Labs\\OpenGL\\Teni2\\img\\hand.bmp"),
		*texture6 = auxDIBImageLoadA("C:\\GitHub\\Labs\\OpenGL\\Teni2\\img\\land.bmp");

	// Создание текстуры
	glGenTextures(6, &texture[0]);
	glBindTexture(GL_TEXTURE_2D, texture[0]);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexImage2D(GL_TEXTURE_2D, 0, 3, texture1->sizeX, texture1->sizeY, 0,
		GL_RGB, GL_UNSIGNED_BYTE, texture1->data);
	glBindTexture(GL_TEXTURE_2D, texture[1]);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexImage2D(GL_TEXTURE_2D, 0, 3, texture2->sizeX, texture2->sizeY, 0,
		GL_RGB, GL_UNSIGNED_BYTE, texture2->data);
	glBindTexture(GL_TEXTURE_2D, texture[2]);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexImage2D(GL_TEXTURE_2D, 0, 3, texture3->sizeX, texture3->sizeY, 0,
		GL_RGB, GL_UNSIGNED_BYTE, texture3->data);
	glBindTexture(GL_TEXTURE_2D, texture[3]);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexImage2D(GL_TEXTURE_2D, 0, 3, texture4->sizeX, texture4->sizeY, 0,
		GL_RGB, GL_UNSIGNED_BYTE, texture4->data);
	glBindTexture(GL_TEXTURE_2D, texture[4]);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexImage2D(GL_TEXTURE_2D, 0, 3, texture5->sizeX, texture5->sizeY, 0,
		GL_RGB, GL_UNSIGNED_BYTE, texture5->data);
	glBindTexture(GL_TEXTURE_2D, texture[5]);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexImage2D(GL_TEXTURE_2D, 0, 3, texture6->sizeX, texture6->sizeY, 0,
		GL_RGB, GL_UNSIGNED_BYTE, texture6->data);
	glBindTexture(GL_TEXTURE_2D, texture[1]);
}

void Jet(int nShadow, GLenum order)
{
	float size = 50;
	GLUquadricObj *quadObj;
	glPushMatrix();
	glEnable(GL_CULL_FACE);
	glCullFace(GL_BACK);
	glFrontFace(order);
	glEnd();
	glDisable(GL_CULL_FACE);

	glBindTexture(GL_TEXTURE_2D, texture[0]);
	// Рисуем плоскость, имитирующую землю
	glBegin(GL_QUADS);
	glColor3d(255, 255, 255);

	glTexCoord2f(0.0f, 0.0f);
	glVertex3f(400.0f, -150.0f, -300.0f);

	glTexCoord2f(0.0f, 1.0f);
	glVertex3f(-100.0f, -150.0f, -300.0f);

	glColor3d(222, 184, 135);

	glTexCoord2f(1.0f, 0.0f);
	glVertex3f(-250.0f, -150.0f, 300.0f);

	glTexCoord2f(1.0f, 1.0f);
	glVertex3f(800.0f, -150.0f, 300.0f);

	glEnd();
	glBindTexture(GL_TEXTURE_2D, texture[1]);

	// Каким цветом рисовать
	if (nShadow == 0)  // Рисовать белым цветом 
		glColor3d(1, 1, 1);
	else        // Рисовать черным цветом тень 
		glColor3ub(0, 0, 0);

	glTranslatef(X, Y, Z);
	glRotatef(Anglex, 1.0, 0.0, 0.0);
	glRotatef(Angley, 0.0, 1.0, 0.0);
	glRotatef(Anglez, 0.0, 0.0, 1.0);
	glTranslatef(-X, -Y, -Z);
	glScalef(sX, sY, sZ);
	glTranslatef(X, 0.0, 0.0);
	glTranslatef(0.0, Y, 0.0);
	glTranslatef(0.0, 0.0, Z);
	
	//основание
	glBindTexture(GL_TEXTURE_2D, texture[0]);
	quadObj = gluNewQuadric();
	gluQuadricTexture(quadObj, GL_TRUE);
	gluQuadricDrawStyle(quadObj, GLU_FILL);
	glPushMatrix();
	glTranslated(0, 0, 100);
	gluSphere(quadObj, 50, 30, 30);
	glPopMatrix();
	gluDeleteQuadric(quadObj);

	//тело
	glBindTexture(GL_TEXTURE_2D, texture[0]);
	quadObj = gluNewQuadric();
	gluQuadricTexture(quadObj, GL_TRUE);
	gluQuadricDrawStyle(quadObj, GLU_FILL);
	glPushMatrix();
	glTranslated(0, 60, 100);
	gluSphere(quadObj, 35, 30, 30);
	glPopMatrix();
	gluDeleteQuadric(quadObj);

	//голова
	glBindTexture(GL_TEXTURE_2D, texture[0]);
	quadObj = gluNewQuadric();
	gluQuadricTexture(quadObj, GL_TRUE);
	gluQuadricDrawStyle(quadObj, GLU_FILL);
	glPushMatrix();
	glTranslated(0, 100, 100);
	gluSphere(quadObj, 25, 30, 30);
	glPopMatrix();
	gluDeleteQuadric(quadObj);

	//правая рука
	glBindTexture(GL_TEXTURE_2D, texture[4]);
	quadObj = gluNewQuadric();
	gluQuadricTexture(quadObj, GL_TRUE);
	gluQuadricDrawStyle(quadObj, GLU_FILL);
	glPushMatrix();
	glTranslated(0, 100, 100);
	glRotated(90, 0, 1, 0);
	glTranslated(0, -40, 0);
	glRotated(-20, 1, 0, 0);
	gluCylinder(quadObj, 2, 2, 110, 30, 30);
	glPopMatrix();
	gluDeleteQuadric(quadObj);

	//левая рука
	glBindTexture(GL_TEXTURE_2D, texture[4]);
	quadObj = gluNewQuadric();
	gluQuadricTexture(quadObj, GL_TRUE);
	gluQuadricDrawStyle(quadObj, GLU_FILL);
	glPushMatrix();
	glTranslated(0, 100, 100);
	glRotated(-90, 0, 1, 0);
	glTranslated(0, -40, 0);
	glRotated(-20, 1, 0, 0);
	gluCylinder(quadObj, 2, 2, 110, 30, 30);
	glPopMatrix();
	gluDeleteQuadric(quadObj);

	//шляпа
	glBindTexture(GL_TEXTURE_2D, texture[1]);
	quadObj = gluNewQuadric();
	gluQuadricTexture(quadObj, GL_TRUE);
	gluQuadricDrawStyle(quadObj, GLU_FILL);
	glPushMatrix();
	glTranslated(0, 117, 100);
	glRotated(-90, 1, 0, 0);
	gluCylinder(quadObj, 22, 0, 30, 30, 30);
	glPopMatrix();
	gluDeleteQuadric(quadObj);

	//нос
	glBindTexture(GL_TEXTURE_2D, texture[2]);
	quadObj = gluNewQuadric();
	gluQuadricTexture(quadObj, GL_TRUE);
	gluQuadricDrawStyle(quadObj, GLU_FILL);
	glPushMatrix();
	glTranslated(0, 105, 100);
	gluCylinder(quadObj, 5, 0, 45, 30, 30);
	glPopMatrix();
	gluDeleteQuadric(quadObj);

	//правый глаз
	glBindTexture(GL_TEXTURE_2D, texture[3]);
	quadObj = gluNewQuadric();
	gluQuadricTexture(quadObj, GL_TRUE);
	gluQuadricDrawStyle(quadObj, GLU_FILL);
	glPushMatrix();
	glTranslated(10, 110, 121);
	gluSphere(quadObj, 2, 30, 30);
	glPopMatrix();
	gluDeleteQuadric(quadObj);

	//левый глаз
	glBindTexture(GL_TEXTURE_2D, texture[3]);
	quadObj = gluNewQuadric();
	gluQuadricTexture(quadObj, GL_TRUE);
	gluQuadricDrawStyle(quadObj, GLU_FILL);
	glPushMatrix();
	glTranslated(-10, 110, 121);
	gluSphere(quadObj, 2, 30, 30);
	glPopMatrix();
	gluDeleteQuadric(quadObj);

	//рот
	glBindTexture(GL_TEXTURE_2D, texture[4]);
	quadObj = gluNewQuadric();
	gluQuadricTexture(quadObj, GL_TRUE);
	gluQuadricDrawStyle(quadObj, GLU_FILL);
	glPushMatrix();
	glTranslated(-10, 98.5, 123.5);
	glRotated(90, 0, 1, 0);
	gluCylinder(quadObj, 2, 2, 20, 30, 30);
	glPopMatrix();
	gluDeleteQuadric(quadObj);

	glPopMatrix();
}

//освещение
void SetLightJetLight()
{
	glDisable(GL_DITHER);
	glEnable(GL_LIGHTING);
	glEnable(GL_LIGHT0);
	// Настройка источника света
	glLightfv(GL_LIGHT0, GL_DIFFUSE, diffuseLight);
	glLightfv(GL_LIGHT0, GL_SPECULAR, specular);
	glLightfv(GL_LIGHT0, GL_POSITION, light_position);
	// Включаем поддержку согласования цветов
	glEnable(GL_COLOR_MATERIAL);
	// Режим согласования цветов назначаем для фонового и рассеянного
	glColorMaterial(GL_FRONT, GL_AMBIENT_AND_DIFFUSE);
	glMaterialfv(GL_FRONT, GL_SPECULAR, specular);// Начальное значение диффузного материала
	glMateriali(GL_FRONT, GL_SHININESS, 10);// Яркий блик
    // Любые три точки на плоскости, имитирующей землю
	GLTVector3 points[3] = { { -30.0f, -149.0f, -20.0f },
	{ -30.0f, -149.0f, 20.0f },
	{ 40.0f, -149.0f, 20.0f } };
	// Вычисление матрицы проекции тени на плоскость основания (землю)
	gltMakeShadowMatrix(points, light_position, shadowMat);
}

void Display() {
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	SetLightJetLight();
	// Ориентируем прожектор
	glEnable(GL_LIGHTING);
	glLightfv(GL_LIGHT0, GL_POSITION, light_position);
	glPushMatrix();
	glScalef(-1., 1., 1.);
	glTranslatef(300., 0., 0.);
	Jet(0, GL_CW);
	glPopMatrix();
	glClear(GL_DEPTH_BUFFER_BIT);
	glPushAttrib(0xffffffff);
	glDisable(GL_LIGHTING);
	glEnable(GL_BLEND);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	glColor4f(0., 0., 0., 0.1);
	glBegin(GL_QUADS);
	glVertex3f(0., 250., -300.);
	glVertex3f(0., -150., -300.);
	glVertex3f(-250., -150., 300);
	glVertex3f(-250., 250., 300);
	glEnd();
	glPopAttrib();
	Jet(0, GL_CCW);
	// Перед рисованием тени отключаем освещение и глубину
	// Чтобы тень была чисто черной и плоской
	glDisable(GL_DEPTH_TEST);
	glDisable(GL_LIGHTING);
	glPushMatrix();
	// Умножение на матрицу преобразования тени
	glMultMatrixf((GLfloat *)shadowMat);
	// Нарисуем тень самолета
	Jet(1, GL_CCW);
	glPopMatrix();
	// Рисуем источник света
	glPushMatrix();
	// Поворот системы координат
	glTranslatef(lx, ly, 0.0);
	// Перемещаем точку рисования  
	glTranslatef(light_position[0], light_position[1], light_position[2]);
	// Сохраняем атрибуты освещения в стеке атрибутов
	glPushAttrib(GL_LIGHTING_BIT);
	// Выключаем временно освещение
	glDisable(GL_LIGHTING);
	glPopAttrib();
	glPopMatrix();

	glEnable(GL_DEPTH_TEST);

	glutSwapBuffers();
	glFlush();
}

void Timer(int) {
	glutPostRedisplay();
	glutTimerFunc(10, Timer, 0);
}

void Keyboard(unsigned char key, int x, int y) {
	switch (key)
	{
	case 'a': Angley++;
		break;
	case 'd': Angley--;
		break;
	case 'w': Anglex++;
		break;
	case 's': Anglex--;
		break;
	case 'q': Anglez++;
		break;
	case 'e': Anglez--;
		break;
	case '+':
		if (sX < 2.2) {
			sX += 0.1f; sY += 0.1f; sZ += 0.1f;
		}
		break;
	case '-':
		if (sX > 0) {
			sX -= 0.1f; sY -= 0.1f; sZ -= 0.1f;
		}
		break;

	case 'g':
		light_position[0] += 0.5;
		lx += 0.5;
		if (lx >= 0) {
			light_position[1] -= 0.1;
			ly -= 0.1;
		}
		else {
			light_position[1] += 0.1;
			ly += 0.1;
		}
		break;
	case 'f':
		light_position[0] -= 0.5;
		lx -= 0.5;
		if (lx <= 0) {
			light_position[1] -= 0.1;
			ly -= 0.1;
		}
		else {
			light_position[1] += 0.1;
			ly += 0.1;
		}
		break;

	default:
		break;
	}
}

void SKeyboard(int key, int x, int y) {
	switch (key)
	{
	case GLUT_KEY_LEFT: if (X>-80) X -= 2;
		break;
	case GLUT_KEY_RIGHT: X += 2;
		break;
	case GLUT_KEY_UP:
		Y += 2;
		break;
	case GLUT_KEY_DOWN: if (Y >= -55) Y -= 2;
		break;
	case GLUT_KEY_PAGE_UP: Z += 2;
		break;
	case GLUT_KEY_PAGE_DOWN: Z -= 2;
		break;
	default:
		break;
	}
}

void Reshape(int width, int height) {
	SetLightJetLight();
	glBindTexture(GL_TEXTURE_2D, texture[1]);

	glEnable(GL_DEPTH_TEST);  // Включить тест глубины
	glClearColor(255.0 / 255.0, 255.0 / 255.0, 255.0 / 255.0, 1.0);// Цвет фона окна
    // Предотвращаем деление на нуль
	if (height == 0)
		height = 1;
	// Устанавливаем поле просмотра с размерами окна
	glViewport(0, 0, width, height);
	// Устанавливает матрицу преобразования в режим проецирования
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	// Устанавливаем размеры отсекающего объема
	GLfloat aspectRatio = (GLfloat)width / (GLfloat)height;// Для соблюдения пропорций
	gluPerspective(60.0f, aspectRatio, 1.0f, 500.0f);    // Отсекающая перспектива
														 // Восстановливает матрицу преобразования в исходный режим вида
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
	// Отодвинем сцену в отрицательную сторону оси 0z
	glTranslatef(0.0f, -50.0f, -500.0f);
}

int main(int argc, char** argv) {
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA | GLUT_DEPTH);
	glutInitWindowSize(WinWid, WinHei);
	glutInitWindowPosition(100, 100);
	glutCreateWindow("Зовите меня Йети, я снежный человек!");
	LoadGLTextures();
	glEnable(GL_TEXTURE_2D);
	glutDisplayFunc(Display);
	glutReshapeFunc(Reshape);
	glutTimerFunc(10, Timer, 0);
	glutKeyboardFunc(Keyboard);
	glutSpecialFunc(SKeyboard);
	glutMainLoop();
	return 0;
}

void gltMakeShadowMatrix(GLTVector3 vPoints[3], GLTVector4 vLightPos, GLTMatrix destMat)
{
	GLTVector4 vPlaneEquation;
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

// Умножить вектор на скаляр
void gltScaleVector(GLTVector3 vVector, const GLfloat fScale)
{
	vVector[0] *= fScale; vVector[1] *= fScale; vVector[2] *= fScale;
}

GLfloat gltGetVectorLengthSqrd(const GLTVector3 vVector)
{
	return (vVector[0] * vVector[0]) + (vVector[1] * vVector[1]) + (vVector[2] * vVector[2]);
}

GLfloat gltGetVectorLength(const GLTVector3 vVector)
{
	return (GLfloat)sqrt(gltGetVectorLengthSqrd(vVector));
}

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

// Возвращает коэффициенты уравнения плоскости по трем точкам
void gltGetPlaneEquation(GLTVector3 vPoint1, GLTVector3 vPoint2, GLTVector3 vPoint3, GLTVector3 vPlane)
{
	// Вычислить вектор нормали
	gltGetNormalVector(vPoint1, vPoint2, vPoint3, vPlane);
	vPlane[3] = -(vPlane[0] * vPoint3[0] + vPlane[1] * vPoint3[1] + vPlane[2] * vPoint3[2]);
}


