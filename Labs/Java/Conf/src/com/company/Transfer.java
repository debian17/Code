package com.company;

public class Transfer {


    static  void transfer(Account acc1, Account acc2, int amount){
        if(acc1.getBalance()<amount){
            System.out.print("Недостаточно средств для перевода.");
        }
        else{
            acc1.Minus(amount);
            acc2.Plus(amount);
        }
    }
}
