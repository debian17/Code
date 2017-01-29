package com.example.ndktest;

import org.opencv.core.Mat;
import org.opencv.core.MatOfKeyPoint;
import org.opencv.features2d.FeatureDetector;

public class Params {
    public FeatureDetector featureDetector;
    public Mat inputFrame;
    public MatOfKeyPoint SceneKeyPoints;

    public Params(FeatureDetector featureDetector, Mat inputFrame, MatOfKeyPoint SceneKeyPoints){
        this.featureDetector = featureDetector;
        this.inputFrame = inputFrame;
        this.SceneKeyPoints = SceneKeyPoints;
    }

    public Params(){

    }
}
