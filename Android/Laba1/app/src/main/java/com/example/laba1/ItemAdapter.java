package com.example.laba1;

import android.content.Context;
import android.graphics.Color;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.List;

public class ItemAdapter extends RecyclerView.Adapter<ItemAdapter.ViewHolder> {
    public static class ViewHolder extends RecyclerView.ViewHolder{

        public TextView Sender;
        public TextView Text;
        public ImageView Image;

        public ViewHolder(View Item_view){
            super(Item_view);
            Sender = (TextView) Item_view.findViewById(R.id.Sender);
            Text = (TextView) Item_view.findViewById(R.id.Text);
            Image = (ImageView) Item_view.findViewById(R.id.picture);
        }
    }
    private List<Item> mItems;
    private Context mContext;

    private Context getContext(){
        return mContext;
    }

    @Override
    public int getItemCount(){
        return mItems.size();
    }

    public ItemAdapter(Context context, List<Item> items){
        mItems = items;
        mContext = context;
    }

    @Override
    public ItemAdapter.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_template, parent, false);
        ViewHolder viewHolder = new ViewHolder(v);
        return viewHolder;
    }

    @Override
    public void onBindViewHolder(ItemAdapter.ViewHolder viewHolder, int position) {
        Item item = mItems.get(position);
        viewHolder.itemView.setBackgroundColor(Color.parseColor(item.getColor()));
        TextView senderTextView = viewHolder.Sender;
        senderTextView.setText(item.getSender());
        TextView messageTextView = viewHolder.Text;
        messageTextView.setText(item.getText());
        ImageView imageView = viewHolder.Image;
        imageView.setImageResource(item.getImage());
    }
}
