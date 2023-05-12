using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDraw
{
    void paint(Texture2D text, Vector3 vec, Color color);
}

public class DrawLineType : IDraw
{
    public void paint(Texture2D text, Vector3 vec, Color color)
    {
        if (text != null && vec != null && color != null)
        {
            for (int x = -2; x < 3; x++)
            {
                for (int y = -2; y < 3; y++)
                {
                    if ((x * x + y * y) <= 25 && (int)vec.x + x < 350)
                    {
                        text.SetPixel((int)vec.x + x, (int)vec.y + y, color); // 其实就是在鼠标指向的像素赋值
                    }
                }
            }
            text.Apply(); // 应用
        }
    }
}

public class CleanLineType : IDraw
{
    private Color clearColor = new Color(0, 0, 0, 0); // 其实就是把鼠标指向的Texture的颜色，赋值成透明的零色【瞎取得名字 哈哈哈】
    public void paint(Texture2D text, Vector3 vec, Color color)
    {
        if (text != null && vec != null && color != null)
        {
            for (int x = -10; x < 11; x++)
            {
                for (int y = -10; y < 11; y++)
                {
                    if ((x * x + y * y) <= 25 && (int)vec.x + x < 410)
                    {
                        text.SetPixel((int)vec.x + x, (int)vec.y + y, clearColor);
                    }
                }
            }
            text.Apply();
        }
    }
}

public class ClearType : IDraw
{
    public void paint(Texture2D text, Vector3 vec, Color color)
    {
        if (text != null)
        {
            for (int i = 0; i < text.height; i++)
            {
                for (int k = 0; k < text.width; k++)
                {
                    text.SetPixel(k, i, color); // 全部赋值成零色
                }
            }
            text.Apply();
        }
    }
}