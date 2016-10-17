package com.bas_bk;

import org.lwjgl.*;
import org.lwjgl.glfw.*;
import org.lwjgl.opengl.*;

import java.nio.IntBuffer;

import static org.lwjgl.glfw.Callbacks.*;
import static org.lwjgl.glfw.GLFW.*;
import static org.lwjgl.opengl.GL11.*;
import static org.lwjgl.system.MemoryUtil.*;

public class HelloWorld {
    float PI = 3.1459265f;

    int WIDTH = 640;
    int HEIGHT = 640;
    static Circle[] c = new Circle[3];
    int refreshMillis = 20;

    //clipping area
    double clipAreaXLeft, clipAreaXRight, clipAreaYBottom, clipAreaYTop;

    // The window handle
    private long window;

    public void run() throws InterruptedException {
        System.out.println("Hello LWJGL " + Version.getVersion() + "!");

        try {
            init();
            loop();

            // Free the window callbacks and destroy the window
            glfwFreeCallbacks(window);
            glfwDestroyWindow(window);
        } finally {
            // Terminate GLFW and free the error callback
            glfwTerminate();
            glfwSetErrorCallback(null).free();
        }
    }

    private void init() {
        // Setup an error callback. The default implementation
        // will print the error message in System.err.
        GLFWErrorCallback.createPrint(System.err).set();

        // Initialize GLFW. Most GLFW functions will not work before doing this.
        if ( !glfwInit() )
            throw new IllegalStateException("Unable to initialize GLFW");

        // Configure our window
        glfwWindowHint(GLFW_VISIBLE, GLFW_FALSE); // the window will stay hidden after creation
        glfwWindowHint(GLFW_RESIZABLE, GLFW_TRUE); // the window will be resizable


        // Create the window
        window = glfwCreateWindow(WIDTH, HEIGHT, "Hello World!", NULL, NULL);
        if ( window == NULL )
            throw new RuntimeException("Failed to create the GLFW window");

        // Setup a key callback. It will be called every time a key is pressed, repeated or released.
        glfwSetKeyCallback(window, (window, key, scancode, action, mods) -> {
            if ( key == GLFW_KEY_ESCAPE && action == GLFW_RELEASE )
                glfwSetWindowShouldClose(window, true); // We will detect this in our rendering loop
        });

        // Get the resolution of the primary monitor
        GLFWVidMode vidmode = glfwGetVideoMode(glfwGetPrimaryMonitor());
        // Center our window
        glfwSetWindowPos(
                window,
                (vidmode.width() - WIDTH) / 2,
                (vidmode.height() - HEIGHT) / 2
        );

        glfwMakeContextCurrent(window);

        glfwSwapInterval(1);

        glfwShowWindow(window);

    }

    private void loop() throws InterruptedException {
        GL.createCapabilities();
        glClearColor(1.0f, 1.0f, 1.0f, 1.0f);
        glMatrixMode(GL_PROJECTION);
        glLoadIdentity();
        while ( !glfwWindowShouldClose(window) ) {
             // clear the framebuffer
            IntBuffer w = BufferUtils.createIntBuffer(1);
            IntBuffer h = BufferUtils.createIntBuffer(1);
            glfwGetWindowSize(window, w, h);
            reshape(w.get(), h.get());
            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
            for (int i = 0; i < c.length; i++) {

                glMatrixMode(GL_MODELVIEW);
                glLoadIdentity();
                float ballX = c[i].getBallX();
                float ballY = c[i].getBallY();
                float ballXMax = c[i].getBallXMax();
                float ballXMin = c[i].getBallXMin();
                float ballYMax = c[i].getBallYMax();
                float ballYMin = c[i].getBallYMin();
                float xSpeed = c[i].getxSpeed();
                float ySpeed = c[i].getySpeed();

                glTranslatef(ballX, ballY, 0.0f);
                glBegin(GL_TRIANGLE_FAN);
                glColor3f(0.0f, 0.5f, 0.0f);
                switch (i) {
                    case 0: glColor3f(0.0f, 1.0f, 0.0f);
                            break;
                    case 1: glColor3f(1.0f, 0.0f, 0.0f);
                            break;
                    case 2: glColor3f(0.0f, 0.0f, 1.0f);
                            break;
                }
                glVertex2f(0.0f, 0.0f);
                int numSegments = 100;
                float angle;
                for (int j = 0; j <= numSegments; j++) {
                    angle = j * 2.0f * PI / numSegments;
                    float x = (float) (Math.cos(angle) * c[i].getBallRadius());
                    float y = (float) (Math.sin(angle) * c[i].getBallRadius());
                    glVertex2f(x, y);
                }
                glEnd();
                ballX += c[i].getxSpeed();
                ballY += c[i].getySpeed();
                if (ballX > ballXMax) {
                    ballX = ballXMax;
                    xSpeed = -xSpeed;
                } else if (ballX < ballXMin) {
                    ballX = ballXMin;
                    xSpeed = -xSpeed;
                }
                if (ballY > ballYMax) {
                    ballY = ballYMax;
                    ySpeed = -ySpeed;
                } else if (ballY < ballYMin) {
                    ballY = ballYMin;
                    ySpeed = -ySpeed;
                }
                for (int p = 0; p < c.length; p++){
                    if (p != i){
                        if (Math.sqrt(Math.pow((c[p].getBallX() - ballX), 2) + Math.pow(c[p].getBallY() - ballY, 2)) < c[i].getBallRadius() + c[p].getBallRadius()){
                            float tmpxS = xSpeed;
                            float tmpyS = ySpeed;
                            xSpeed = c[p].getxSpeed();
                            ySpeed = c[p].getySpeed();
                            c[p].setxSpeed(tmpxS);
                            c[p].setySpeed(tmpyS);
                        }
                    }
                }
                glfwPollEvents();
                c[i].setBallX(ballX);
                c[i].setBallY(ballY);
                c[i].setxSpeed(xSpeed);
                c[i].setySpeed(ySpeed);



            }
            glfwSwapBuffers(window);
        }

    }
void reshape(int width, int height) throws InterruptedException {
    if (height == 0) height = 1;
    float aspect = (float) width/height;

    glViewport(0,0, width, height);

    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();

    if (width >= height){
        clipAreaXLeft = -1.0 * aspect;
        clipAreaXRight = 1.0 * aspect;
        clipAreaYBottom = -1.0;
        clipAreaYTop = 1.0;
    }
    else {
        clipAreaXLeft = -1.0;
        clipAreaXRight = 1.0;
        clipAreaYBottom = -1.0 / aspect;
        clipAreaYTop = 1.0 / aspect;
    }
    glOrtho(clipAreaXLeft, clipAreaXRight, clipAreaYBottom, clipAreaYTop, -1.0, 1.0);
    for (int i = 0; i < c.length; i++) {
        float ballRadius = c[i].getBallRadius();
        c[i].setBallXMin((float) clipAreaXLeft + ballRadius);
        c[i].setBallXMax((float) clipAreaXRight - ballRadius);
        c[i].setBallYMin((float) clipAreaYBottom + ballRadius);
        c[i].setBallYMax((float) clipAreaYTop - ballRadius);
    }
    Thread.sleep(refreshMillis);
}
    public static void main(String[] args) throws InterruptedException {

        c[0] = new Circle(0.3f, -0.2f, -0.2f, 0.02f, 0.007f);
        c[1] = new Circle(0.2f, 0.5f, 0.5f, -0.01f, 0.01f);
        c[2] = new Circle(0.2f, -0.2f, 0.4f, 0.03f, -0.007f);
        new HelloWorld().run();
    }

}