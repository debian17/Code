package com.example.laba2;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;

public class MainActivity extends AppCompatActivity {
    private EditText editText;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        editText = (EditText) findViewById(R.id.edit_message);
    }

    public void OnClick_btn1(View view){
        if(editText.getText().toString().equals("")){
            editText.setHint(R.string.hint);
        }
        else{
            Intent intent = new Intent(this, SecondActivity.class);
            intent.putExtra("message",editText.getText().toString());
            startActivity(intent);
        }
    }
}
