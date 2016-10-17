package com.company;


public class SportAirPlane extends AirPlane{

    @Override
    public void Display() {
        System.out.println("Отображается спортивный самолет.");
    }

    @Override
    public void Fly(){
        System.out.println("Летит спортивный самолет.");
    }

    @Override
    public void AutoPilotOn(){
        System.out.println("Включен автопилот спортивного самолета.");
    }

    @Override
    public void AutoPilotOff(){
        System.out.println("Выключен автопилот спортивного самолета.");
    }

    @Override
    public void Aerobatics(){
        System.out.println("Выполнение фигуры высшего пилотажа для спортивного самолета.");
    }
}
