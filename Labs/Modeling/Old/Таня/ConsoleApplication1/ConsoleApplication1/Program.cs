using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lab_6
{
    class Program
    {
        static public int Nx = 20;
        static public int Ny = 20;
        static public int N = Nx * Ny;
        static public int hx = 1;
        static public int hy = 1;
        static public int lt = 10;
        static public double ht = 1;
        static public double ro = 0.9982;
        static public double sigma = 0.5;
        static public double pogr = 0.000000001;
        static double[] O = new double[N];
        static double[] v = new double[N];
        static double[] u = new double[N];
        static double[] mu = new double[N];
        static double[,] Un = new double[Nx, Ny];

        static double[] U = new double[N];
        static double[] V = new double[N];
        static double[] B1 = new double[N];
        static double[] B2 = new double[N];
        static double[] B3 = new double[N];
        static double[] B4 = new double[N];
        static double[] B5 = new double[N];
        static double[] B6 = new double[N];
        static double[] B7 = new double[N];
        static double[] B8 = new double[N];
        static double[] B9 = new double[N];
        static double[] A = new double[N];
        static double[] F = new double[N];
        static double[] Fu = new double[N];
        static double[] Fv = new double[N];
        static double[] P = new double[N];


        static public double[] zeidel(int np, int vp, double[] A, double[] B1, double[] B2, double[] B3, double[] B4, double[] UV, double[] F, double pogr)
        {
            double max = 2 * pogr;

            do
            {
                max = 0;
                for (int i = np; i < vp; i++)
                {
                    for (int j = np; j < vp; j++)
                    {
                        var m0 = i + j * Nx;
                        var m1 = m0 + 1;
                        var m2 = m0 - 1;
                        var m3 = m0 + Nx;
                        var m4 = m0 - Nx;
                        double w = UV[m0];
                        UV[m0] = (F[m0] + B1[m0] * UV[m1] + B2[m0] * UV[m2] + B3[m0] * UV[m3] + B4[m0] * UV[m4]) / A[m0];
                        w = Math.Abs(w - UV[m0]);
                        if (w > max) max = w;
                    }
                }
            } while (max >= pogr);

            return UV;
        }

        static public void func1()
        {
            for (int i = 2; i < Nx - 2; i++)
            {
                for (int j = 2; j < Ny - 2; j++)
                {
                    var m0 = i + j * Nx;
                    var m1 = m0 + 1;
                    var m2 = m0 - 1;
                    var m3 = m0 + Nx;
                    var m4 = m0 - Nx;
                    var m24 = m0 - 1 - Nx;


                    var q1 = (O[m0] + O[m4]) / 2;
                    var q2 = (O[m2] + O[m24]) / 2;
                    var q3 = (O[m0] + O[m2]) / 2;
                    var q4 = (O[m4] + O[m24]) / 2;
                    var q0 = (q1 + q2) / 2;



                    B1[m0] = q1 * (-(U[m1] + U[m0]) / (4 * hx) + (mu[m1] + mu[m0]) / (2 * hx * hx));
                    B2[m0] = q2 * ((U[m2] + U[m0]) / (4 * hx) + (mu[m2] + mu[m0]) / (2 * hx * hx));
                    B3[m0] = q3 * (-(V[m3] + V[m0]) / (4 * hy) + (mu[m3] + mu[m0]) / (2 * hy * hy));
                    B4[m0] = q4 * ((V[m4] + V[m0]) / (4 * hy) + (mu[m4] + mu[m0]) / (2 * hy * hy));

                    B6[m0] = (1 - sigma) * B1[m0];
                    B7[m0] = (1 - sigma) * B2[m0];
                    B8[m0] = (1 - sigma) * B3[m0];
                    B9[m0] = (1 - sigma) * B4[m0];

                    B1[m0] = sigma * B1[m0];
                    B2[m0] = sigma * B2[m0];
                    B3[m0] = sigma * B3[m0];
                    B4[m0] = sigma * B4[m0];


                    A[m0] = q0 / ht + B1[m0] + B2[m0] + B3[m0] + B4[m0];
                    B5[m0] = q0 / ht - B6[m0] - B7[m0] - B8[m0] - B9[m0];

                    Fu[m0] = B5[m0] * U[m0] + B6[m0] * U[m1] + B7[m0] * U[m2] + B8[m0] * U[m3] + B9[m0] * U[m4];
                    Fv[m0] = B5[m0] * V[m0] + B6[m0] * V[m1] + B7[m0] * V[m2] + B8[m0] * V[m3] + B9[m0] * V[m4];
                }
            }
            U = zeidel(2, Nx - 2, A, B1, B2, B3, B4, U, Fu, pogr);
            V = zeidel(2, Nx - 2, A, B1, B2, B3, B4, V, Fv, pogr);
        }

        static public void func2()
        {
            for (int i = 1; i < Nx - 1; i++)
            {
                for (int j = 1; j < Ny - 1; j++)
                {
                    var m0 = i + j * Nx;
                    var m1 = m0 + 1;
                    var m2 = m0 - 1;
                    var m3 = m0 + Nx;
                    var m4 = m0 - Nx;
                    var m24 = m0 - 1 - Nx;


                    var q1 = (O[m0] + O[m4]) / 2;
                    var q2 = (O[m2] + O[m24]) / 2;
                    var q3 = (O[m0] + O[m2]) / 2;
                    var q4 = (O[m4] + O[m24]) / 2;
                    var q0 = (q1 + q2) / 2;



                    B1[m0] = q1 / (hx * hx);
                    B2[m0] = q2 / (hx * hx);
                    B3[m0] = q3 / (hy * hy);
                    B4[m0] = q4 / (hy * hy);

                    A[m0] = B1[m0] + B2[m0] + B3[m0] + B4[m0];
                    F[m0] = (-ro / ht) * ((q1 * (U[m1] + U[m0]) - q2 * (U[m2] + U[m0])) / (2 * hx) + (q3 * (V[m3] + V[m0]) - q4 * (V[m4] + V[m0])) / (2 * hy));

                }
            }
            P = zeidel(1, Nx - 1, A, B1, B2, B3, B4, P, F, pogr);

        }

        static public void func3()
        {
            for (int i = 2; i < Nx - 2; i++)
            {
                for (int j = 2; j < Ny - 2; j++)
                {
                    var m0 = i + j * Nx;
                    var m1 = m0 + 1;
                    var m2 = m0 - 1;
                    var m3 = m0 + Nx;
                    var m4 = m0 - Nx;
                    var m24 = m0 - 1 - Nx;


                    var q1 = (O[m0] + O[m4]) / 2;
                    var q2 = (O[m2] + O[m24]) / 2;
                    var q3 = (O[m0] + O[m2]) / 2;
                    var q4 = (O[m4] + O[m24]) / 2;
                    var q0 = (q1 + q2) / 2;

                    U[m0] = U[m0] - (ht / (ro * q0)) * (q1 * (P[m1] - P[m0]) / (2 * hx) + q2 * (P[m0] - P[m2]) / (2 * hx));
                    V[m0] = V[m0] - (ht / (ro * q0)) * (q3 * (P[m3] - P[m0]) / (2 * hy) + q4 * (P[m0] - P[m4]) / (2 * hy));
                }
            }
        }

        static void Main(string[] args)
        {
            for (int k = 0; k < N; k++)
            {


                u[k] = 3;
                v[k] = 4;
                mu[k] = 2;
                O[k] = 0;
                U[k] = 0;

            }
            int j = 1;
            for (int i = 1; i < Nx - 2; i++)
            {
                U[i + j * Nx] = 1;
            }


            for (int i = 1; i < Nx - 2; i++)
            {
                for (j = 1; j < Ny - 2; j++)
                {
                    O[i + j * Nx] = 1;
                }
            }


            for (int k = 0; k < N; k++)
            {


                u[k] = 3;
                v[k] = 4;
                mu[k] = 2;

            }
            double t = 0;
            do
            {
                func1();
                func2();
                func3();
                t = t + ht;
            } while (t < lt);


            var f = 0;
            using (var ss = new StreamWriter("u.txt"))
                foreach (var x in U)
                {
                    ss.Write(Convert.ToInt32(x * 100000) + "  ");
                    f++;
                    if (f == Nx)
                    {
                        ss.WriteLine();
                        f = 0;
                    }
                }
            f = 0;
            using (var ss = new StreamWriter("v.txt"))
                foreach (var x in V)
                {
                    ss.Write(Convert.ToInt32(x * 100000) + "  ");
                    f++;
                    if (f == Nx)
                    {
                        ss.WriteLine();
                        f = 0;
                    }
                }
            f = 0;
            using (var ss = new StreamWriter("p.txt"))
                foreach (var x in P)
                {
                    ss.Write(Convert.ToInt32(x * 100000) + "  ");
                    f++;
                    if (f == Nx)
                    {
                        ss.WriteLine();
                        f = 0;
                    }
                }



            Console.ReadKey();
        }
    }
}
