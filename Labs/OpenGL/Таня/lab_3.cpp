#include <windows.h>
#include <stdlib.h> 
#include <time.h>
#include <GL\glut.h>
#include <GL\GLAux.h>
#include <math.h>

float width_win = 700;
float higth_win = 700;
float diffuse[] = { 1.0, 1.0, 1.0, 1.0 };
float pos[] = { width_win / 2, higth_win / 2, higth_win / 2, 1.0 };// (x, y, z) задают вектор направления, а компонента w всегда равна нулю (иначе источник превратится в точечный).
float angel = 0.0;
float rot = 0.0;
float scal = 0.0;
AUX_RGBImageRec* photo_image;

//unsigned int textures[1];

/*void LoadTextures(){
//glPixelStorei(GL_UNPACK_ALIGNMENT, 1);
	AUX_RGBImageRec*texture1=auxDIBImageLoadA("kk.bmp");//загружаем текстуру
	glGenTextures(1, &textures[0]); //берем адрес первого эл
	glBindTexture(GL_TEXTURE_2D, textures[0]);
	//


	glPixelStorei(GL_UNPACK_ALIGNMENT, 1);
	gluBuild2DMipmaps(GL_TEXTURE_2D, GL_RGB, texture1->sizeX,
		texture1->sizeY, GL_RGB, GL_UNSIGNED_BYTE,
		texture1->data);
	glTexParameterf(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER,
		GL_NEAREST);
	glTexParameterf(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER,
		GL_NEAREST);
	glTexParameterf(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_NEAREST);
	glTexParameterf(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_NEAREST);
	
	
	/*glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
		
		//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP);
	//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP);
	glTexImage2D (GL_TEXTURE_2D, 0, 3,texture1->sizeX, texture1->sizeY, 0,
		GL_RGB, GL_UNSIGNED_BYTE, texture1->data);
	//glTexEnvf(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE,
		(float)GL_REPLACE);
}*/


void Draw(){

	GLUquadricObj *quadObj;

	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	float size = 50;
	glEnable(GL_DEPTH_TEST);//проверка глубины

	//голова верх
	/*glPushMatrix();
	glColor3f(0.0, 1.0, 0.0);
	glScalef(1.0, 1.1, 1.0);
	glutSolidSphere(50, 30, 30);
	glPopMatrix();*/
	glEnable(GL_TEXTURE_2D);
	photo_image = auxDIBImageLoadA("kk.bmp");
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);

	glPixelStorei(GL_UNPACK_ALIGNMENT, 1);
	glTexImage2D(GL_TEXTURE_2D, 0, 3,
		photo_image->sizeX,
		photo_image->sizeY,
		0, GL_RGB, GL_UNSIGNED_BYTE,
		photo_image->data);
	quadObj = gluNewQuadric();
	gluQuadricTexture(quadObj, GL_TRUE);
	gluQuadricDrawStyle(quadObj, GLU_FILL);
	glColor3d(1, 1, 1);
	glPushMatrix();
	glScalef(1.0, 1.1, 1.0);
	glRotated(-90, 1, 0, 0);
	gluSphere(quadObj, 50, 30, 30);
	glPopMatrix();
	gluDeleteQuadric(quadObj);
	glDisable(GL_TEXTURE_2D);



	
	//повязка
	glEnable(GL_TEXTURE_2D);
	photo_image = auxDIBImageLoadA("p.bmp");
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);

	glPixelStorei(GL_UNPACK_ALIGNMENT, 1);
	glTexImage2D(GL_TEXTURE_2D, 0, 3,
		photo_image->sizeX,
		photo_image->sizeY,
		0, GL_RGB, GL_UNSIGNED_BYTE,
		photo_image->data);
	quadObj = gluNewQuadric();
	gluQuadricTexture(quadObj, GL_TRUE);
	gluQuadricDrawStyle(quadObj, GLU_FILL);
	glColor3d(1, 1, 1);
	glPushMatrix();
	glTranslatef(0, 7, 0);
	glRotated(-90, 1, 0, 0);
	gluCylinder(quadObj, 51, 43, 25, 50, 50);
	glPopMatrix();
	gluDeleteQuadric(quadObj);
	glDisable(GL_TEXTURE_2D);


	//голова низ
	glEnable(GL_TEXTURE_2D);
	photo_image = auxDIBImageLoadA("kk.bmp");
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);

	glPixelStorei(GL_UNPACK_ALIGNMENT, 1);
	glTexImage2D(GL_TEXTURE_2D, 0, 3,
		photo_image->sizeX,
		photo_image->sizeY,
		0, GL_RGB, GL_UNSIGNED_BYTE,
		photo_image->data);
	quadObj = gluNewQuadric();
	gluQuadricTexture(quadObj, GL_TRUE);
	gluQuadricDrawStyle(quadObj, GLU_FILL);
	glColor3d(1, 1, 1);
	glPushMatrix();
	glTranslatef(0, -16, 8);
	glScalef(1.3, 0.8, 1.1);
	glRotated(-90, 1, 0, 0);
	gluSphere(quadObj, 50, 50, 50);
	glPopMatrix();
	gluDeleteQuadric(quadObj);
	glDisable(GL_TEXTURE_2D);
	


	//рот
	glPushMatrix();
	glColor3f(63.0 / 255.0, 92.0 / 255.0, 20.0 / 255.0);
	glRotatef(-20, 1.0, 0.0, 0.0);
	glTranslatef(0, -39, 20);
	glScalef(1.2, 0.2, 0.9);
	glutSolidSphere(40, 30, 30);
	glPopMatrix();

	glPushMatrix();
	glColor3f(63.0 / 255.0, 92.0 / 255.0, 20.0 / 255.0);
	glRotatef(20, 1.0, 0.0, 0.0);
	glTranslatef(0, -9, 35);
	glScalef(1.2, 0.2, 0.9);
	glutSolidSphere(40, 30, 30);
	glPopMatrix();


	//правый глаз
	glPushMatrix();
	glColor3f(1.0, 1.0, 1.0);
	glTranslatef(15, 13, size - 10);
	glScalef(1.5, 1.0, 1.0);
	glutSolidSphere(10, 30, 30);
	glPopMatrix();

	glPushMatrix();
	glColor3f(0.0, 0.0, 0.0);
	glTranslatef(15, 13, size - 6.8);
	//glScalef(1.0, 1.1, 1.0);
	glutSolidSphere(7.5, 30, 30);
	glPopMatrix();

	//левый глаз
	glPushMatrix();
	glColor3f(1.0, 1.0, 1.0);
	glTranslatef(-15, 13, size - 10);
	glScalef(1.5, 1.0, 1.0);
	glutSolidSphere(10, 30, 30);
	glPopMatrix();

	glPushMatrix();
	glColor3f(0.0, 0.0, 0.0);
	glTranslatef(-15, 13, size - 6.8);
	glutSolidSphere(7.5, 30, 30);
	glPopMatrix();


	//надпись
	glEnable(GL_TEXTURE_2D);
	photo_image = auxDIBImageLoadA("r.bmp");
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
	glPixelStorei(GL_UNPACK_ALIGNMENT, 1);
	glTexImage2D(GL_TEXTURE_2D, 0, 3,
		photo_image->sizeX,
		photo_image->sizeY,
		0, GL_RGB, GL_UNSIGNED_BYTE,
		photo_image->data);
	quadObj = gluNewQuadric();
	gluQuadricTexture(quadObj, GL_TRUE);
	gluQuadricDrawStyle(quadObj, GLU_FILL);
	
	
	


	glColor3d(1, 1, 1);
	glPushMatrix();
	glTranslatef(0, -120, 0);
	glRotated(-90, 1, 0, 0);
	glRotated(80, 0, 0, 1);
	gluCylinder(quadObj, 100, 100, 40, 50, 50);
	glPopMatrix();
	gluDeleteQuadric(quadObj);
	
	glDisable(GL_BLEND);
	glDisable(GL_TEXTURE_2D);




	glDisable(GL_DEPTH_TEST);


	glutSwapBuffers();


}


void Timer(int value){
	glutPostRedisplay();
	glutTimerFunc(1, Timer, 0);
}






void Keyboard(unsigned char key, int x, int y){

	switch (key){
		//вращение
	case 'd':
		angel = 0;
		angel++;
		glRotatef(angel, 0.0, 1.0, 0.0);
		glutPostRedisplay();
		break;
	case 'a':
		angel = 0;
		angel--;
		glRotatef(angel, 0.0, 1.0, 0.0);
		glutPostRedisplay();
		break;
	case 'w':
		angel = 0;
		angel--;
		glRotatef(angel, 1.0, 0.0, 0.0);
		glutPostRedisplay();
		break;
	case 'x':
		angel = 0;
		angel++;
		glRotatef(angel, 1.0, 0.0, 0.0);
		glutPostRedisplay();
		break;
	case 'e':
		angel = 0;
		angel--;
		glRotatef(angel, 0.0, 0.0, 1.0);
		glutPostRedisplay();
		break;
	case 'z':
		angel = 0;
		angel++;
		glRotatef(angel, 0.0, 0.0, 1.0);
		glutPostRedisplay();
		break;
		//движение по осям
	case '6':
		rot = 0;
		rot++;
		glTranslatef(rot, 0.0, 0.0);
		glutPostRedisplay();
		break;
	case '4':
		rot = 0;
		rot--;
		glTranslatef(rot, 0.0, 0.0);
		glutPostRedisplay();
		break;
	case '8':
		rot = 0;
		rot++;
		glTranslatef(0.0, rot, 0.0);
		glutPostRedisplay();
		break;
	case '2':
		rot = 0;
		rot--;
		glTranslatef(0.0, rot, 0.00);
		glutPostRedisplay();
		break;
	case '9':
		rot = 0;
		rot--;
		glTranslatef(0.0, 0.0, rot);
		glutPostRedisplay();
		break;
	case '1':
		rot = 0;
		rot++;
		glTranslatef(0.0, 0.0, rot);
		glutPostRedisplay();
		break;

	}
}

void SKeyboard(int key, int x, int y){
	switch (key){
	case GLUT_KEY_UP:

		scal = 1.0;
		scal += 0.01;
		glScalef(scal, scal, scal);
		glutPostRedisplay();
		break;
	case GLUT_KEY_DOWN:

		scal = 1.0;
		scal -= 0.01;
		glScalef(scal, scal, scal);
		glutPostRedisplay();
		break;


	}

}



void Init(){
	//LoadTextures();
	//glEnable(GL_TEXTURE_2D);
	glClearColor(180.0 / 255.0, 243.0 / 255.0, 246.0 / 255.0, 1.0);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	//перспектива
	glOrtho(-width_win / 2, width_win / 2, -higth_win / 2, higth_win / 2, -higth_win / 2, higth_win / 2);
	glMatrixMode(GL_MODELVIEW);


}


int main(int argc, char**argv){
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB | GLUT_DEPTH);
	glutInitWindowSize(width_win, higth_win);

	glutCreateWindow("Черепашка");
	glutDisplayFunc(Draw);
	glShadeModel(GL_SMOOTH);

	glEnable(GL_LIGHTING);
	glEnable(GL_LIGHT0);
	glLightfv(GL_LIGHT0, GL_DIFFUSE, diffuse);
	glLightfv(GL_LIGHT0, GL_POSITION, pos);
	glEnable(GL_COLOR_MATERIAL);

	Init();
	//glutTimerFunc(33, Timer, 0);
	//glutMouseFunc(MousePress);



	glutKeyboardFunc(Keyboard);
	glutSpecialFunc(SKeyboard);

	glutMainLoop();
	return 0;

}