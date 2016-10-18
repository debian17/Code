package com.company;


public class IAutoPilotClass implements IAutoPilot {

    @Override
    public void AutoPilotOn(){
        System.out.println("Включен автопилот.");
    }

    @Override
    public void AutoPilotOff(){
        System.out.println("Выключен автопилот.");
    }
}
