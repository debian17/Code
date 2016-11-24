package com.Laba3;


public class Infantryman extends Solder {

    public Infantryman(String Property){
        super(Property);
    }

    @Override
    public int GetCost() {
        return 8;
    }

    @Override
    public void JustDoIt(){
        System.out.println("Пехотинец "+Property+" выполняет приказ.");
    }
}
