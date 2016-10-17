package com.company;

import java.util.concurrent.locks.ReentrantLock;

public class SecondClass extends MyThread{

    SecondClass(ReentrantLock locker){
        super(locker);
    }

    @Override
    public void run(){
        try{
            System.out.println("Поток y начал работать!");
            locker.lock();
            for(int i=0;i<4;i++){
                Main.y++;
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
