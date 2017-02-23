package com.example.laba3;

import android.os.Handler;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.NumberPicker;
import android.widget.Toast;
import android.widget.ToggleButton;

public class MainActivity extends AppCompatActivity {

    private ToggleButton mStateToggleButton = null;
    private NumberPicker mHoursPicker = null;
    private NumberPicker mMinutesPicker = null;
    private NumberPicker mSecondsPicker = null;

    private boolean mIsRun = false;
    private final Handler mHandler = new Handler();

    private static final String IS_RUN_KEY = "IsRun";

    private boolean mWasRunning = false;
    private static final String WAS_RUNNING_KEY = "WasRunning";

    //при создании
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        mStateToggleButton = (ToggleButton)findViewById(R.id.state_toggle_button);
        mHoursPicker = (NumberPicker)findViewById(R.id.hours_picker);
        mMinutesPicker = (NumberPicker)findViewById(R.id.minutes_picker);
        mSecondsPicker = (NumberPicker)findViewById(R.id.seconds_picker);

        mHoursPicker.setEnabled(false);
        mMinutesPicker.setEnabled(false);
        mSecondsPicker.setEnabled(false);

        mHoursPicker.setMaxValue(24);
        mMinutesPicker.setMaxValue(59);
        mSecondsPicker.setMaxValue(59);

        if (savedInstanceState != null) {
            mSecondsPicker.setValue(savedInstanceState.getInt("s"));
            mMinutesPicker.setValue(savedInstanceState.getInt("m"));
            mHoursPicker.setValue(savedInstanceState.getInt("h"));
            mWasRunning = savedInstanceState.getBoolean(WAS_RUNNING_KEY);
            setRun(savedInstanceState.getBoolean(IS_RUN_KEY));
        }
    }

    private void setRun(boolean value) {
        mIsRun = value;

        if(mIsRun){
            mHandler.postDelayed(new TimerRunnable(), 1000);
        }
        mStateToggleButton.setChecked(value);
    }

    //
    private void updateNumberPickers() {

        if(mSecondsPicker.getValue()<59){
            mSecondsPicker.setValue(mSecondsPicker.getValue()+1);
        }

        if(mSecondsPicker.getValue()==59){
            mSecondsPicker.setValue(0);
            if(mMinutesPicker.getValue()==59){
                mMinutesPicker.setValue(0);
                mHoursPicker.setValue(mHoursPicker.getValue()+1);
                return;
            }
        }

        if(mMinutesPicker.getValue() == 59){
            mMinutesPicker.setValue(0);
            mHoursPicker.setValue(mHoursPicker.getValue()+1);
        }
    }

    private class TimerRunnable implements Runnable {

        @Override
        public void run() {
            if(!mIsRun) return;
            updateNumberPickers();
            mHandler.postDelayed(this, 1000);
        }
    }

    public void onClickStateButton(View view) {
        setRun(mStateToggleButton.isChecked());
    }

    //сохраняем текущее состояние
    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putInt("s", mSecondsPicker.getValue());
        outState.putInt("m", mMinutesPicker.getValue());
        outState.putInt("h", mHoursPicker.getValue());
        outState.putBoolean(IS_RUN_KEY, mIsRun);
        outState.putBoolean(WAS_RUNNING_KEY, mWasRunning);
    }

    //активность полностью скрывается
    @Override
    protected void onStop() {
        //super.onPause();
        super.onStop();
        if (mIsRun){
            mWasRunning = true;
        }
        setRun(false);
    }

    //активность готова к отображению и становится видимой
    @Override
    protected void onStart() {
        //super.onResume();
        super.onStart();
        if (mWasRunning) {
            setRun(true);
            mWasRunning = false;
        }
    }

    public void Reset(View view){
        mSecondsPicker.setValue(0);
        mMinutesPicker.setValue(0);
        mHoursPicker.setValue(0);
        mIsRun = false;
        mStateToggleButton.setChecked(false);
    }
}
