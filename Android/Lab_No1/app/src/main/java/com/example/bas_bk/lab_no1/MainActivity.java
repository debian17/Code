package com.example.bas_bk.lab_no1;

import android.graphics.drawable.Drawable;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;

public class MainActivity extends AppCompatActivity {

    ArrayList<Dialog> dialogs;
    TextView title;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        title = (TextView)findViewById(R.id.title);
        title.setText(R.string.titleTV);

        dialogs = new ArrayList<Dialog>();
        dialogs.add(new Dialog("Вупсень", "Я не Пупсень!", true, true, getResources().getIdentifier("@drawable/vupsen", null, getPackageName())));
        dialogs.add(new Dialog("Пупсень", "Я не Вупсень!", false, true, getResources().getIdentifier("@drawable/pupsen", null, getPackageName())));
        dialogs.add(new Dialog("Лунтик", "Убейте меня!", false, false, getResources().getIdentifier("@drawable/luntik", null, getPackageName())));
        RecyclerView rvDialogs = (RecyclerView) findViewById(R.id.rvDialogs);
        DialogAdapter adapter = new DialogAdapter(this, dialogs);
        rvDialogs.setAdapter(adapter);
        rvDialogs.setLayoutManager(new LinearLayoutManager(this));
    }
    public void click(View v) {
        Toast.makeText(this, "Да мы тут это, плюшками играемся...", Toast.LENGTH_LONG).show();
    }
}
