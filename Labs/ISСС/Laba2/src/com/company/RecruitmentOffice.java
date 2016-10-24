package com.company;

import java.util.ArrayList;

public class RecruitmentOffice implements IObservable {

    private boolean CallOfDuty;

    ArrayList<IObserver> observers;
    
    public RecruitmentOffice(){
        observers = new ArrayList<IObserver>();
        CallOfDuty=false;
    }

    public void Change_State() throws InterruptedException {
        CallOfDuty=!CallOfDuty;
        NotifyObservers();
    }

    @Override
    public void AddObserver(IObserver O) {
        observers.add(O);
    }

    @Override
    public void RemoveObserver(IObserver O) {
        observers.remove(O);
    }

    @Override
    public void NotifyObservers() throws InterruptedException {
        if(CallOfDuty){
            System.out.println("Военкомат: начался призыв!");
        }
        else{
            System.out.println("Военкомат: призыв окончен!");
        }
        for(IObserver o : observers){
            o.Upadate(CallOfDuty);
        }
    }
}
