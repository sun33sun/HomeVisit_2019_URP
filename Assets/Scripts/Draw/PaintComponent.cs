using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PaintComponent
{

    public PaintComponent()
    {

    }



    // 创建对象时，我们这里，需要得刀RawImage的 Texture
    public PaintComponent(RawImage rImage, Texture2D texture)
    {
        setRawImage(rImage);
        setTexture(texture);
        prepareCpnt();
    }
    private RawImage rawImage;
    public void setRawImage(RawImage image)
    {
        this.rawImage = image;
    }
    public RawImage getRawImage()
    {
        return this.rawImage;
    }

    private Texture2D texture;

    public void setTexture(Texture2D tex)
    {
        this.texture = tex;
    }

    public Texture2D getTexture()
    {
        return this.texture;
    }

    private IDraw drawType;
    public void setDraw(IDraw draw)
    {

        // 设置绘画接口，通过设置不同的绘制接口实例来达到不同的功能
        this.drawType = draw;
    }

    public void prepareCpnt()
    {
        if (this.rawImage != null && this.texture != null)
        {
            this.rawImage.texture = this.texture;
        }
    }

    // 清除功能
    public void clear()
    {

        // 原理跟其他的绘制一样，也是通过IDraw接口的不同实例来实现清除功能
        IDraw clearType = new ClearType();
        clearType.paint(this.texture, new Vector3(0, 0, 0), new Color(0, 0, 0, 0));
    }
    // 绘制功能，调用之前设置的drawType来进行绘制
    public void paint(Vector3 vec, Color color)
    {
        if (this.drawType != null)
        {
            this.drawType.paint(this.texture, vec, color);
        }
    }
}