package com.company;

public class Account implements Runnable {

    public  void run(){

    }
    private  int balance;

    Account(int InitBalance){
        balance=InitBalance;
    }

    public void Plus(int amount){
        balance+=amount;
    }

    public void Minus(int amount){
        balance-=amount;
    }
    public int getBalance(){
        return  balance;
    }
}
