package com.example.bas_bk.lab_no4;


import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.widget.TextViewCompat;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.TextView;

import static com.example.bas_bk.lab_no4.BookListFragment.adapter;

public class BookDetailFragment extends Fragment {
    TextView header;
    TextView description;
    Button clickMe;
    Book book;
    DBHelper dbHelper;

    public BookDetailFragment() {
    }

    @Override
    public View onCreateView(final LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        final View v = inflater.inflate(R.layout.fragment_book_detail, container, false);
        dbHelper = DBHelper.getInstance(getContext());
        book = dbHelper.getBook(getArguments().getInt("ID"));
        header = (TextView) v.findViewById(R.id.header);
        header.setText(book.name);
        description = (TextView) v.findViewById(R.id.description);
        description.setText(book.desc);
        clickMe = (Button) v.findViewById(R.id.clickPLZ);
        final Bundle args = new Bundle();
        clickMe.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                dbHelper.switchStatus(book);
                clickMe.setVisibility(View.INVISIBLE);
                FragmentTransaction fragmentTransaction = getActivity().getSupportFragmentManager().beginTransaction();
                BookListFragment bookListFragment = new BookListFragment();
                if (book.deleted) {
                    args.putBoolean("Trashed", true);
                    bookListFragment.setArguments(args);
                }
                fragmentTransaction.replace(R.id.list_container, bookListFragment).addToBackStack(null);
                fragmentTransaction.commit();
            }
        });



        clickMe.setText("Удалить");
        if (book.deleted) {
            clickMe.setText("Восстановить");
        }
        return v;
    }

}
