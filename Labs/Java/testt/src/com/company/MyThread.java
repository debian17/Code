package com.company;

import java.util.concurrent.locks.ReentrantLock;

public class MyThread implements Runnable {

    protected ReentrantLock locker;

    MyThread(ReentrantLock locker){
        this.locker=locker;
    }

    public void run(){
        try{
            System.out.println("Поток x начал работать!");
            locker.lock();
            for(int i=0;i<4;i++){
                Main.x++;
                Thread.sleep(10);
            }
        }
        catch (InterruptedException e){
            System.out.println("Что-то полшлоа не так!");
        }
        finally {
            locker.unlock();
        }
    }
}
