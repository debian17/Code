package com.Laba3;

public class Pilot extends Solder {

    public Pilot(String Property){
      super(Property);
    }

    @Override
    public int GetCost() {
        return 12;
    }

    @Override
    public void JustDoIt(){
        System.out.println("Пилот "+Property+" выполняет приказ.");
    }
}
