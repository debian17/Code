package com.Laba3;

public abstract class Solder {

    public String Property;

    public Solder(String Property) {
        this.Property= Property;
    }

    public abstract int GetCost();

    public abstract void JustDoIt();
}
