package com.Laba5;

import java.io.FileWriter;
import java.io.IOException;

public class Main {

    static int N = 100;
    static double LX = 100;
    static double LT = 100;
    static double HT = 0.1;
    static double T = LT / HT;
    static double HX = 1;
    static double K = 1;
    static double SIGMA = 0.5;
    static double[] c = new double[N];
    static double[] Z = new double[N];

    static void DoIt(){
        double[] a = new double[N];
        double[] b = new double[N];
        double[] f = new double[N];
        double[] ALFA = new double[N];
        double[] BETA = new double[N];

        for(int j=0; j<T;j++){
            for( int i=1; i<N-1;i++){
                c[i] = (1/HT) + (K/(HX*HX));
                a[i] = K/2 * (HX*HX);
                b[i] = K/2 * (HX*HX);
                f[i] = (Z[i]/HT) + (K/2) * ((Z[i+1] - 2 *Z[i] + Z[i-1])/(HX*HX));
            }
            c[0] = 1/HT +(2*SIGMA/(HX*HX)) + (0.4* SIGMA/HX);
            a[0] = 0;
            b[0] = 2*SIGMA/(HX*HX);
            b[N-1] = 0;
            f[0] = (Z[0]/HT) + 2*(1-SIGMA) * (Z[1]-Z[0]/(HX*HX)) + ((0.4*(1-SIGMA)*Z[0]-0.4)/HX);
            ALFA[0] = 0;
            BETA[0] = 0;
            for(int i=0; i<N-1;i++){
                ALFA[i+1] = b[i]/(c[i] - a[i]*ALFA[i]);
                BETA[i+1] = (f[i] + a[i]*BETA[i]) / (c[i] - a[i]*ALFA[i]);
            }
            Z[N-1] = 1 - Math.cos(Math.PI * K * HT/100);

            int l = N-1;
            while (l>0){
                Z[l-1] = BETA[l] + ALFA[l]*Z[l];
                l--;
            }
        }
    }


    public static void main(String[] args) {

        for(int i=0; i<N;i++){
            Z[i] = 0;
        }
        for(int i=40; i<60;i++){
            Z[i] = 1;
        }

        try{
            FileWriter fw = new FileWriter("1.prn");

            for(int i = 0; i<N;i++){
                String buf = Double.toString(Z[i]);
                fw.write(buf);
                fw.write("\r\n");
            }
            fw.flush();
            fw.close();
        }
        catch (IOException e){
            e.getMessage();
        }


        DoIt();


        try{
            FileWriter fw = new FileWriter("2.prn");

            for(int i = 0; i<N;i++){
                String buf = Double.toString(Z[i]);
                fw.write(buf);
                fw.write("\r\n");
            }
            fw.flush();
            fw.close();
        }
        catch (IOException e){
            e.getMessage();
        }



    }
}
