package com.Laba3;

public abstract class SolderDecorator extends Solder {

    protected Solder solder;

    public SolderDecorator(String Property, Solder solder){
        super(Property);
        this.solder=solder;
    }
}
