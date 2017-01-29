package com.example.ndktest;


import org.opencv.core.Mat;
import org.opencv.core.MatOfKeyPoint;
import org.opencv.features2d.FeatureDetector;

import java.util.concurrent.locks.ReentrantLock;

public class JThread implements Runnable {
    private Mat inputFrame;
    private MatOfKeyPoint keyPoints;
    private FeatureDetector featureDetector;

    JThread(FeatureDetector featureDetector, Mat inputFrame, MatOfKeyPoint keyPoints){
        this.featureDetector = featureDetector;
        this.inputFrame = inputFrame;
        this.keyPoints = keyPoints;
    }

    public void run(){
        synchronized (inputFrame){
            featureDetector.detect(inputFrame, keyPoints);
        }

    }
}
