package com.Laba5;

public abstract class AbstractChair {

    protected String MATERIAL_TYPE;
    protected boolean BACK;

    public AbstractChair(String MATERIAL_TYPE, boolean BACK){
        this.MATERIAL_TYPE=MATERIAL_TYPE;
        this.BACK=BACK;
    }

    public abstract String GetMaterial();
    public abstract boolean GetBack();

}
