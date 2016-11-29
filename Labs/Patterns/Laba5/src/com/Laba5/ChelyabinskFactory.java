package com.Laba5;

public class ChelyabinskFactory extends AbstractFactory {

    private String MATERIAL_TYPE = "Чугун";

    public ChelyabinskFactory(String Name) {
        super(Name);
    }

    @Override
    public AbstractTube CreateBigTube(int LENGTH) {
        return new BigTube(MATERIAL_TYPE,LENGTH);
    }

    @Override
    public AbstractTube CreateLittleTube(int LENGTH) {
        return new LittleTube(MATERIAL_TYPE,LENGTH);
    }

    @Override
    public AbstractChair CreateManChair(boolean BACK) {
        return new ManChair(MATERIAL_TYPE, BACK);
    }

    @Override
    public AbstractChair CreateKidChair(boolean BACK) {
        return new KidChair(MATERIAL_TYPE, BACK);
    }

}
