package com.company;

public class PaperAirPlane extends AirPlane {

    public PaperAirPlane(){
        Aerobatics = new Petlya();
    }

    @Override
    public void Display() {
        System.out.println("Отображается бумажный самолет.");
    }

    @Override
    public void Fly(){
        System.out.println("Летит бумажный самолет.");
    }


}
