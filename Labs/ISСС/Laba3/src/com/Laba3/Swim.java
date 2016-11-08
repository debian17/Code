package com.Laba3;

public class Swim extends SolderDecorator {

    public Swim(Solder solder){
        super(solder.Property +" плавает;",solder);
    }

    @Override
    public int GetCost() {
        return solder.GetCost()+1;
    }

    public void SwimIt(){
        System.out.println("Я плыву.");
    }

    @Override
    public void JustDoIt() {
    }
}
