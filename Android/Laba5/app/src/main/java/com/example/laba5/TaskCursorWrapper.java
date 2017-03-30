package com.example.laba5;

import android.database.Cursor;
import android.database.CursorWrapper;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Андрей Кравченко on 05-Mar-17.
 */

public class TaskCursorWrapper extends CursorWrapper {

    public TaskCursorWrapper(Cursor cursor) {
        super(cursor);
    }

    public Task getTask() {
        String name = getString(getColumnIndex(Task.NAME));
        String date = getString(getColumnIndex(Task.DATE));
        String desc = getString(getColumnIndex(Task.DESCRIPTION));

        Task task = new Task(
                name,
                date,
                desc
        );
        return task;
    }

    public List<Task> getContacts() {
        List<Task> contacts = new ArrayList<>();
        TaskCursorWrapper cursor = queryTask(null, null);
        try {
            cursor.moveToFirst();
            while (!cursor.isAfterLast()) {
                contacts.add(cursor.getTask());
                cursor.moveToNext();
            }
        } finally {
            cursor.close();
        }
        return contacts;
    }

    private TaskCursorWrapper queryTask(String whereClause, String[] whereArgs) {
        Cursor cursor = TaskManager.mDatabase.query(
                Task.NAME,
                null, // Columns - null выбирает все столбцы
                whereClause,
                whereArgs,
                null, // groupBy
                null, // having
                null // orderBy
        );

        return new TaskCursorWrapper(cursor);
    }
}
