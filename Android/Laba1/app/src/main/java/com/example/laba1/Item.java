package com.example.laba1;

/**
 * Created by Андрей Кравченко on 01.02.2017.
 */

public class Item {
    private String Sender;
    private String Text;
    private int Image;
    private String Color;

    public Item(String sender, String text, int image, String color) {
        Sender = sender;
        Text = text;
        Image = image;
        Color = color;
    }

    public String getSender() {
        return Sender;
    }
    public String getText() {
        return Text;
    }

    public int getImage() {
        return Image;
    }

    public String getColor(){
        return Color;
    }
}
