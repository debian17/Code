package com.company;

public class Store {

    int counter = 0;
    final int N = 5;

    synchronized int put() {
        if(counter<=N)
        {
            counter++;
            System.out.println ("склад имеет " + counter + " товар(ов)");
            return 1;
        }
        return 0;
    }

    synchronized int get() {
        if(counter>0)
        {
            counter--;
            System.out.println ("склад имеет " + counter + " товар(ов)");
            return 1;
        }
        return 0;
    }
}
