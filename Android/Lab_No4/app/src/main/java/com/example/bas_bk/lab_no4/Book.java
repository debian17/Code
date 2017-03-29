package com.example.bas_bk.lab_no4;

public class Book {
    public int id;
    public String name;
    public String desc;
    public Boolean deleted;

    @Override
    public String toString(){
        return name;
    }
}
