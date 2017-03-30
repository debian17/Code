package com.example.laba5;

import android.content.ContentValues;
import android.content.Context;
import android.database.sqlite.SQLiteDatabase;

/**
 * Created by Андрей Кравченко on 05-Mar-17.
 */

public class TaskManager {

    private Context mContext;
    public static SQLiteDatabase mDatabase;

    public TaskManager(Context context) {
        mContext = context;
        mDatabase = new TaskBaseHelper(mContext).getWritableDatabase();

        addTask(new Task("Задача №1", "12.12.2012", "Купи хлеб."));
        addTask(new Task("Задача №2", "13.12.2012", "Сдай лабу."));
        addTask(new Task("Задача №3", "14.12.2012", "Получи диплом."));
        addTask(new Task("Задача №4", "15.12.2012", "Сходи на тренировку."));
        addTask(new Task("Задача №5", "16.12.2012", "Захвати мир."));
    }

    private static ContentValues getContentValues(Task task) {
        ContentValues values = new ContentValues();
        values.put(Task.NAME, task.getName_task());
        values.put(Task.DATE, task.getDate());
        values.put(Task.DESCRIPTION, task.getDescription());
        return values;
    }

    public void addTask(Task task) {
        ContentValues values = getContentValues(task);
        mDatabase.insert(Task.NAME, null, values);
    }


}
