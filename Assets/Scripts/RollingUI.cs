using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RollingUI : MonoBehaviour
{
    public RectTransform parent;
    public Button btnLeft;
    public Button btnRight;
    public List<RectTransform> rects = new List<RectTransform>();
    public Sprite[] sprites;

    //根据rects获取
    List<TextMeshProUGUI> tmps = new List<TextMeshProUGUI>();
    List<Image> imgs = new List<Image>();
    List<string> strList = new List<string>() { "传统", "多孩", "单亲", "学习困难", "病残", "心理不健全", "行为偏差", "生活困难", "留守", "外来务工" };
    int selectIndex = 2;
    WaitForSeconds wait1 = new WaitForSeconds(1);

    void Start()
    {
        //初始化
		for (int i = 0; i < rects.Count; i++)
		{
			tmps.Add(rects[i].GetComponentInChildren<TextMeshProUGUI>());
            imgs.Add(rects[i].GetComponent<Image>());
			tmps[i].text = strList[i];
        }
        btnLeft.onClick.AddListener(() => { StartCoroutine(OnLeftAsync()); });
        btnRight.onClick.AddListener(() => { StartCoroutine(OnRightAsync()); });
    }

	IEnumerator OnLeftAsync()
	{
        btnLeft.interactable = false;
        btnRight.interactable = false;

        imgs[2].sprite = sprites[0];
        imgs[3].sprite = sprites[1];
        rects[2].DOSizeDelta(new Vector2(377, 220), 0.99f);
        parent.DOLocalMoveX(-377, 0.99f);
        yield return wait1;
        //重新加载数据
        selectIndex += 1;
        imgs[2].sprite = sprites[1];
        imgs[3].sprite = sprites[0];
		for (int i = 0; i < 5; i++)
		{
            int ringIndex = selectIndex - 2 + i;
            ringIndex = ringIndex >= 0 ? ringIndex : strList.Count + ringIndex; 
            tmps[i].text = strList[ringIndex];
		}
        parent.localPosition = Vector3.zero;
        rects[2].DOSizeDelta(new Vector2(480, 280), 0.99f);
        yield return wait1;

        btnLeft.interactable = true;
        btnRight.interactable = true;
    }

    IEnumerator OnRightAsync()
	{
        btnLeft.interactable = false;
        btnRight.interactable = false;

        imgs[2].sprite = sprites[0];
        imgs[3].sprite = sprites[1];
        rects[2].DOSizeDelta(new Vector2(377, 220), 0.99f);
        parent.DOLocalMoveX(377, 0.99f);
        yield return wait1;
        //重新加载数据
        selectIndex -= 1;
        imgs[2].sprite = sprites[1];
        imgs[3].sprite = sprites[0];
        for (int i = 0; i < 5; i++)
        {
            int ringIndex = selectIndex - 2 + i;
            ringIndex = ringIndex >= 0 ? ringIndex : strList.Count + ringIndex;
            tmps[i].text = strList[ringIndex];
        }
        parent.localPosition = Vector3.zero;
        rects[2].DOSizeDelta(new Vector2(480, 280), 0.99f);
        yield return wait1;

        btnLeft.interactable = true;
        btnRight.interactable = true;
    }
}
