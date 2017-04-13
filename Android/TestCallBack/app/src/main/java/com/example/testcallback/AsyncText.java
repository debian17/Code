package com.example.testcallback;

import android.os.AsyncTask;

/**
 * Created by Андрей Кравченко on 12-Apr-17.
 */

public class AsyncText extends AsyncTask<Void, Void, String> {

    public TaslIsDone taslIsDone;

    public AsyncText(TaslIsDone taslIsDone){
        this.taslIsDone = taslIsDone;
    }

    @Override
    protected String doInBackground(Void... params) {
        try {
            Thread.sleep(5000);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return "ЕЕЕЕ, BITCH!";
    }

    @Override
    protected void onPostExecute(String s) {
        super.onPostExecute(s);
        taslIsDone.onTaskIsDone(s);

    }
}
