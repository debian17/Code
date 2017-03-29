package com.example.bas_bk.lab_no4;

import android.content.Intent;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Toast;

import java.util.List;

public class MainActivity extends AppCompatActivity {
    BookListFragment bookListFragment;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

//        Book book1 = new Book();
//        book1.name = "Сто способов спрятать труп";
//        book1.desc = "В данной книге вы найдете сто способов спрятать ненужный вам труп.";
//        book1.deleted = false;
//
//        Book book2 = new Book();
//        book2.name = "Как ухаживать за улиткой";
//        book2.desc = "В данной книге вы узнаете, что делать, чтобы ваша улитка не отъехала в другой мир.";
//        book2.deleted = false;
//
//        Book book3 = new Book();
//        book3.name = "'Это он сделал' и другие отмазки";
//        book3.desc = "Сборник самых качественных отмазок за всю историю человечества.";
//        book3.deleted = false;
//
//        DBHelper dbHelper = DBHelper.getInstance(this);
//
//        dbHelper.addBook(book1);
//        dbHelper.addBook(book2);
//        dbHelper.addBook(book3);
    }

    @Override
    public void onResume(){
        super.onResume();
        bookListFragment = new BookListFragment();
        FragmentTransaction fragmentTransaction = getSupportFragmentManager().beginTransaction();
        fragmentTransaction.replace(R.id.list_container, bookListFragment).addToBackStack(null);
        fragmentTransaction.commit();
    }

    public void onTrashClick(View v) {
        bookListFragment = new BookListFragment();

        Bundle args = new Bundle();
        args.putBoolean("Trashed", true);
        bookListFragment.setArguments(args);

        FragmentTransaction fragmentTransaction = getSupportFragmentManager().beginTransaction();
        fragmentTransaction.replace(R.id.list_container, bookListFragment).addToBackStack(null);
        fragmentTransaction.commit();
    }

    public void onAddClick(View v) {
        Intent intent = new Intent(MainActivity.this, AddActivity.class);
        startActivity(intent);
    }

}
