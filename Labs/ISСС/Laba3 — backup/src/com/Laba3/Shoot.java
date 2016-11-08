package com.Laba3;

public class Shoot extends SolderDecorator {

    public Shoot(Solder solder){
        super(solder.Name +" стреляет;",solder);
    }

    @Override
    public int GetCost() {
        return solder.GetCost()+4;
    }

    public void ShootIt(){
        System.out.println("Я стреляю.");
    }

    @Override
    public void JustDoIt() {
    }
}
