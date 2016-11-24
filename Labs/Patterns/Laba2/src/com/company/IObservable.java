package com.company;


public interface IObservable {
    void AddObserver(IObserver O);
    void RemoveObserver(IObserver O);
    void NotifyObservers() throws InterruptedException;
}
