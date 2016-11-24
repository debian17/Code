package com.company;

public class Main {

    public static void main(String[] args) throws InterruptedException {

        RecruitmentOffice voenkomat= new RecruitmentOffice();

        Student s1 = new Student("Вася",voenkomat);
        Student s2 = new Student("Петя",voenkomat);
        Student s3 = new Student("Вова",voenkomat);

        Komissar k = new Komissar("Каттани",voenkomat);

        Doctor d = new Doctor("Гиппократ",voenkomat);

        voenkomat.Change_State();

        voenkomat.Change_State();

        voenkomat.RemoveObserver(d);

        voenkomat.Change_State();
    }
}
