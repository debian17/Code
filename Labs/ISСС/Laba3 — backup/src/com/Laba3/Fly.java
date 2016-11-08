package com.Laba3;

public class Fly extends SolderDecorator {

    public Fly(Solder solder){
        super(solder.Name +" летает;",solder);
    }

    @Override
    public int GetCost() {
        return solder.GetCost()+5;
    }

    public void FlyIt(){
        System.out.println("Я лечу.");
    }

    @Override
    public void JustDoIt() {
    }

}
