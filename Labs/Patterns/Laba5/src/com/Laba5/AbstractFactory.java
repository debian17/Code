package com.Laba5;

public abstract class AbstractFactory {

    public String Name;
    public AbstractFactory(String Name){
        this.Name=Name;
    }

    public abstract AbstractTube CreateBigTube(int LENGTH);
    public abstract AbstractTube CreateLittleTube(int LENGTH);

    public abstract AbstractChair CreateManChair(boolean BACK);
    public abstract AbstractChair CreateKidChair(boolean BACK);
}
