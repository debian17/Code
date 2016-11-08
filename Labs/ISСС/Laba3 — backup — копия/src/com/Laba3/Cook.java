package com.Laba3;

public class Cook extends SolderDecorator {

    public Cook(Solder solder){
      super(solder.Property +" готовит;",solder);
    }

    @Override
    public int GetCost() {
        return solder.GetCost()+2;
    }

    public void CookIt(){
        System.out.println("Я готовлю.");
    }

    @Override
    public void JustDoIt() {
    }
}
