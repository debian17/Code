package com.company;

public class PaperAirPlane extends AirPlane {

    @Override
    public void Display() {
        System.out.println("Отображается бумажный самолет.");
    }

    @Override
    public void Fly(){
        System.out.println("Летит бумажный самолет.");
    }

    @Override
    public void AutoPilotOn() throws Exception{
        throw new Exception("Нет автопилота!");
    }

    @Override
    public void AutoPilotOff() throws  Exception{
        throw new Exception("Нет автопилота!");
    }

    @Override
    public void Aerobatics(){
        System.out.println("Выполнение фигуры высшего пилотажа для бумажного самолета.");
    }
}
