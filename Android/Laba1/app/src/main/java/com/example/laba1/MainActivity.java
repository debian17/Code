package com.example.laba1;

import android.graphics.drawable.Drawable;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.ImageView;
import android.widget.TextView;

import static com.example.laba1.R.string.Maintext;
import static com.example.laba1.R.string.c1;
import static com.example.laba1.R.string.c2;
import static com.example.laba1.R.string.c3;
import static com.example.laba1.R.string.tc1;

public class MainActivity extends AppCompatActivity {

    private TextView text;
    private TextView contact1;
    private TextView contact2;
    private TextView contact3;
    private TextView ct1;
    private TextView ct2;
    private TextView ct3;
    private ImageView im1;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        String p1 = "@drawable/i1";
        text = (TextView) findViewById(R.id.List_contact);
        text.setText(Maintext);

        contact1 = (TextView)findViewById(R.id.contact1);
        contact1.setText(c1);

        ct1 = (TextView) findViewById(R.id.tc1);
        ct1.setText(tc1);

        im1 = (ImageView) findViewById(R.id.pic1);
        int imageResource = getResources().getIdentifier(p1, null, getPackageName());
        Drawable res = getResources().getDrawable(imageResource);
        im1.setImageDrawable(res);

    }
}
