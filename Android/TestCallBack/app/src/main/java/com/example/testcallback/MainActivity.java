package com.example.testcallback;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity implements TaslIsDone {
    private TextView textView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        textView = (TextView) findViewById(R.id.text_test);
        AsyncText asyncText = new AsyncText(this);
        asyncText.execute();
    }

    @Override
    public void onTaskIsDone(String s) {
        textView.setText(s);
    }
}
