package com.example.laba2;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.TextView;

public class ThridActivity extends AppCompatActivity {
    private TextView textView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_thrid);
        textView = (TextView) findViewById(R.id.t_text);
        Intent intent = getIntent();
        textView.setText(intent.getStringExtra(Intent.EXTRA_TEXT));
    }
}
