package com.company;


public class Doctor implements IObserver {

    String Name;
    IObservable RecruitmentOffice;

    public Doctor(String Name, IObservable RO){
        this.Name=Name;
        RecruitmentOffice = RO;
        RO.AddObserver(this);
    }

    @Override
    public void Upadate(Object obj) {
        boolean CallOfDuty = (boolean)obj;
        if(CallOfDuty){
            System.out.println("Доктор "+Name+": время лечить призывиков!");
            Heal();
        }
        else {
            System.out.println("Доктор "+Name+": все здоровы. Лечение окончено.");
        }
    }

    void Heal(){
        System.out.println("Доктор "+Name+": прикладывает подорожник к призывнику.");
    }
}
