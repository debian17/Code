package com.company;

import org.omg.PortableInterceptor.SYSTEM_EXCEPTION;

import java.io.*;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        String FileName="1.txt";
        System.out.println("Введите строку для записи:");
        Scanner s = new Scanner(System.in);

        /*try(FileWriter fw = new FileWriter(FileName)){
            for(int i=-10;i<10;i++){
                String buf = Integer.toString(i);
                fw.write(buf);
                fw.append("\r\n");
            }
            fw.flush();

        }
        catch(IOException e){
            e.getMessage();
        }*/

        try(BufferedReader br = new BufferedReader(new FileReader(FileName))){
            String line;
            while ((line=br.readLine())!=null){
                System.out.println(line);
            }
        }
        catch (IOException e){
            e.getMessage();
        }

    }
}
