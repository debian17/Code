using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lab_5
{
    class Program
    {
        
        static public int Nx=100;
        static public int Ny = 100;
        static public int N=Nx*Ny;
        static public int hx=1;
        static public int hy = 1;
        static public int lt = 10;
        static public double ht = 0.1;
        static public double sigma = 0.5;
        static public double pogr = 0.00000001;

        static public void Show(double[] mas)
        {
            for (int i = 1000; i < 2000; i++)
            {
                if(i % 10!= 0)
                    Console.Write("{0,-6:0.##}", mas[i]);
                else
                    Console.WriteLine("{0,-6:0.##}", mas[i]);
            }
        }


        static public double[] zeidel(double[] A, double[] B1, double[] B2, double[] B3, double[] B4, double[] C, double[] F, double pogr)
        {
            
            double max=2*pogr;

            do
            {
                max = 0;
                for (int i = 1; i < Nx - 2; i++)
                {
                    for (int j = 1; j < Ny - 2; j++)
                    {
                        var m0 = i + j * Nx;
                        var m1 = m0 + 1;
                        var m2 = m0 - 1;
                        var m3 = m0 + Nx;
                        var m4 = m0 - Nx;
                        double w = C[m0];
                        C[m0] = (F[m0] + B1[m0] * C[m1] + B2[m0] * C[m2] + B3[m0] * C[m3] + B4[m0] * C[m4]) / A[m0];
                        w = Math.Abs(w - C[m0]);
                        if (w > max) max = w;
                    }
                }
            } while (max >= pogr);

            return C;
        }


        static void Main(string[] args)
        {
            double [] O=new double[N];
            double[] v = new double[N];
            double[] u = new double[N];
            double[] mu = new double[N];
            double[,] Cn = new double[Nx, Ny];
            double[] C = new double[N];
            double[] B1 = new double[N];
            double[] B2 = new double[N];
            double[] B3 = new double[N];
            double[] B4 = new double[N];
            double[] B5 = new double[N];
            double[] B6 = new double[N];
            double[] B7 = new double[N];
            double[] B8 = new double[N];
            double[] B9 = new double[N];
            double[] A = new double[N];
            double[] F = new double[N];

            for (int k = 0; k < N; k++)
            {
                O[k] = 1;
                u[k] = 3;
                v[k] = 4;
                mu[k] = 2;
            }


                for (int i = 0; i < Nx; i++)
                {
                    for (int j = 0; j < Ny; j++)
                    {
                        
                        Cn[i, j] = 0;
                    }

                }


            for (int i = 10; i <= 20; i++)
            {
                for (int j = 10; j <= 20; j++)
                { 
                 Cn[i, j] = Math.Sin(3.14 * (i - 10) / 10) * Math.Sin(3.14 * (j - 10) / 10);
                }
            }
            int l = 0;
            for (int i = 0; i < Nx; i++)
            {
                for (int j = 0; j < Ny; j++)
                {

                    C[l]=Cn[i, j];
                    l++;
                }

            }
            var f = 0;
            using (var ss = new StreamWriter("nach.txt"))
                foreach (var x in C)
                {
                    ss.Write(x + "  ");
                    f++;
                    if (f == 100)
                    {
                        ss.WriteLine();
                        f = 0;
                    }
                }


         //  Show(Cnn);
          double t = 0;  
           do{
            for (int i = 1; i < Nx-2; i++)
            {
                for (int j = 1; j < Ny-2; j++)
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



                    B1[m0] = q1 * (-(u[m1] + u[m0]) / (4 * hx) + (mu[m1] + mu[m0]) / (2 * hx * hx));
                    B2[m0] = q2 * ( (u[m2] + u[m0]) / (4 * hx) + (mu[m2] + mu[m0]) / (2 * hx * hx));
                    B3[m0] = q3 * (-(v[m3] + v[m0]) / (4 * hy) + (mu[m3] + mu[m0]) / (2 * hy * hy));
                    B4[m0] = q4 * ( (v[m4] + v[m0]) / (4 * hy) + (mu[m4] + mu[m0]) / (2 * hy * hy));
                    
                    B6[m0] = (1 - sigma) * B1[m0];
                    B7[m0] = (1 - sigma) * B2[m0];
                    B8[m0] = (1 - sigma) * B3[m0];
                    B9[m0] = (1 - sigma) * B4[m0];

                    B1[m0] = sigma * B1[m0];
                    B2[m0] = sigma * B2[m0];
                    B3[m0] = sigma * B3[m0];
                    B4[m0] = sigma * B4[m0];


                    A[m0] =  q0 / ht + B1[m0] + B2[m0] + B3[m0] + B4[m0];
                    B5[m0] = q0 / ht - B6[m0] - B7[m0] - B8[m0] - B9[m0];

                    F[m0] = B5[m0] * C[m0] + B6[m0] * C[m1] + B7[m0] * C[m2] + B8[m0] * C[m3] + B9[m0] * C[m4];
                                      
                }               
            }
            C=zeidel(A, B1, B2, B3, B4, C, F, pogr);
            t=t+ht; 
           }while(t<lt);

           using (var sw = new StreamWriter("result.txt"))
               foreach (var x in C) sw.WriteLine(x);
            var e=0;
           using (var ss = new StreamWriter("result1.txt"))
               foreach (var x in C)
               {
                   ss.Write(x + "  ");
                   e++;
                   if (e == 100)
                   {
                       ss.WriteLine();
                       e = 0;
                   }
               }

           // Console.ReadKey();
        }
    }
}
