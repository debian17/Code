package com.example.laba5;

import android.content.Context;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CheckBox;
import android.widget.TextView;
import android.widget.Toast;

import java.util.List;

/**
 * Created by Андрей Кравченко on 05-Mar-17.
 */

public class RVAdapter extends RecyclerView.Adapter<RVAdapter.ViewHolder> {

    public static class ViewHolder extends RecyclerView.ViewHolder {
        private TextView task_name;
        private TextView date;
        private FloatingActionButton floatingActionButton;
        private CheckBox checkBox;

        ViewHolder(View ItemView){
            super(ItemView);
            task_name = (TextView) ItemView.findViewById(R.id.name_task);
            date = (TextView) ItemView.findViewById(R.id.date);
            floatingActionButton = (FloatingActionButton) ItemView.findViewById(R.id.fab);
            floatingActionButton.setClickable(true);
            checkBox = (CheckBox) ItemView.findViewById(R.id.cb);
        }
    }

    private Context context;
    private List<Task> tasks;

    public RVAdapter(Context context, List<Task> tasks){
        this.tasks = tasks;
        this.context = context;
    }

    @Override
    public ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.rvlist_item, parent, false);
        ViewHolder viewHolder = new ViewHolder(view);
        return viewHolder;
    }

    @Override
    public void onBindViewHolder(final ViewHolder holder, final int position) {
        Task item = tasks.get(position);
        holder.task_name.setText(item.getName_task());
        holder.date.setText(item.getDate());

        holder.floatingActionButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Toast.makeText(context, tasks.get(position).getDescription(), Toast.LENGTH_SHORT).show();
            }
        });

        holder.checkBox.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(holder.checkBox.isChecked()){
                    Toast.makeText(context, "Молодец!", Toast.LENGTH_SHORT).show();
                }
                else {
                    Toast.makeText(context, "Не забудь выполнить!", Toast.LENGTH_SHORT).show();
                }
            }
        });
    }

    @Override
    public int getItemCount() {
        return tasks.size();
    }
}
