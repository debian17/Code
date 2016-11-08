package com.Laba3;

public abstract class Solder {

    public String Name;

    public Solder(String Name) {
        this.Name= Name;
    }

    public abstract int GetCost();

    public abstract void JustDoIt();
}
