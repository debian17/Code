package com.example.laba1;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.List;

import static android.icu.lang.UCharacter.GraphemeClusterBreak.L;

public class MainActivity extends AppCompatActivity {
    private TextView textView;
    private ArrayList<Item> items;
    private RecyclerView RV;
    private ItemAdapter itemAdapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        textView = (TextView)findViewById(R.id.Contact_list);
        textView.setText(R.string.c_list);
        items = new ArrayList<Item>();

        items.add(new Item("Печальная жаба","Я печальна жаба и жизнь моя печальна",
                getResources().getIdentifier("@drawable/item1", null, getPackageName()),"#e6eeff"));
        items.add(new Item("Ждун","Я тут подожду пока...",
                getResources().getIdentifier("@drawable/item2", null, getPackageName()),"#ffffb3"));
        items.add(new Item("Вжух","Вжух и ты сдал лабу!",
                getResources().getIdentifier("@drawable/item3", null, getPackageName()),"#800000"));

        RV = (RecyclerView)findViewById(R.id.RV_Items);
        itemAdapter = new ItemAdapter(this, items);
        RV.setAdapter(itemAdapter);
        RV.setLayoutManager(new LinearLayoutManager(this));
    }
}
