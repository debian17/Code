package com.example.laba5;

import android.content.Intent;
import android.database.Cursor;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.View;

import java.util.ArrayList;
import java.util.List;

import static com.example.laba5.R.id.RV;

public class MainActivity extends AppCompatActivity {
    private RecyclerView recyclerView;
    private RVAdapter rvAdapter;
    private LinearLayoutManager linearLayoutManager;
    private List<Task> tasks;
    private TaskCursorWrapper taskCursorWrapper;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        //Cursor cursor = TaskManager.mDatabase.query(Task.TABLE_NAME,)

        tasks = new ArrayList<Task>();

        tasks.add(new Task("Задача №1", "12.12.2012", "Купи хлеб."));
        tasks.add(new Task("Задача №2", "13.12.2012", "Сдай лабу."));
        tasks.add(new Task("Задача №3", "14.12.2012", "Получи диплом."));
        tasks.add(new Task("Задача №4", "15.12.2012", "Сходи на тренировку."));
        tasks.add(new Task("Задача №5", "16.12.2012", "Захвати мир."));

        recyclerView = (RecyclerView) findViewById(R.id.RV);
        linearLayoutManager = new LinearLayoutManager(this);
        recyclerView.setLayoutManager(linearLayoutManager);

        rvAdapter = new RVAdapter(this, tasks);

        recyclerView.setAdapter(rvAdapter);
    }

    public void onAddClick(View view){
        Intent intent = new Intent(this, SecondActivity.class);
        startActivity(intent);
    }

}
