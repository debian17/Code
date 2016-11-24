package com.Laba5;

public abstract class AbstractTube {

    protected String MATERIAL_TYPE;
    protected int LENGTH;

    public AbstractTube(String MATERIAL_TYPE, int LENGTH){
        this.MATERIAL_TYPE=MATERIAL_TYPE;
        this.LENGTH=LENGTH;
    }

    public abstract String GetMaterial();
    public abstract int GetLength();
}
