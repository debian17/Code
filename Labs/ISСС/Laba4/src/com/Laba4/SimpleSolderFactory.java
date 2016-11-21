package com.Laba4;

public class SimpleSolderFactory {
    public static final int TYPE_PILOT=0;
    public static final int TYPE_SAILOR=1;
    public static final int TYPE_INFANTRYMAN=2;

    public ISolder Create(int TYPE, String Name, String Rank){
        switch (TYPE){
            case TYPE_PILOT:{
                return new Pilot(Name,Rank);
            }
            case TYPE_SAILOR:{
                return new Sailor(Name,Rank);
            }
            case TYPE_INFANTRYMAN:{
                return new Infantryman(Name,Rank);
            }
            default:{
                return null;
            }
        }
    }
}
