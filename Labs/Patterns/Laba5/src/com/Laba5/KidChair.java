package com.Laba5;

public class KidChair extends AbstractChair {

    public KidChair(String MATERIAL_TYPE, boolean BACK) {
        super(MATERIAL_TYPE, BACK);
    }

    @Override
    public String GetMaterial() {
        return MATERIAL_TYPE;
    }

    @Override
    public boolean GetBack() {
        return BACK;
    }
}
