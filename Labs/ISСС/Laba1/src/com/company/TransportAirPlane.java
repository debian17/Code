package com.company;

public class TransportAirPlane extends AirPlane {

    @Override
    public void Display() {
        System.out.println("Отображается транспортный самолет.");
    }

    @Override
    public void Fly(){
        System.out.println("Летит транспортный самолет.");
    }

    @Override
    public void AutoPilotOn(){
        System.out.println("Включен автопилот транспортного самолета.");
    }

    @Override
    public void AutoPilotOff(){
        System.out.println("Выключен автопилот транспортного самолета.");
    }

    @Override
    public void Aerobatics(){
        System.out.println(" Сомнительное выполнение фигуры высшего пилотажа для транспортного самолета.");
    }
}
