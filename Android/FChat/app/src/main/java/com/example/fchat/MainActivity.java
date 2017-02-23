package com.example.fchat;

import android.content.Intent;
import android.os.*;
import android.support.annotation.NonNull;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.RelativeLayout;
import android.widget.TextView;

import com.firebase.ui.auth.AuthUI;
import com.firebase.ui.database.FirebaseListAdapter;
import com.google.android.gms.auth.api.Auth;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.database.FirebaseDatabase;
import android.text.format.DateFormat;

public class MainActivity extends AppCompatActivity {
    private static int SIGN_IN_REQUEST_CODE = 1;

    private FirebaseListAdapter<Message> adapter;
    private RelativeLayout activity_main;
    private Button button;
    private EditText input;
    private TextView author;
    private TextView textMessage;
    private TextView time;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        activity_main = (RelativeLayout) findViewById(R.id.activity_main);
        button = (Button) findViewById(R.id.send_btn);
        input = (EditText) findViewById(R.id.editText);

        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                FirebaseDatabase.getInstance().getReference().push().setValue(new Message(input.getText().toString(),
                        FirebaseAuth.getInstance().getCurrentUser().getEmail()));
                input.setText("");

            }
        });

        if(FirebaseAuth.getInstance().getCurrentUser() ==null){
            startActivityForResult(AuthUI.getInstance().createSignInIntentBuilder().build(), SIGN_IN_REQUEST_CODE);
        }
        else{
            displayChat();
        }
    }

    private void displayChat(){
        ListView listView = (ListView) findViewById(R.id.list_message);
        adapter = new FirebaseListAdapter<Message>(this, Message.class, R.layout.item,
                FirebaseDatabase.getInstance().getReference()) {
            @Override
            protected void populateView(View v, Message model, int position) {
                textMessage = (TextView) v.findViewById(R.id.TextMessage);
                author = (TextView) v.findViewById(R.id.SenderName);
                time = (TextView) v.findViewById(R.id.TimeInfo);

                textMessage.setText(model.getTextMessage());
                author.setText(model.getAuthor());
                time.setText(DateFormat.format("dd-MM--yyyy (HH:mm:ss)", model.getTime()));
            }
        };
        listView.setAdapter(adapter);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data){
        super.onActivityResult(requestCode,resultCode,data);

        if(requestCode == SIGN_IN_REQUEST_CODE){
            if(resultCode == RESULT_OK){
                Snackbar.make(activity_main,"Вход выполнен", Snackbar.LENGTH_LONG).show();
                displayChat();
            }
            else{
                Snackbar.make(activity_main,"Ошибка входа!", Snackbar.LENGTH_LONG).show();
                finish();
            }
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu){
        getMenuInflater().inflate(R.menu.menu, menu);
        return  true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem menuItem){
        if(menuItem.getItemId()== R.id.menu_signout){
            AuthUI.getInstance().signOut(this).addOnCompleteListener(new OnCompleteListener<Void>() {
                @Override
                public void onComplete(@NonNull Task<Void> task) {
                    Snackbar.make(activity_main,"Выход выполнен!", Snackbar.LENGTH_LONG).show();
                    finish();
                }
            });
        }
        return true;
    }
}
