#include <iostream>
#include <math.h>
#include <fstream>

using namespace std;

const int nx = 50;
const int ny = 50;
const double pi =3.14;
const int ro = 1000;

    double hx = 1/(double)nx;
    double hy = 1/(double)ny;
    double ht = 0.1;
    int lt = 10;
    int mu=2;
    double sigma = 0.5;

    const double e = 0.0000001;
    double o[ny*nx];

    double *k = (new double [ny*nx]);
    double **U = &k;

    double *q = (new double [ny*nx]);
    double **V = &q;

    double *w = (new double [ny*nx]);
    double **P = &w;

    fstream f;

void zeidel (int start, int finish, double *B1, double *B2, double *B3, double *B4, double* F, double *A, double **C) {

    double temp;
    int m0, m1, m2, m3, m4;

    do {
        temp = 0;

        for (int i=start; i<finish; i++)
        for (int j=start; j<finish; j++)
        {
            m0 = i + j * nx;
            m1 = m0+1;
            m2 = m0-1;
            m3 = m0+nx;
            m4 = m0-nx;

            double buf = (*C) [m0];
            //c[i][j] = (F[m0] + B1[m0]*c[i][j] + B2[m0]*c[i+1][j] + B3[m0]*c[i-1][j] + B4[m0]*c[i][j+1])/A[m0];
            (*C)[m0] = (F[m0] + B1[m0] * ((*C) [m1]) + B2[m0] * ((*C) [m2]) + B3[m0] * ((*C)[m3]) + B4[m0] * ((*C)[m4])) / A[m0];
            buf = fabs (buf - ((*C)[m0]));
            if (buf > temp) temp = buf;
        }

    } while (temp >= e);
}


void funcUV () {

    double q0, q1, q2, q3, q4;
    int m0, m1, m2, m3, m4, m24;

    //double b1, b2, b3, b4;

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
    double FU[ny*nx];
    double FV[ny*nx];

    for (int i=0; i<(ny*nx); i++){
        (*U)[i]= 0;
        (*V)[i]= 0;
        o[i] = 1;
    }

    for (int i=0; i<nx; i++) {
        (*U)[i+1+nx] = 1;
    }

    for (int i=0; i<nx; i++) {
        o[i] = 0;
        o[i+(ny-1)*nx] = 0;
        o[i+(ny-2)*nx] = 0;
        o[i*nx] = 0;
        o[i*nx+ny-1] = 0;
        o[i*nx+ny-2] = 0;
    }

    for (int i=0; i<nx*ny; i++) {
        cout << o[i];
        if ((i+1) % nx == 0)
            cout << endl;
    }

        for (int i=2; i<(ny-2); i++)
        for (int j=2; j<(nx-2); j++) {
            m0 = i + j * nx;
            m1 = m0+1;
            m2 = m0-1;
            m3 = m0+nx;
            m4 = m0-nx;
            m24 = m0-1-nx;

            q1 = (o[m0]+o[m4])/2;
            q2 = (o[m2]+o[m24])/2;
            q3 = (o[m0]+o[m2])/2;
            q4 = (o[m4]+o[m24])/2;
            q0 = (q1+q2)/2;

            /*b1 = (-u/(2*hx) + mu/(hx*hx))*q1;
            b2 = (u/(2*hx) + mu/(hx*hx))*q2;
            b3 = (-v/(2*hy) + mu/(hy*hy))*q3;
            b4 = (v/(2*hy) + mu/(hy*hy))*q4;*/

            B1[m0] = q1 * (-((*U)[m1] + (*U)[m0]) / (4 * hx) + mu / (hx * hx));
            B2[m0] = q2 * ( ((*U)[m2] + (*U)[m0]) / (4 * hx) + mu / (hx * hx));
            B3[m0] = q3 * (-((*V)[m3] + (*V)[m0]) / (4 * hy) + mu / (hy * hy));
            B4[m0] = q4 * ( ((*V)[m4] + (*V)[m0]) / (4 * hy) + mu / (hy * hy));

            B1[m0] = sigma*B1[m0];
            B2[m0] = sigma*B2[m0];
            B3[m0] = sigma*B3[m0];
            B4[m0] = sigma*B4[m0];

            B6[m0] = (1-sigma)*B1[m0];
            B7[m0] = (1-sigma)*B2[m0];
            B8[m0] = (1-sigma)*B3[m0];
            B9[m0] = (1-sigma)*B4[m0];


            A[m0] = q0/ht+B1[m0]+B2[m0]+B3[m0]+B4[m0];
            B5[m0] = q0/ht - B6[m0] - B7[m0] - B8[m0] - B9[m0];

            //F[m0] = B5[m0]*c[i][j] + B6[m0]*c[i+1][j] + B7[m0]*c[i-1][j] + B8[m0]*c[i][j+1] + B9[m0]*c[i][j-1];
            //F[m0] = B5[m0] * ((*Z)[m0]) + B6[m0] * ((*Z)[m1]) + B7[m0] * ((*Z)[m2]) + B8[m0] * ((*Z)[m3]) + B9[m0] * ((*Z)[m4]);
            FU[m0] = B5[m0] * ((*U)[m0]) + B6[m0] * ((*U)[m1]) + B7[m0] * ((*U)[m2]) + B8[m0] * ((*U)[m3]) + B9[m0] * ((*U)[m4]);
            FV[m0] = B5[m0] * ((*V)[m0]) + B6[m0] * ((*V)[m1]) + B7[m0] * ((*V)[m2]) + B8[m0] * ((*V)[m3]) + B9[m0] * ((*V)[m4]);
        }
        zeidel (2, nx-2, B1, B2, B3, B4, FU, A, U);
        zeidel (2, nx-2, B1, B2, B3, B4, FV, A, V);


    int l;

    f.open("u.prn", ios::out | ios::trunc);
    l = 1;
    while (l <= nx*ny) {
        f << (*U)[l] << " ";
        //if ((l % nx == 0) && (l != 0))
        if ((l % nx == 0))
            f << endl;
        l++;
    }
    f.close();


    f.open("v.prn", ios::out | ios::trunc);
    l = 1;
    while (l <= nx*ny) {
        f << (*V)[l] << " ";
        //if ((l % nx == 0) && (l != 0))
        if ((l % nx == 0))
            f << endl;
        l++;
    }
    f.close();
}


void funcP () {

    double q0, q1, q2, q3, q4;
    int m0, m1, m2, m3, m4, m24;

    double B1[ny*nx];
    double B2[ny*nx];
    double B3[ny*nx];
    double B4[ny*nx];

    double A[ny*nx];
    double F[ny*nx];

    for (int i=0; i<(ny*nx); i++){
        (*P)[i]= 0;
    }

        for (int i=1; i<(ny-1); i++)
        for (int j=1; j<(nx-1); j++) {
            m0 = i + j * nx;
            m1 = m0+1;
            m2 = m0-1;
            m3 = m0+nx;
            m4 = m0-nx;
            m24 = m0-1-nx;

            q1 = (o[m0]+o[m4])/2;
            q2 = (o[m2]+o[m24])/2;
            q3 = (o[m0]+o[m2])/2;
            q4 = (o[m4]+o[m24])/2;
            q0 = (q1+q2)/2;

            B1[m0] = q1 / (hx * hx);
            B2[m0] = q2 / (hx * hx);
            B3[m0] = q3 / (hy * hy);
            B4[m0] = q4 / (hy * hy);

            A[m0] = B1[m0]+B2[m0]+B3[m0]+B4[m0];

            F[m0] = - ro * ( (q1*((*U)[m1]+(*U)[m0])/2 - q2*((*U)[m0]+(*U)[m2])/2) / hx + (q3*((*V)[m3]+(*V)[m0])/2 - q4*((*V)[m0]+(*V)[m4])/2) / hy) / ht;
        }
        zeidel (1, nx-1, B1, B2, B3, B4, F, A, P);



    int l;

    f.open("p.prn", ios::out | ios::trunc);
    l = 0;
    while (l < nx*ny) {
        f << (*P)[l] << " ";
        //if ((l % nx == 0) && (l != 0))
        if (((l+1) % nx == 0))
            f << endl;
        l++;
    }
    f.close();
}


void funcCorrect () {

    int m0, m1, m2, m3, m4, m24;
    double q0, q1, q2, q3, q4;

    for (int i=2; i<(ny-2); i++)
    for (int j=2; j<(nx-2); j++) {
        m0 = i + j * nx;
        m1 = m0+1;
        m2 = m0-1;
        m3 = m0+nx;
        m4 = m0-nx;
        m24 = m0-1-nx;

        q1 = (o[m0]+o[m4])/2;
        q2 = (o[m2]+o[m24])/2;
        q3 = (o[m0]+o[m2])/2;
        q4 = (o[m4]+o[m24])/2;
        q0 = (q1+q2)/2;

        (*U)[m0] = (*U)[m0] - ht*(q1*((*P)[m1] - (*P)[m0])/(2*hx) + q2*((*P)[m0] - (*P)[m2])/(2*hx)) / (ro*q0);
        (*V)[m0] = (*V)[m0] - ht*(q3*((*P)[m3] - (*P)[m0])/(2*hy) + q4*((*P)[m0] - (*P)[m4])/(2*hy)) / (ro*q0);
    }

    int l;

    f.open("u.prn", ios::out | ios::trunc);
    l = 1;
    while (l <= nx*ny) {
        f << (*U)[l] << " ";
        //if ((l % nx == 0) && (l != 0))
        if ((l % nx == 0))
            f << endl;
        l++;
    }
    f.close();


    f.open("v.prn", ios::out | ios::trunc);
    l = 1;
    while (l <= nx*ny) {
        f << (*V)[l] << " ";
        //if ((l % nx == 0) && (l != 0))
        if ((l % nx == 0))
            f << endl;
        l++;
    }
    f.close();
}


int main()
{
    int o[ny*nx];
    //double c[ny][nx]

    cout << "Hello world!" << endl;

    double t = 0;
    do{
        funcUV();
        funcP();
        funcCorrect();
        t += ht;
    } while (t <= lt);

    return 0;
}
