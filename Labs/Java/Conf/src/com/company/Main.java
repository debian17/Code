package com.company;

public class Main {

    public static void main(String[] args) {
        Account a = new Account(1000);
        Account b = new Account(2000);

        Runnable rn = new Runnable() {
            @Override
            public void run() {

            }
        };

        new Thread(rn).start();


    }
}
