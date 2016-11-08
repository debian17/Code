package com.Laba3;

public class Sailor extends Solder {

    public Sailor(String Property){
        super(Property);
    }

    @Override
    public int GetCost() {
        return 10;
    }

    @Override
    public void JustDoIt(){
        System.out.println("Матрос "+Property+" выполняет приказ.");
    }
}
