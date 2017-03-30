package com.example.laba5;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

/**
 * Created by Андрей Кравченко on 05-Mar-17.
 */

public class TaskBaseHelper extends SQLiteOpenHelper {
    private static final int VERSION = 1;
    private static final String DATABASE_NAME = "Tasks.db";

    public TaskBaseHelper(Context context){
        super(context, DATABASE_NAME, null, VERSION);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        db.execSQL("create table " + Task.TABLE_NAME + "(" +
                " _id integer primary key autoincrement, " +
                Task.NAME + ", " +
                Task.DATE + ", " +
                Task.DESCRIPTION +
                ")"
        );


    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {

    }
}
