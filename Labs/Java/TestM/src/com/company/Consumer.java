package com.company;

import static java.lang.Thread.sleep;

public class Consumer extends Thread {

    Store store;
    int product=0;

    Consumer(Store store)
    {
        this.store=store;
    }

    public void run() {
        try
        {
            while(product<5){

                product=product+store.get();
                System.out.println ("Потребитель купил " + product + " товар(ов)");
                sleep(100);
            }
        }
        catch(InterruptedException e)
        {
            System.out.println ("поток потребителя прерван");
        }
    }
}
