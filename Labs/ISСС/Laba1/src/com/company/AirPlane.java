package com.company;


public abstract class AirPlane implements IAerobatics, IAutoPilot {

    public void Display(){}
    public void Fly(){}

    @Override
    public void AutoPilotOn() throws Exception {}

    @Override
    public  void AutoPilotOff() throws Exception {}

    @Override
    public void Aerobatics(){}

}
