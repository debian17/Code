#include <jni.h>
#include <string>
#include <org>

using namespace std;

extern "C"
jstring Java_com_example_ndktest_MainActivity_stringFromJNI(JNIEnv *env, jobject /* this */) {
    string hello = "Hello from C++";
    return env->NewStringUTF(hello.c_str());
}

extern "C"
jstring Java_com_example_ndktest_MainActivity_somestringFromJNI(JNIEnv *env, jobject /* this */) {
string hello = "My string";
return env->NewStringUTF(hello.c_str());
}