package com.Laba3;

public abstract class SolderDecorator extends Solder {

    protected Solder solder;

    public SolderDecorator(String Name, Solder solder){
        super(Name);
        this.solder=solder;
    }
}
