package com.bas_bk;

/**
 * Created by BAS_BK on 01.10.2016.
 */
public class Circle {
    private float ballRadius;
    private float ballX;
    private float ballY;
    private float ballXMax, ballXMin, ballYMax, ballYMin;
    private float xSpeed;
    private float ySpeed;

    public Circle(float ballRadius, float ballX, float ballY, float xSpeed, float ySpeed) {
        this.ballRadius = ballRadius;
        this.ballX = ballX;
        this.ballY = ballY;
        this.xSpeed = xSpeed;
        this.ySpeed = ySpeed;
    }

    public float getBallXMax() {
        return ballXMax;
    }

    public void setBallXMax(float ballXMax) {
        this.ballXMax = ballXMax;
    }

    public float getBallRadius() {
        return ballRadius;
    }

    public void setBallRadius(float ballRadius) {
        this.ballRadius = ballRadius;
    }

    public float getBallX() {
        return ballX;
    }

    public void setBallX(float ballX) {
        this.ballX = ballX;
    }

    public float getBallY() {
        return ballY;
    }

    public void setBallY(float ballY) {
        this.ballY = ballY;
    }

    public float getBallXMin() {
        return ballXMin;
    }

    public void setBallXMin(float ballXMin) {
        this.ballXMin = ballXMin;
    }

    public float getBallYMax() {
        return ballYMax;
    }

    public void setBallYMax(float ballYMax) {
        this.ballYMax = ballYMax;
    }

    public float getBallYMin() {
        return ballYMin;
    }

    public void setBallYMin(float ballYMin) {
        this.ballYMin = ballYMin;
    }

    public float getxSpeed() {
        return xSpeed;
    }

    public void setxSpeed(float xSpeed) {
        this.xSpeed = xSpeed;
    }

    public float getySpeed() {
        return ySpeed;
    }

    public void setySpeed(float ySpeed) {
        this.ySpeed = ySpeed;
    }
}
