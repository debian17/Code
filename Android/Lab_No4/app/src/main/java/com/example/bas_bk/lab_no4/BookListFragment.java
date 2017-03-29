package com.example.bas_bk.lab_no4;


import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.app.ListFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ListAdapter;
import android.widget.ListView;

public class BookListFragment extends ListFragment {
    static ArrayAdapter<Book> adapter;

    public BookListFragment() {
    }

    @Override
    public void onListItemClick(ListView l, View v, int position, long id) {
        super.onListItemClick(l, v, position, id);
        Book clickedBook = (Book) l.getAdapter().getItem(position);
        BookDetailFragment fragment = new BookDetailFragment();
        Bundle args = new Bundle();
        args.putInt("ID", clickedBook.id);
        fragment.setArguments(args);
        FragmentTransaction fragmentTransaction = getActivity().getSupportFragmentManager().beginTransaction();
        if (getActivity().findViewById(R.id.detail_container) == null) {
            fragmentTransaction.replace(R.id.list_container, fragment).addToBackStack(null);
        } else {
            fragmentTransaction.replace(R.id.detail_container, fragment).addToBackStack(null);
        }
        fragmentTransaction.commit();
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        DBHelper dbHelper = DBHelper.getInstance(getContext());
        if (getArguments() == null) {
            adapter = new ArrayAdapter<>(inflater.getContext(),
                    android.R.layout.simple_list_item_1, dbHelper.getBooks());
        } else {
            adapter = new ArrayAdapter<>(inflater.getContext(),
                    android.R.layout.simple_list_item_1, dbHelper.getDelBooks());
        }
        setListAdapter(adapter);
        return super.onCreateView(inflater, container, savedInstanceState);
    }

}
