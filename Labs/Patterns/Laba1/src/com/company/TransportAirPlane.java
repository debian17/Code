package com.company;

public class TransportAirPlane extends AirPlane {

    public TransportAirPlane(){
        Aerobatics = new Bochka();
        AutoPilot = new IAutoPilotClass();
    }

    @Override
    public void Display() {
        System.out.println("Отображается транспортный самолет.");
    }

    @Override
    public void Fly(){
        System.out.println("Летит транспортный самолет.");
    }

}
