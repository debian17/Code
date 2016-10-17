package com.company;

public class Main {

    public static void main(String[] args) {

        Store store = new Store();
        new Producer(store).start();
        new Consumer(store).start();

        new Thread(new Producer(store)).start();

        
    }
}
