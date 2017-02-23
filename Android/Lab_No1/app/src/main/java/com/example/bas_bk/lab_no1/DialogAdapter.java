package com.example.bas_bk.lab_no1;

import android.content.Context;
import android.graphics.Color;
import android.graphics.drawable.Drawable;
import android.support.v7.widget.RecyclerView;
import android.text.Layout;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;

import java.util.List;

/**
 * Created by BAS_BK on 31.01.2017.
 */

public class DialogAdapter extends RecyclerView.Adapter<DialogAdapter.ViewHolder> {
    public static class ViewHolder extends RecyclerView.ViewHolder {
        public TextView senderName;
        public TextView textMessage;
        public ImageView image;

        public ViewHolder (View itemView) {
            super(itemView);

            senderName = (TextView) itemView.findViewById(R.id.sender);
            textMessage = (TextView) itemView.findViewById(R.id.textMessage);
            image = (ImageView) itemView.findViewById(R.id.ava);
        }
    }

    private List<Dialog> mDialog;
    private Context mContext;

    public DialogAdapter (Context context, List<Dialog> dialogs) {
        mContext = context;
        mDialog = dialogs;
    }

    private Context getContext() {
        return mContext;
    }

    @Override
    public DialogAdapter.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(parent.getContext()).inflate(R.layout.rv_item, parent, false);
        ViewHolder viewHolder = new ViewHolder(v);
        return viewHolder;
    }

    @Override
    public void onBindViewHolder(DialogAdapter.ViewHolder viewHolder, int position) {
        Dialog dialog = mDialog.get(position);
        if (dialog.isRead()) {
            viewHolder.itemView.setBackgroundColor(Color.WHITE);
        }
        else if (!dialog.isRead() && dialog.isUrgent()) {
            viewHolder.itemView.setBackgroundColor(Color.parseColor("#5078fa"));
        }
        else {
            viewHolder.itemView.setBackgroundColor(Color.parseColor("#9eb3f7"));
        }

        TextView senderTextView = viewHolder.senderName;
        senderTextView.setText(dialog.getSenderName());
        TextView messageTextView = viewHolder.textMessage;
        messageTextView.setText(dialog.getTextMessage());
        ImageView imageView = viewHolder.image;
        imageView.setImageResource(dialog.getImage());

    }

    @Override
    public int getItemCount() {
        return mDialog.size();
    }
}
