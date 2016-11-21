package com.Laba4;

public class Solder implements ISolder {
    public String Name;
    public String Rank;
    public Solder(String Name, String Rank){
        this.Name=Name;
        this.Rank=Rank;
    }

    @Override
    public void Serve() {

    }
}
