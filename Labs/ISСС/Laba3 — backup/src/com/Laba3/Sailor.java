package com.Laba3;

public class Sailor extends Solder {

    public Sailor(String Name){
        super(Name);
    }

    @Override
    public int GetCost() {
        return 10;
    }

    @Override
    public void JustDoIt(){
        System.out.println("Матрос "+Name+" выполняет приказ.");
    }
}
