package com.Laba5;

public class Main {

    public static void main(String[] args) {

        ChelyabinskFactory CF = new ChelyabinskFactory("Челябинский_METAL");
        OmskFactory OF = new OmskFactory("Омский_ГазМяс");

        Client c = new Client(CF,OF);

        AbstractChair ac = c.GetManChair(0,true);
        System.out.println("Метариал="+ac.GetMaterial()+" Спинка="+ac.BACK);

        AbstractTube at = c.GetBigTube(1,100);
        System.out.println("Метариал="+at.GetMaterial()+" Длина="+at.LENGTH);


    }
}
