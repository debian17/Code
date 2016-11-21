package com.Laba4;

public class Pilot extends Solder {

    private String SquadronName;

    public Pilot(String Name, String Rank) {
        super(Name, Rank);
    }

    public void SetSquadronName(String SquadronName){
        this.SquadronName=SquadronName;
    }

    public String GetSquadronName(){
        return  SquadronName;
    }

    @Override
    public void Serve() {
        System.out.println("Пилот "+Name+" в звании "+Rank+" несет службу.");
    }

    public void Fly(){
        System.out.println("Пилот "+Name+" в звании "+Rank+" выполняет боевой вылет.");
    }
}
