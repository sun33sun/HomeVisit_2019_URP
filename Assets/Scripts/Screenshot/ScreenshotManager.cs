using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectBase;
using Cysharp.Threading.Tasks;
using QFramework;

namespace HomeVisit.Screenshot
{
    public class ScreenshotManager : SingletonMono<ScreenshotManager>
    {
        //public Texture2D screenShot;
        //public Image image;
        public Sprite testimage;

        bool isScreenshot = false;
        public void CaptureScreenshot(RawImage rawImage)
		{
            if (isScreenshot) 
                return;
            CaptureScreenshotAsync(rawImage).Forget();
        }

        public async UniTaskVoid CaptureScreenshotAsync(RawImage rawImage)
        {
            isScreenshot = true;
            UIRoot.Instance.gameObject.SetActive(false);
            // Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24,false);
            // //screenShot.ReadPixels(rect, 0, 0);
            // screenShot.Apply();
            // byte[] bytes = screenShot.EncodeToJPG(); 
            // string filename = Application.dataPath + "Screenshot2.png";

            await UniTask.WaitForEndOfFrame(this);
            Rect rect = new Rect(0, 0, Screen.width, Screen.height);
            // 先创建一个的空纹理，大小可根据实现需要来设置    
            Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
            // 读取屏幕像素信息并存储为纹理数据
            screenShot.ReadPixels(rect, 0, 0);
            screenShot.Apply();
            rawImage.texture = screenShot;
            // 然后将这些纹理数据，成一个png图片文件    
            //byte[] bytes = screenShot.EncodeToPNG();
            //TestJPEGDownload.DownloadImage(bytes, "测试图片.png");
            UIRoot.Instance.gameObject.SetActive(true);
            isScreenshot = false;
		}
    }
}

