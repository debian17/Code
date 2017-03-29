package com.example.bas_bk.lab_no4;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

import java.util.ArrayList;
import java.util.List;

import static android.content.ContentValues.TAG;

public class DBHelper extends SQLiteOpenHelper {
    private static DBHelper sInstance;
    private static final String DATABASE_NAME = "bookDB";
    private static final int DATABASE_VERSION = 1;
    private static final String TABLE_BOOKS = "books";
    private static final String KEY_BOOK_ID = "id";
    private static final String KEY_BOOK_NAME = "name";
    private static final String KEY_BOOK_DESC = "desc";
    private static final String KEY_BOOK_DELETED = "deleted";

    public static synchronized DBHelper getInstance(Context context) {
        if (sInstance == null){
            sInstance = new DBHelper(context.getApplicationContext());
        }
        return sInstance;
    }

    private DBHelper(Context context){
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
    }


    @Override
    public void onCreate(SQLiteDatabase db) {
        String CREATE_BOOKS_TABLE = "CREATE TABLE " + TABLE_BOOKS +
                "(" +
                    KEY_BOOK_ID + " INTEGER PRIMARY KEY," +
                    KEY_BOOK_NAME + " TEXT," +
                    KEY_BOOK_DESC + " TEXT," +
                    KEY_BOOK_DELETED + " INTEGER" +
                ")";
        db.execSQL(CREATE_BOOKS_TABLE);


    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        if (oldVersion != newVersion) {
            db.execSQL("DROP TABLE IF EXISTS " + TABLE_BOOKS);
            onCreate(db);
        }
    }

    public void addBook(Book book){
        SQLiteDatabase db = getWritableDatabase();

        db.beginTransaction();
        try {
            ContentValues values = new ContentValues();
            values.put(KEY_BOOK_NAME, book.name);
            values.put(KEY_BOOK_DESC, book.desc);
            values.put(KEY_BOOK_DELETED, ((book.deleted)? 1 : 0));

            db.insertOrThrow(TABLE_BOOKS, null, values);
            db.setTransactionSuccessful();
        } catch (Exception e) {
            Log.d(TAG, "Error while trying to add book to database");
        } finally {
            db.endTransaction();
        }
    }

    public Book getBook(int id) {
        Book book = new Book();
        String BOOK_SELECT_QUERY =
                String.format("SELECT * FROM %s WHERE id = %s", TABLE_BOOKS, id);

        SQLiteDatabase db = getReadableDatabase();
        Cursor cursor = db.rawQuery(BOOK_SELECT_QUERY, null);
        try {
            if (cursor.moveToFirst()) {
                book.id = cursor.getInt(cursor.getColumnIndex(KEY_BOOK_ID));
                book.name = cursor.getString(cursor.getColumnIndex(KEY_BOOK_NAME));
                book.desc = cursor.getString(cursor.getColumnIndex(KEY_BOOK_DESC));
                book.deleted = (cursor.getInt(cursor.getColumnIndex(KEY_BOOK_DELETED)) == 1)? true : false;
            }
        } catch (Exception e) {
            Log.d(TAG, "Error while trying to get books from database");
        } finally {
            if (cursor != null && !cursor.isClosed()) {
                cursor.close();
            }
        }
        return book;
    }

    public List<Book> getBooks() {
        List<Book> books = new ArrayList<>();
        String BOOKS_SELECT_QUERY =
                String.format("SELECT * FROM %s WHERE deleted = 0", TABLE_BOOKS);

        SQLiteDatabase db = getReadableDatabase();
        Cursor cursor = db.rawQuery(BOOKS_SELECT_QUERY, null);
        try {
            if (cursor.moveToFirst()) {
                do {
                    Book newBook = new Book();
                    newBook.id = cursor.getInt(cursor.getColumnIndex(KEY_BOOK_ID));
                    newBook.name = cursor.getString(cursor.getColumnIndex(KEY_BOOK_NAME));
                    newBook.desc = cursor.getString(cursor.getColumnIndex(KEY_BOOK_DESC));
                    newBook.deleted = (cursor.getInt(cursor.getColumnIndex(KEY_BOOK_DELETED)) == 1)? true : false;
                    books.add(newBook);
                } while (cursor.moveToNext());
            }
        } catch (Exception e) {
            Log.d(TAG, "Error while trying to get books from database");
        } finally {
            if (cursor != null && !cursor.isClosed()) {
                cursor.close();
            }
        }
        return books;
    }

    public List<Book> getDelBooks() {
        List<Book> books = new ArrayList<>();
        String BOOKS_SELECT_QUERY =
                String.format("SELECT * FROM %s WHERE deleted = 1", TABLE_BOOKS);

        SQLiteDatabase db = getReadableDatabase();
        Cursor cursor = db.rawQuery(BOOKS_SELECT_QUERY, null);
        try {
            if (cursor.moveToFirst()) {
                do {
                    Book newBook = new Book();
                    newBook.id = cursor.getInt(cursor.getColumnIndex(KEY_BOOK_ID));
                    newBook.name = cursor.getString(cursor.getColumnIndex(KEY_BOOK_NAME));
                    newBook.desc = cursor.getString(cursor.getColumnIndex(KEY_BOOK_DESC));
                    newBook.deleted = (cursor.getInt(cursor.getColumnIndex(KEY_BOOK_DELETED)) == 1)? true : false;
                    books.add(newBook);
                } while (cursor.moveToNext());
            }
        } catch (Exception e) {
            Log.d(TAG, "Error while trying to get books from database");
        } finally {
            if (cursor != null && !cursor.isClosed()) {
                cursor.close();
            }
        }
        return books;
    }

    public int switchStatus(Book book) {
        SQLiteDatabase db = getWritableDatabase();

        ContentValues values = new ContentValues();
        values.put(KEY_BOOK_DELETED, ((book.deleted)? 0 : 1));

        return db.update(TABLE_BOOKS, values, KEY_BOOK_NAME + " = ?", new String[] {book.name});
    }
}
