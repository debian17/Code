package com.Laba5;

public class LittleTube extends AbstractTube {

    public LittleTube(String MATERIAL_TYPE, int LENGTH) {
        super(MATERIAL_TYPE, LENGTH);
    }

    @Override
    public String GetMaterial() {
        return MATERIAL_TYPE;
    }

    @Override
    public int GetLength() {
        return LENGTH;
    }
}
