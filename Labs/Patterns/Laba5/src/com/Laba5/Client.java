package com.Laba5;

public class Client {

    private AbstractFactory CF;
    private AbstractFactory OF;

    public Client(AbstractFactory CF, AbstractFactory OF){
        this.CF = CF;
        this.OF = OF;
    }

    public AbstractChair GetManChair(int From, boolean BACK){
        if(From==0){
            return CF.CreateManChair(BACK);
        }
        else {
            return OF.CreateManChair(BACK);
        }
    }

    public AbstractChair GetKidChair(int From, boolean BACK){
        if(From==0){
            return CF.CreateKidChair(BACK);
        }
        else {
            return OF.CreateKidChair(BACK);
        }
    }

    public AbstractTube GetBigTube(int From, int LENGTH){
        if (From == 0) {
            return CF.CreateBigTube(LENGTH);
        }
        else {
            return OF.CreateBigTube(LENGTH);
        }
    }

    public AbstractTube GetLittleTube(int From, int LENGTH){
        if (From == 0) {
            return CF.CreateLittleTube(LENGTH);
        }
        else {
            return OF.CreateLittleTube(LENGTH);
        }
    }

}
