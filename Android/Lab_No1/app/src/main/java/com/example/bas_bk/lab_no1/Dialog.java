package com.example.bas_bk.lab_no1;


import android.graphics.drawable.Drawable;

/**
 * Created by BAS_BK on 31.01.2017.
 */

public class Dialog {
    private String senderName;
    private String textMessage;
    private boolean isRead;
    private boolean isUrgent;
    private int image;

    public Dialog(String senderName, String textMessage, boolean isRead, boolean isUrgent, int image) {
        this.senderName = senderName;
        this.textMessage = textMessage;
        this.isRead = isRead;
        this.isUrgent = isUrgent;
        this.image = image;
    }

    public String getSenderName() {
        return senderName;
    }

    public String getTextMessage() {
        return textMessage;
    }

    public boolean isRead() {
        return isRead;
    }

    public boolean isUrgent() {
        return isUrgent;
    }

    public int getImage() {
        return image;
    }
}
