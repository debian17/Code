package com.Laba6;

import java.io.FileWriter;
import java.io.IOException;

public class Main {

    static int NX = 20;
    static int NY = 20;
    static int N = NX * NY;
    static int HX = 1;
    static int HY = 1;
    static int LT = 10;
    static double HT = 1;
    static double DENSITY = 1;
    static double SIGMA = 0.5;
    static double E = 0.000000001;
    static double[] OCCUPANCY_ARR = new double[N];
    static double[] MU = new double[N];
    static double[] ARR_U = new double[N];
    static double[] ARR_V = new double[N];
    static double[] B_ARRAY_1 = new double[N];
    static double[] B_ARRAY_2 = new double[N];
    static double[] B_ARRAY_3 = new double[N];
    static double[] B_ARRAY_4 = new double[N];
    static double[] B_ARRAY_5 = new double[N];
    static double[] B_ARRAY_6 = new double[N];
    static double[] B_ARRAY_7 = new double[N];
    static double[] B_ARRAY_8 = new double[N];
    static double[] B_ARRAY_9 = new double[N];
    static double[] ACOEFFICIENT = new double[N];
    static double[] FCOEFFICIENT = new double[N];
    static double[] FHCOEFFICIENT = new double[N];
    static double[] FWCOEFFICIENT = new double[N];
    static double[] ARR_P = new double[N];

    static public double[] METHOD_ZEIDEL(int START_INDEX, int END_INDEX, double[] ACOEFFICIENT, double[] B_ARRAY_1, double[] B_ARRAY_2,
                                         double[] B_ARRAY_3, double[] B_ARRAY_4, double[] ARRUPV, double[] FCOEFFICIENT, double E) {
        double MAXIMUM = 1;
        while (MAXIMUM >= E) {
            MAXIMUM = 0;
            for (int i = START_INDEX; i < END_INDEX; i++) {
                for (int j = START_INDEX; j < END_INDEX; j++) {
                    int i0 = i + j * NX;
                    int i1 = i0 + 1;
                    int i2 = i0 - 1;
                    int i3 = i0 + NX;
                    int i4 = i0 - NX;
                    double TEMP = ARRUPV[i0];
                    ARRUPV[i0] = (FCOEFFICIENT[i0] + B_ARRAY_1[i0] * ARRUPV[i1] + B_ARRAY_2[i0] * ARRUPV[i2] + B_ARRAY_3[i0] * ARRUPV[i3] + B_ARRAY_4[i0] * ARRUPV[i4]) / ACOEFFICIENT[i0];
                    TEMP = Math.abs(TEMP - ARRUPV[i0]);
                    if (TEMP > MAXIMUM) {
                        MAXIMUM = TEMP;
                    }
                }
            }
        }
        ;
        return ARRUPV;
    }

    static void METHOD1() {
        for (int i = 2; i < NX - 2; i++) {
            for (int j = 2; j < NY - 2; j++) {
                int i0 = i + j * NX;
                int i1 = i0 + 1;
                int i2 = i0 - 1;
                int i3 = i0 + NX;
                int i4 = i0 - NX;
                int i5 = i0 - 1 - NX;
                double QCOEFFICIENT1 = (OCCUPANCY_ARR[i0] + OCCUPANCY_ARR[i4]) / 2;
                double QCOEFFICIENT2 = (OCCUPANCY_ARR[i2] + OCCUPANCY_ARR[i5]) / 2;
                double QCOEFFICIENT3 = (OCCUPANCY_ARR[i0] + OCCUPANCY_ARR[i2]) / 2;
                double QCOEFFICIENT4 = (OCCUPANCY_ARR[i4] + OCCUPANCY_ARR[i5]) / 2;
                double QCOEFFICIENT_0 = (QCOEFFICIENT1 + QCOEFFICIENT2) / 2;
                B_ARRAY_1[i0] = QCOEFFICIENT1 * (-(ARR_U[i1] + ARR_U[i0]) / (HX * 4) + (MU[i0] + MU[i1]) / (HX * HX * 2));
                B_ARRAY_2[i0] = QCOEFFICIENT2 * ((ARR_U[i2] + ARR_U[i0]) / (HX * 4) + (MU[i0] + MU[i2]) / (HX * HX * 2));
                B_ARRAY_3[i0] = QCOEFFICIENT3 * (-(ARR_V[i3] + ARR_V[i0]) / (HY * 4) + (MU[i0] + MU[i3]) / (HY * HY * 2));
                B_ARRAY_4[i0] = QCOEFFICIENT4 * ((ARR_V[i4] + ARR_V[i0]) / (HY * 4) + (MU[i0] + MU[i4]) / (HY * HY * 2));
                B_ARRAY_6[i0] = B_ARRAY_1[i0] * (1 - SIGMA);
                B_ARRAY_7[i0] = B_ARRAY_2[i0] * (1 - SIGMA);
                B_ARRAY_7[i0] = B_ARRAY_3[i0] * (1 - SIGMA);
                B_ARRAY_9[i0] = B_ARRAY_4[i0] * (1 - SIGMA);
                B_ARRAY_1[i0] *= SIGMA;
                B_ARRAY_2[i0] *= SIGMA;
                B_ARRAY_3[i0] *= SIGMA;
                B_ARRAY_4[i0] *= SIGMA;
                ACOEFFICIENT[i0] = QCOEFFICIENT_0 / HT + B_ARRAY_1[i0] + B_ARRAY_2[i0] + B_ARRAY_3[i0] + B_ARRAY_4[i0];
                B_ARRAY_5[i0] = QCOEFFICIENT_0 / HT - B_ARRAY_6[i0] - B_ARRAY_7[i0] - B_ARRAY_8[i0] - B_ARRAY_9[i0];
                FHCOEFFICIENT[i0] = B_ARRAY_5[i0] * ARR_U[i0] +
                        B_ARRAY_6[i0] * ARR_U[i1] +
                        B_ARRAY_7[i0] * ARR_U[i2] +
                        B_ARRAY_8[i0] * ARR_U[i3] +
                        B_ARRAY_9[i0] * ARR_U[i4];
                FWCOEFFICIENT[i0] = B_ARRAY_5[i0] * ARR_V[i0] +
                        B_ARRAY_6[i0] * ARR_V[i1] +
                        B_ARRAY_7[i0] * ARR_V[i2] +
                        B_ARRAY_8[i0] * ARR_V[i3] +
                        B_ARRAY_9[i0] * ARR_V[i4];
            }
        }
        ARR_U = METHOD_ZEIDEL(2, NX - 2, ACOEFFICIENT, B_ARRAY_1, B_ARRAY_2, B_ARRAY_3, B_ARRAY_4, ARR_U, FHCOEFFICIENT, E);
        ARR_V = METHOD_ZEIDEL(2, NX - 2, ACOEFFICIENT, B_ARRAY_1, B_ARRAY_2, B_ARRAY_3, B_ARRAY_4, ARR_V, FWCOEFFICIENT, E);
    }

    static void METHOD2() {
        for (int i = 1; i < NX - 1; i++) {
            for (int j = 1; j < NY - 1; j++) {
                int i0 = i + j * NX;
                int i1 = i0 + 1;
                int i2 = i0 - 1;
                int i3 = i0 + NX;
                int i4 = i0 - NX;
                int i5 = i0 - 1 - NX;
                double QCOEFFICIENT1 = (OCCUPANCY_ARR[i0] + OCCUPANCY_ARR[i4]) / 2;
                double QCOEFFICIENT2 = (OCCUPANCY_ARR[i2] + OCCUPANCY_ARR[i5]) / 2;
                double QCOEFFICIENT3 = (OCCUPANCY_ARR[i0] + OCCUPANCY_ARR[i2]) / 2;
                double QCOEFFICIENT4 = (OCCUPANCY_ARR[i4] + OCCUPANCY_ARR[i5]) / 2;
                B_ARRAY_1[i0] = QCOEFFICIENT1 / (HX * HX);
                B_ARRAY_2[i0] = QCOEFFICIENT2 / (HX * HX);
                B_ARRAY_3[i0] = QCOEFFICIENT3 / (HY * HY);
                B_ARRAY_4[i0] = QCOEFFICIENT4 / (HY * HY);
                ACOEFFICIENT[i0] = B_ARRAY_1[i0] + B_ARRAY_2[i0] + B_ARRAY_3[i0] + B_ARRAY_4[i0];
                FCOEFFICIENT[i0] = (-DENSITY / HT) * ((QCOEFFICIENT1 * (ARR_U[i0] + ARR_U[i1]) - QCOEFFICIENT2 * (ARR_U[i0] + ARR_U[i2])) /
                        (HX * 2) + (QCOEFFICIENT3 * (ARR_V[i0] + ARR_V[i3]) - QCOEFFICIENT4 * (ARR_V[i0] + ARR_V[i4])) / (HY * 2));
            }
        }
        ARR_P = METHOD_ZEIDEL(1, NX - 1, ACOEFFICIENT, B_ARRAY_1, B_ARRAY_2, B_ARRAY_3, B_ARRAY_4, ARR_P, FCOEFFICIENT, E);
    }

    static void METHOD3() {
        for (int i = 2; i < NX - 2; i++) {
            for (int j = 2; j < NY - 2; j++) {
                int i0 = i + j * NX;
                int i1 = i0 + 1;
                int i2 = i0 - 1;
                int i3 = i0 + NX;
                int i4 = i0 - NX;
                int i5 = i0 - 1 - NX;

                double QCOEFFICIENT1 = (OCCUPANCY_ARR[i0] + OCCUPANCY_ARR[i4]) / 2;
                double QCOEFFICIENT2 = (OCCUPANCY_ARR[i2] + OCCUPANCY_ARR[i5]) / 2;
                double QCOEFFICIENT3 = (OCCUPANCY_ARR[i0] + OCCUPANCY_ARR[i2]) / 2;
                double QCOEFFICIENT4 = (OCCUPANCY_ARR[i4] + OCCUPANCY_ARR[i5]) / 2;
                double QCOEFFICIENT_0 = (QCOEFFICIENT1 + QCOEFFICIENT2) / 2;

                ARR_U[i0] = ARR_U[i0] - (HT / (DENSITY * QCOEFFICIENT_0)) * (QCOEFFICIENT1 * (ARR_P[i1] - ARR_P[i0]) / (HX * 2) + QCOEFFICIENT2 * (ARR_P[i0] - ARR_P[i2]) / (HX * 2));
                ARR_V[i0] = ARR_V[i0] - (HT / (DENSITY * QCOEFFICIENT_0)) * (QCOEFFICIENT3 * (ARR_P[i3] - ARR_P[i0]) / (HY * 2) + QCOEFFICIENT4 * (ARR_P[i0] - ARR_P[i4]) / (HY * 2));
            }
        }
    }

    public static void main(String[] args) {

        for (int k = 0; k < N; k++) {
            MU[k] = 2;
            OCCUPANCY_ARR[k] = 0;
            ARR_U[k] = 0;
        }

        int j = 1;
        for (int i = 1; i < NX - 2; i++) {
            ARR_U[i + j * NX] = 1;
        }

        for (int i = 1; i < NX - 2; i++) {
            for (j = 1; j < NY - 2; j++) {
                OCCUPANCY_ARR[i + j * NX] = 1;
            }
        }

        for (int k = 0; k < N; k++) {
            MU[k] = 2;
        }

        double T = 0;
        while (T < LT) {
            METHOD1();
            METHOD2();
            METHOD3();
            T += HT;
        }
        ;

        int l = 0;
        try {
            FileWriter fw = new FileWriter("RESULT_U.prn", false);
            while (l < ARR_U.length) {
                double buf_i = ARR_U[l];
                int d_i = 0;
                d_i = (int) buf_i;
                String buf = Double.toString(buf_i);
                fw.write(buf);
                fw.write(" ");
                l++;
                if (l % NX == 0) {
                    fw.write("\r\n");
                }
            }
            fw.flush();
            fw.close();
        } catch (IOException e) {
            e.getMessage();
        }

        l = 0;
        try {
            FileWriter fw = new FileWriter("RESULT_V.prn", false);
            while (l < ARR_U.length) {
                double buf_i = ARR_V[l];
                int d_i = 0;
                d_i = (int) buf_i;
                String buf = Double.toString(buf_i);
                //System.out.println(ARR_U[l]+"   "+buf);
                fw.write(buf);
                fw.write(" ");
                l++;
                if (l % NX == 0) {
                    fw.write("\r\n");
                }
            }
            fw.flush();
            fw.close();
        } catch (IOException e) {
            e.getMessage();
        }

        l = 0;
        try {
            FileWriter fw = new FileWriter("RESULT_P.prn", false);
            while (l < ARR_U.length) {
                double buf_i = ARR_P[l];
                int d_i = 0;
                d_i = (int) buf_i;
                String buf = Double.toString(buf_i);
                fw.write(buf);
                fw.write(" ");
                l++;
                if (l % NX == 0) {
                    fw.write("\r\n");
                }
            }
            fw.flush();
            fw.close();
        } catch (IOException e) {
            e.getMessage();
        }
    }
}
