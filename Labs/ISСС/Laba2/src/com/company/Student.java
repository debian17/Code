package com.company;

import java.util.Random;

public class Student implements IObserver {

    public String Name;
    private boolean inArm;
    IObservable RecruitmentOffice;

    public Student(String Name, IObservable RO){
        this.Name=Name;
        RecruitmentOffice = RO;
        RO.AddObserver(this);
    }

    @Override
    public void Upadate(Object obj) throws InterruptedException {

        boolean CallOfDuty = (boolean)obj;
        if(CallOfDuty){
            Thread.sleep(150);
            Random r = new Random(System.currentTimeMillis());
            int p = r.nextInt(2);

            if(p==0){
                Study();
            }
            else{
                Hide();
            }
        }
        else {
            System.out.println("Студент "+Name+":вот теперь можно расслабиться!");
        }
    }

    public void Study(){
        System.out.println("Студент "+Name+":начинаю активно сдавать сессию!");
    }

    public void Hide(){
        System.out.println("Студент "+Name+":скрылся от призыва у бабушки в деревне.");
    }
}
