package com.Laba3;

public class Pilot extends Solder {

    public Pilot(String Name){
      super(Name);
    }

    @Override
    public int GetCost() {
        return 12;
    }

    @Override
    public void JustDoIt(){
        System.out.println("Пилот "+Name+" выполняет приказ.");
    }
}
