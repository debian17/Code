package com.company;
import java.util.concurrent.locks.ReentrantLock;

public class Main {

    public  static int x;
    public  static  int y;

    public static void main(String[] args) throws InterruptedException {
        ReentrantLock locker= new ReentrantLock();
        for(int i=0;i<2;i++){
            Thread t = new Thread(new MyThread(locker));
            t.setName("Поток "+i);
            t.start();
            t.join();
        }
        for(int i=0;i<2;i++){
            Thread t = new Thread(new SecondClass(locker));
            t.setName("Поток "+i);
            t.start();
            t.join();
        }

        System.out.println("Результат = "+x);
        System.out.println("Результат = "+y);
    }
}
