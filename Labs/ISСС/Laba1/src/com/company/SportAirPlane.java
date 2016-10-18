package com.company;


public class SportAirPlane extends AirPlane{

    public SportAirPlane(){
        Aerobatics = new Bochka();
        AutoPilot = new IAutoPilotClass();
    }

    @Override
    public void Display() {
        System.out.println("Отображается спортивный самолет.");
    }

    @Override
    public void Fly(){
        System.out.println("Летит спортивный самолет.");
    }


}
