package com.Laba4;

public class Infantryman extends Solder {

    private int NMU;

    public Infantryman(String Name, String Rank) {
        super(Name, Rank);
    }

    public void SetNMU(int NMU){
        this.NMU=NMU;
    }

    public int GetNMU(){
        return NMU;
    }

    @Override
    public void Serve() {
        System.out.println("Пехотинец "+Name+" в звании "+Rank+" несет службу.");
    }

    public void Attack(){
        System.out.println("Пехотинец "+Name+" в звании "+Rank+" атакует врага.");
    }
}
