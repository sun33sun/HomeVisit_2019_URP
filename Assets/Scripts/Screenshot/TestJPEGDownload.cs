using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public static class TestJPEGDownload
{
    [DllImport("__Internal")]
    private static extern void ImageDownloader(string str, string fn);

    public static void DownloadImage(byte[] imageData, string imageFileName = "newpic")
    {
        if (imageData != null)
        {
            Debug.Log("Downloading..." + imageData + "++++++++++++" + imageFileName);
#if UNITY_EDITOR
            File.WriteAllBytes(Application.streamingAssetsPath + "/" + imageFileName, imageData);
#elif UNITY_WEBGL
            ImageDownloader(System.Convert.ToBase64String(imageData), imageFileName);
#endif
        }
        else
        {
            Debug.LogError("!!!!!!!!!!!!!!!!!!!!!");
        }
    }
}