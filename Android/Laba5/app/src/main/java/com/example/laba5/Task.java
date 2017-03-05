package com.example.laba5;

import java.util.Date;

/**
 * Created by Андрей Кравченко on 05-Mar-17.
 */

public class Task {

    private String name_task;
    private String date;
    private String description;

    public Task(String name_task, String date, String description) {
        this.name_task = name_task;
        this.date = date;
        this.description = description;
    }

    public String getName_task() {
        return name_task;
    }

    public String getDate() {
        return date;
    }

    public String getDescription() {
        return description;
    }
}
