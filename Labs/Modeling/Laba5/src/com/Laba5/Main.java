package com.Laba5;

import java.io.FileWriter;
import java.io.IOException;

public class Main {

    static final int NX=100;
    static final int NY=100;
    static final double PI=3.14;
    static final double E = 0.0000001;

    static int U = 3;
    static int V = 4;
    static double HX=1;
    static double HY=1;
    static double HT=0.1;
    static int LT=10;
    static int MU = 2;
    static double SIGMA = 0.5;

    static int[] O = new int[NX*NY];
    static double[][] C = new double[NY][NX];

    static double Q0;
    static double Q1;
    static double Q2;
    static double Q3;
    static double Q4;
    static int M0;
    static int M1;
    static int M2;
    static int M3;
    static int M4;
    static int M24;
    static double Bm1;
    static double Bm2;
    static double Bm3;
    static double Bm4;

    static double[] B1 = new double[NX*NY];
    static double[] B2 = new double[NX*NY];
    static double[] B3 = new double[NX*NY];
    static double[] B4 = new double[NX*NY];
    static double[] B5 = new double[NX*NY];
    static double[] B6 = new double[NX*NY];
    static double[] B7 = new double[NX*NY];
    static double[] B8 = new double[NX*NY];
    static double[] B9 = new double[NX*NY];
    static double[] A = new double[NX*NY];
    static double[] F = new double[NX*NY];
    static double[] Z = new double[NX*NY];



    static void ZEIDEL(){
        double TEMP;
        do{
            TEMP=0;
            for(int i=1;i<NX-2;i++){
                for (int j=1;j<NY-2;j++){
                    M0 = i+j*NX;
                    M1 = M0+1;
                    M2 = M0-1;
                    M3 = M0+NX;
                    M4 = M0 - NX;
                    double BUFFER = Z[M0];
                    Z[M0] = (F[M0]+ B1[M0]*Z[M1]+B2[M0]*Z[M2]+B3[M0]*Z[M3]+B4[M0]*Z[M4])/A[M0];
                    BUFFER = Math.abs(BUFFER - Z[M0]);
                    if(BUFFER>TEMP){
                        TEMP=BUFFER;
                    }
                }
            }
        }while (TEMP>=E);
    }


    public static void main(String[] args) {

        for(int i=0;i<NY;i++){
            for(int j=0;j<NX;j++){
                O[i*NX+j]=1;
            }
        }

        for(int i=0;i<NX;i++){
            for(int j=0;j<NY;j++){
                if((10<=j) && (j<=20) && (10<=i) && (i<=20)){
                    C[i][j]=(Math.sin(PI*(j-10)/10)) * (Math.sin(PI*(i-10)/10));
                }
                else {
                    C[i][j] = 0;
                }
            }
        }


        try {
            FileWriter fw = new FileWriter("1.prn",false);
            for(int i=0;i<NX;i++){
                for(int j=0;j<NY;j++){
                    String buf = Double.toString(C[i][j]);
                    if(C[i][j]==0){
                        fw.write("0");
                        fw.write(" ");
                    }
                    else{
                        fw.write(buf);
                        fw.write(" ");
                    }
                }
                fw.append("\r\n");
            }
            fw.flush();
            fw.close();
        }
        catch (IOException e) {
                e.getMessage();
        }


        int l = 0;
        for(int i=0;i<NX;i++){
            for(int j=0;j<NY;j++){
                Z[l]=C[i][j];
                l++;
            }
        }


        double T = 0;
        do {
            for(int i=1;i<NX-2;i++){
                for(int j=1;j<NY-2;j++){
                    M0 = i+j*NX;
                    M1 = M0 +1;
                    M2 = M0 -1;
                    M3 = M0 + NX;
                    M4 = M0 -NX;
                    M24 = M0 - NX - 1;

                    Q1 = (O[M0]+O[M4])/2;
                    Q2 = (O[M2]+O[M24])/2;
                    Q3 = (O[M0]+O[M2])/2;
                    Q4 = (O[M4]+O[M24])/2;
                    Q0 = (Q1 + Q2)/2;

                    /*Bm1 = (-U/(HX*2) + MU/(HX*HX))*Q1;
                    Bm2 = (U /(HX*2) + MU/(HX*HX))*Q2;
                    Bm3 = (-V /(HY*2) + MU/(HY*HY))*Q3;
                    Bm4 = (V /(HY*2) + MU/(HY*HY))*Q4;*/

                    B1[M0] = Q1 * (-(U*2))/(HX*4) + (MU*2)/(HX*HX*2);
                    B2[M0] = Q2 * ((U*2))/(HX*4) + (MU*2)/(HX*HX*2);
                    B3[M0] = Q3 * (-(V*2))/(HY*4) + (MU*2)/(HY*HY*2);
                    B4[M0] = Q4 * ((V*2))/(HY*4) + (MU*2)/(HY*HY*2);

                    B6[M0] = B1[M0] * (1-SIGMA);
                    B7[M0] = B2[M0] * (1-SIGMA);
                    B8[M0] = B3[M0] * (1-SIGMA);
                    B9[M0] = B4[M0] * (1-SIGMA);

                    B1[M0] = B1[M0]*SIGMA;
                    B2[M0] = B2[M0]*SIGMA;
                    B3[M0] = B3[M0]*SIGMA;
                    B4[M0] = B4[M0]*SIGMA;


                    A[M0] = Q0/HT + B1[M0] + B2[M0] + B3[M0] + B4[M0];
                    B5[M0] = Q0/HT - B6[M0] - B7[M0] - B8[M0] - B9[M0];

                    F[M0] = B5[M0]*Z[M0] + B6[M0]*Z[M1] + B7[M0]*Z[M2] + B8[M0]*Z[M3] + B9[M0]*Z[M4];
                }
            }
            ZEIDEL();
            T+=HT;
        }while (T<=LT);

        l=0;
        try{
            FileWriter fw = new FileWriter("2.prn", false);
            while (l<NX*NY){
                String buf = Double.toString(Z[l]);
                if(Z[l]==0){
                    fw.write("0");
                    fw.write("  ");
                }
                else{
                    fw.write(buf);
                    fw.write("  ");
                }
                if(l % NX == 0){
                    fw.write("\r\n");
                }
                l++;
            }
            fw.flush();
            fw.close();
        }
        catch (IOException e){
            e.getMessage();
        }
    }
}
