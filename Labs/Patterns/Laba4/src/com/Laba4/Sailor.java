package com.Laba4;

public class Sailor extends Solder {

    private String NameOfShip;

    public Sailor(String Name, String Rank) {
        super(Name, Rank);
    }

    public void SetNameOfShip(String NameOfShip){
        this.NameOfShip=NameOfShip;
    }

    public String GetNameOfShip(){
        return NameOfShip;
    }

    @Override
    public void Serve() {
        System.out.println("Моряк "+Name+" в звании "+Rank+" несет службу.");
    }

    public void Swim(){
        System.out.println("Матрос "+Name+" в звании "+Rank+" плывет на корабле.");
    }
}
