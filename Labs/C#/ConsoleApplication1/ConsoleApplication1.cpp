#include <iostream>
#include <math.h>
#include <fstream>

using namespace std;

const int nx = 100;
const int ny = 100;
const double pi = 3.14;
int u = 3;
int v = 4;
double hx = 1;
double hy = 1;
double ht = 0.1;
int lt = 10;
int mu = 2;
int sigma = 0.5;

const double e = 0.0000001;

int o[ny*nx];
double c[ny][nx];

double q0, q1, q2, q3, q4;
int m0, m1, m2, m3, m4, m24;

double b1, b2, b3, b4;

double B1[ny*nx];
double B2[ny*nx];
double B3[ny*nx];
double B4[ny*nx];
double B5[ny*nx];
double B6[ny*nx];
double B7[ny*nx];
double B8[ny*nx];
double B9[ny*nx];

double A[ny*nx];
double F[ny*nx];
double Z[ny*nx];

void zeidel() {

	double temp;

	do {
		temp = 0;

		for (int i = 1; i<(ny - 1); i++)
			for (int j = 1; j<(nx - 1); j++)
			{
				m0 = i + j * nx;
				m1 = m0 + 1;
				m2 = m0 - 1;
				m3 = m0 + nx;
				m4 = m0 - nx;

				double buf = Z[m0];
				//c[i][j] = (F[m0] + B1[m0]*c[i][j] + B2[m0]*c[i+1][j] + B3[m0]*c[i-1][j] + B4[m0]*c[i][j+1])/A[m0];
				Z[m0] = (F[m0] + B1[m0] * Z[m1] + B2[m0] * Z[m2] + B3[m0] * Z[m3] + B4[m0] * Z[m4]) / A[m0];
				buf = fabs(buf - Z[m0]);
				if (buf > temp) temp = buf;
			}
		cout << temp << endl;

	} while (temp >= e);
}


int main()
{
	cout << "Hello world!" << endl;

	for (int i = 0; i<ny; i++)
		for (int j = 0; j<nx; j++) {
			o[i*nx + j] = 1;
		}

	for (int i = 1; i<(ny - 1); i++)
		for (int j = 1; j<(nx - 1); j++)
			if ((10 <= j) && (j <= 20) && (10 <= i) && (i <= 20))
				c[i][j] = (sin(pi*(j - 10) / 10))*sin(pi*(i - 10) / 10);
			else
				c[i][j] = 0;

	fstream f;
	f.open("1.prn", ios::out | ios::trunc);
	for (int i = 1; i<(ny - 1); i++) {
		for (int j = 1; j<(nx - 1); j++)
			f << c[i][j] << " ";
		f << endl;
	}

	f.close();

	int l = 0;
	for (int i = 0; i < nx; i++)
	{
		for (int j = 0; j < ny; j++)
		{
			Z[l] = c[i][j];
			l++;
		}
	}

	double t = 0;
	do {
		for (int i = 1; i<(ny - 1); i++)
			for (int j = 1; j<(nx - 1); j++) {
				m0 = i + j * nx;
				m1 = m0 + 1;
				m2 = m0 - 1;
				m3 = m0 + nx;
				m4 = m0 - nx;
				m24 = m0 - 1 - nx;

				q1 = (o[m0] + o[m4]) / 2;
				q2 = (o[m2] + o[m24]) / 2;
				q3 = (o[m0] + o[m2]) / 2;
				q4 = (o[m4] + o[m24]) / 2;
				q0 = (q1 + q2) / 2;

				b1 = (-u / (2 * hx) + mu / (hx*hx))*q1;
				b2 = (u / (2 * hx) + mu / (hx*hx))*q2;
				b3 = (-v / (2 * hy) + mu / (hy*hy))*q3;
				b4 = (v / (2 * hy) + mu / (hy*hy))*q4;



				B1[m0] = sigma*b1;
				B2[m0] = sigma*b2;
				B3[m0] = sigma*b3;
				B4[m0] = sigma*b4;

				B6[m0] = (1 - sigma)*b1;
				B7[m0] = (1 - sigma)*b2;
				B8[m0] = (1 - sigma)*b3;
				B9[m0] = (1 - sigma)*b4;

				A[m0] = q0 / ht + B1[m0] + B2[m0] + B3[m0] + B4[m0];
				B5[m0] = q0 / ht - B6[m0] - B7[m0] - B8[m0] - B9[m0];

				//F[m0] = B5[m0]*c[i][j] + B6[m0]*c[i+1][j] + B7[m0]*c[i-1][j] + B8[m0]*c[i][j+1] + B9[m0]*c[i][j-1];
				F[m0] = B5[m0] * Z[m0] + B6[m0] * Z[m1] + B7[m0] * Z[m2] + B8[m0] * Z[m3] + B9[m0] * Z[m4];
			}
		zeidel();
		t += ht;
	} while (t <= lt);

	f.open("2.prn", ios::out | ios::trunc);
	l = 0;
	while (l <= nx*ny) {
		f << Z[l] << " ";
		//if ((l % nx == 0) && (l != 0))
		if ((l % nx == 0))
			f << endl;
		l++;

	}

	f.close();

	return 0;
}
