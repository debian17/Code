package com.company;


public interface IObserver {
    void Update(Object obj) throws InterruptedException;
}
