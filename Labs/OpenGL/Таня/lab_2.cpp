#include <windows.h>
#include <stdlib.h> 
#include <time.h>
#include <GL\glut.h>
#include <math.h>

float width_win=700;
float higth_win=700;
float diffuse[] = { 1.0, 1.0, 1.0, 1.0 };
float pos[] = {width_win/2, higth_win/2, higth_win/2, 1.0 };// (x, y, z) задают вектор направления, а компонента w всегда равна нулю (иначе источник превратится в точечный).
float angel = 0.0;
float rot = 0.0;
float scal = 0.0;




void Draw(){



	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
float size = 50;
glEnable(GL_DEPTH_TEST );//проверка глубины

//голова верх
glPushMatrix();
glColor3f(0.0, 1.0, 0.0);
glScalef(1.0, 1.1, 1.0);
glutSolidSphere(50, 30, 30);
glPopMatrix();


//повязка
glPushMatrix();
glColor3f(1.0, 0.0, 0.0);
glTranslatef(0, 7, 0);

glBegin(GL_LINES);
for (float alpha = 0; alpha <= 360; alpha += 0.001)
{
	glVertex3f(45.0*sin(alpha), 20.0, 45.0*cos(alpha));
	glVertex3f(51.0*sin(alpha), 0.0, 51.0*cos(alpha));
	
}
glEnd();
glPopMatrix();

//голова низ
glPushMatrix();
glColor3f(0.0, 1.0, 0.0);
glTranslatef(0, -16, 8);
glScalef(1.3, 0.8, 1.1);
glutSolidSphere(50, 50, 50);
glPopMatrix();


//рот
glPushMatrix();
glColor3f(46.0/255.0, 121.0/255.0, 22.0/255.0);
glRotatef(-20, 1.0, 0.0, 0.0);
glTranslatef(0, -39, 20);
glScalef(1.2, 0.2, 0.9);
glutSolidSphere(40, 30, 30);
glPopMatrix();

glPushMatrix();
glColor3f(46.0 / 255.0, 121.0 / 255.0, 22.0 / 255.0);
glRotatef(20, 1.0, 0.0, 0.0);
glTranslatef(0, -9, 35);
glScalef(1.2, 0.2, 0.9);
glutSolidSphere(40, 30, 30);
glPopMatrix();


//правый глаз
glPushMatrix();
glColor3f(1.0, 1.0, 1.0);
glTranslatef(15, 13, size-10);
glScalef(1.5, 1.0, 1.0);
glutSolidSphere(10, 30, 30);
glPopMatrix();

glPushMatrix();
glColor3f(0.0, 0.0, 0.0);
glTranslatef(15, 13, size-6.8 );
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
		scal+=0.01;
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
	glClearColor(180.0/255.0, 243.0/255.0, 246.0/255.0, 1.0);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	//перспектива
	glOrtho(-width_win/2, width_win/2, -higth_win/2, higth_win/2, -higth_win/2, higth_win/2);
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