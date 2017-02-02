package com.example.laba2;

import android.content.Intent;
import android.os.StrictMode;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

public class SecondActivity extends AppCompatActivity {
    private TextView textView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_second);
        textView = (TextView) findViewById(R.id.message_text);
        Intent intent = getIntent();
        String message = intent.getStringExtra("message");
        if(!message.equals("")){
            textView.setText(message);
        }
        else {
            textView.setText("NULL");
        }
    }

    public void OnClick_btn2(View view){
        Intent intent = new Intent(Intent.ACTION_SEND);
        intent.setType("text/plain");
        intent.putExtra(Intent.EXTRA_TEXT, textView.getText().toString());
        startActivity(intent);
    }
}
