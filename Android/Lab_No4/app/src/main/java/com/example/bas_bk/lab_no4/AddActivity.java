package com.example.bas_bk.lab_no4;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;

public class AddActivity extends AppCompatActivity {
    EditText name;
    EditText desc;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add);

        name = (EditText) findViewById(R.id.name_ET);
        desc = (EditText) findViewById(R.id.desc_ET);
    }

    public void onAdd(View v) {
        Book newBook = new Book();

        newBook.name = name.getText().toString();
        newBook.desc = desc.getText().toString();
        newBook.deleted = false;

        DBHelper dbHelper = DBHelper.getInstance(this);
        dbHelper.addBook(newBook);

        name.getText().clear();
        desc.getText().clear();
    }
}
