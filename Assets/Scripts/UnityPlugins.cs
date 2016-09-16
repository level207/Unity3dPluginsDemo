using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine.UI;

#if UNITY_STANDALONE || UNITY_EDITOR
using UnityPluginsCLR;
#endif

/* 
平台定义
UNITY_EDITOR	编辑器调用。
UNITY_STANDALONE_OSX	专门为Mac OS（包括Universal，PPC和Intelarchitectures）平台的定义。
UNITY_DASHBOARD_WIDGET	Mac OS Dashboard widget (Mac OS仪表板小部件)。
UNITY_STANDALONE_WIN	Windows。
UNITY_STANDALONE_LINUX	Linux的独立的应用程序。
UNITY_STANDALONE	独立的平台（Mac，Windows或Linux）。
UNITY_WEBPLAYER	网页播放器（包括Windows和Mac Web播放器可执行文件）。
UNITY_WII	Wii游戏机平台。
UNITY_IPHONE	iPhone平台。
UNITY_ANDROID	Android平台。
UNITY_PS3	PlayStation 3。
UNITY_XBOX360	Xbox 360。
UNITY_NACL	谷歌原生客户端（使用这个必须另外使用UNITY_WEBPLAYER）。
UNITY_FLASH	Adobe Flash。
也可以判断Unity版本，目前支持的版本
UNITY_2_6	平台定义为主要版本的Unity 2.6。
UNITY_2_6_1	平台定义的特定版本1的主要版本2.6。
UNITY_3_0	平台定义为主要版本的Unity 3.0。
UNITY_3_0_0	平台定义的特定版本的Unity 3.0 0。
UNITY_3_1	平台定义为主要版本的Unity 3.1。
UNITY_3_2	平台定义为主要版本的Unity 3.2。
UNITY_3_3	平台定义为主要版本的Unity 3.3。
UNITY_3_4	平台定义为主要版本的Unity 3.4。
UNITY_3_5	平台定义为主要版本的Unity 3.5。
UNITY_4_0	平台定义为主要版本的Unity 4.0。
UNITY_4_0_1	主要版本4.0.1统一的平台定义。
UNITY_4_1	平台定义为主要版本的Unity 4.1。
 */

public class UnityPlugins : MonoBehaviour {

#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
    [DllImport("UnityPluginsNative")]
    private extern static IntPtr UnityCallCppNativeAdd(int a, int b);
#endif

#if UNITY_IOS || UNITY_IPHONE
    [DllImport("__Internal")]
    public static extern void UnityCallIOSPrint();
#endif

#if UNITY_STANDALONE_OSX && !UNITY_EDITOR
    [DllImport("UnityPluginsMacOSX")]
    private static extern void UnityCallMacOSXPrint();
#endif

    public Text gTextOne;
    public Text gTextTwo;
    public Text gTextTre;
    public Text gTextFor;
    public Text gTextFiv;

    // Use this for initialization
    void Start () {

        Type type = Type.GetType("Mono.Runtime");
        if (type != null)
        {
            MethodInfo info = type.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static);

            if (info != null)
            {
                Debug.Log(info.Invoke(null, null));
            }
        }

        //Invoke("CheckBackButton", 1.0f);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.backButtonLeavesApp || Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    private void CheckBackButton()
    {
        if (Input.backButtonLeavesApp || Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OnClickButtonCppCLR()
    {
        //CLR托管代码可以支持Standalone三个平台调用，且不需要DLLimport动态库
        Debug.Log("OnClickButtonCppCLR");

#if UNITY_STANDALONE || UNITY_EDITOR
        try
        {
            CLR4orUnity unityPluginsCLR = new CLR4orUnity();

            int sum = 0;

            //返回两数相加的和
            sum = unityPluginsCLR.UnityCallCppCLRAdd(10, 20);

            gTextOne.text = sum.ToString();

            Debug.Log(sum);

        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return;
        }
#endif
    }

    public void OnClickButtonCppNative()
    {
        Debug.Log("OnClickButtonCppNative");

#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
        try
        {            
            //这个原本是要做一个加法，后来调试返回字符串，现在返回一个helloworld
            string str = Marshal.PtrToStringAnsi(UnityCallCppNativeAdd(20, 30));

            gTextTwo.text = str;
            Debug.Log(str);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return;
        }
#endif
    }

    public void OnClickButtonAndroid()
    {
        Debug.Log("OnClickButtonAndroid");

#if UNITY_ANDROID
        try
        {
            //返回两数相加的和
            /* 调用静态方法 */
            //AndroidJavaClass unityPluginsAndroid = new AndroidJavaClass("com.example.mylibrary.UnityPluginsJava"); //这种用法是OK的

            AndroidJavaObject unityPluginsAndroid = new AndroidJavaObject("com.example.mylibrary.UnityPluginsJava"); //这种用法也是OK的
            int sum = unityPluginsAndroid.CallStatic<int>("UnityCallJavaAdd", 20, 50);

            Debug.Log("sum = " + sum);

            /* 调用动态方法 */
            AndroidJavaObject jo = new AndroidJavaObject("com.example.mylibrary.UnityPluginsJava");
            int sub = jo.Call<int>("UnityCallJavaSub", 100, 50);

            Debug.Log("sub = " + sub);

            gTextTre.text = sub.ToString();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return;
        }
#endif
    }

    public void OnClickButtonIOS()
    {
        Debug.Log("OnClickButtonIOS");

#if UNITY_IPHONE || UNITY_IOS
        try
        {
            //IOS的返回值比较麻烦，现在只知道调用成功，但是没有返回值，返回值部分还需要花时间研究
            UnityCallIOSPrint();
            gTextFor.text = "UnityCallIOSPrint OK";
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return;
        }
#endif
    }

    public void OnClickButtonMacOSX()
    {
        Debug.Log("OnClickButtonMacOSX");

#if UNITY_STANDALONE_OSX && !UNITY_EDITOR
        try
        {
            //MacOSX的返回值现在也没有做好
            UnityCallMacOSXPrint();
            gTextFiv.text = "UnityCallMacOSXPrint OK";
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return;
        }
#endif
    }
}
