package com.company;


public class Komissar implements IObserver {

    String Name;
    IObservable RecruitmentOffice;

    public Komissar(String Name, IObservable RO){
        this.Name=Name;
        RecruitmentOffice = RO;
        RO.AddObserver(this);
    }

    @Override
    public void Upadate(Object obj) {
        boolean CallOfDuty = (boolean)obj;
        if(CallOfDuty){
            System.out.println("Коммисар "+Name+": да начнется охота за призывниками!");
            Hunt();
        }
        else {
            System.out.println("Коммисар "+Name+": охота окончена.");
        }
    }

    void Hunt(){
        System.out.println("Коммисар "+Name+": I will hunt you down...");
    }
}
