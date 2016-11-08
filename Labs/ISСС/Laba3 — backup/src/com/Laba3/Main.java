package com.Laba3;

public class Main {

    public static void main(String[] args) {

        Solder solder1 = new Pilot("Василий");
        solder1 = new Fly(solder1);
        solder1 = new Cook(solder1);
        System.out.println(solder1.Name+" Cost="+solder1.GetCost());

        Solder solder2 = new Sailor("Петр");
        solder2 = new Swim(solder2);
        solder2 = new Cook(solder2);
        solder2 = new Shoot(solder2);
        System.out.println(solder2.Name+" Cost="+solder2.GetCost());

        Solder solder3 = new Infantryman("Иннокентий");
        solder3 = new Shoot(solder3);
        System.out.println(solder3.Name+" Cost="+solder3.GetCost());

        Solder terminator = new Infantryman("Арнольд");
        terminator = new Shoot(terminator);
        terminator = new Cook(terminator);
        terminator = new Swim(terminator);
        terminator = new Fly(terminator);
        System.out.println(terminator.Name+" Cost="+terminator.GetCost());
    }
}
