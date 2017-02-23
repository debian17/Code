package com.example.fchat;

import java.util.Date;

/**
 * Created by Андрей Кравченко on 08.02.2017.
 */

public class Message {

    private String author;
    private String textMessage;
    private long time;

    public Message(String author, String textMessage) {
        this.author = author;
        this.textMessage = textMessage;
        time = new Date().getTime();
    }

    public Message() {
    }

    public String getTextMessage() {
        return textMessage;
    }

    public String getAuthor() {
        return author;
    }

    public long getTime() {
        return time;
    }

    public void setAuthor(String author) {
        this.author = author;
    }

    public void setTextMessage(String textMessage) {
        this.textMessage = textMessage;
    }

    public void setTime(long time) {
        this.time = time;
    }
}
