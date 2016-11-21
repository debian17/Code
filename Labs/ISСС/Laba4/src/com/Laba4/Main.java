package com.Laba4;

public class Main {

    public static void main(String[] args) {
        SimpleSolderFactory Army = new SimpleSolderFactory();

        ISolder s = Army.Create(SimpleSolderFactory.TYPE_INFANTRYMAN,"asd","asdds");
        System.out.println(s.getClass());


        /*Solder[] solders = new Solder[3];

        try{
            solders[0] = Army.Create(SimpleSolderFactory.TYPE_INFANTRYMAN,"Райн","Рядовой");
            solders[1] = Army.Create(SimpleSolderFactory.TYPE_PILOT,"Джек","Капитан");
            solders[2] = Army.Create(SimpleSolderFactory.TYPE_SAILOR,"Попай","Матрос");
        }
        catch (Exception e){
            e.getMessage();
        }

        for(int i=0;i<3;i++){
            solders[i].Serve();
        }*/

    }
}
