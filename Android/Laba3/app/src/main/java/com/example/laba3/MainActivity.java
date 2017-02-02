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
    private int mResidue = 0;
    private final Handler mHandler = new Handler();

    private static final String IS_RUN_KEY = "IsRun";
    private static final String RESIDUE_KEY = "Residue";

    private boolean mWasRunning = false;
    private static final String WAS_RUNNING_KEY = "WasRunning";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        mStateToggleButton = (ToggleButton)findViewById(R.id.state_toggle_button);
        mHoursPicker = (NumberPicker)findViewById(R.id.hours_picker);
        mMinutesPicker = (NumberPicker)findViewById(R.id.minutes_picker);
        mSecondsPicker = (NumberPicker)findViewById(R.id.seconds_picker);
        mHoursPicker.setMaxValue(24);
        mMinutesPicker.setMaxValue(59);
        mSecondsPicker.setMaxValue(59);
        if (savedInstanceState != null) {
            mResidue = savedInstanceState.getInt(RESIDUE_KEY);
            mWasRunning = savedInstanceState.getBoolean(WAS_RUNNING_KEY);
            setRun(savedInstanceState.getBoolean(IS_RUN_KEY));
            updateNumberPickers();
        }
    }

    private void setRun(boolean value) {
        mHoursPicker.setEnabled(!value);
        mMinutesPicker.setEnabled(!value);
        mSecondsPicker.setEnabled(!value);
        mIsRun = value;
        if (mIsRun){
            mHandler.postDelayed(new TimerRunnable(), 1000);
        }
        mStateToggleButton.setChecked(value);
    }

    private int calcResidue() {
        return mSecondsPicker.getValue() + mMinutesPicker.getValue() * 60 +
                mHoursPicker.getValue() * 60 * 60;
    }


    private void updateNumberPickers() {
        mHoursPicker.setValue(mResidue / (60 * 60));
        mMinutesPicker.setValue((mResidue % (60 * 60)) / 60);
        mSecondsPicker.setValue(mResidue % 60);
    }


    private class TimerRunnable implements Runnable {

        @Override
        public void run() {
            if (!mIsRun){
                return;
            }
            mResidue = calcResidue();

            if (mResidue > 0){
                --mResidue;
            }
            if (mResidue == 0) {
                Toast.makeText(
                        MainActivity.this,
                        getString(R.string.time_over),
                        Toast.LENGTH_LONG)
                        .show();

                setRun(false);
                return;
            }
            updateNumberPickers();
            mHandler.postDelayed(this, 1000);
        }
    }

    public void onClickStateButton(View view) {
        setRun(mStateToggleButton.isChecked());
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        mResidue = calcResidue();
        outState.putInt(RESIDUE_KEY, mResidue);
        outState.putBoolean(IS_RUN_KEY, mIsRun);
        outState.putBoolean(WAS_RUNNING_KEY, mWasRunning);
    }

    @Override
    protected void onStop() {
        super.onPause();

        if (mIsRun){
            mWasRunning = true;
        }
        setRun(false);
    }

    @Override
    protected void onStart() {
        super.onResume();
        if (mWasRunning) {
            setRun(true);
            mWasRunning = false;
        }
    }
}
