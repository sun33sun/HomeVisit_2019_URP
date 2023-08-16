using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using ProjectBase;
using QFramework;
using HomeVisit.UI;
using System.Collections;
using System.Linq;

namespace HomeVisit
{
	public class Questionnaire : MonoBehaviour
	{
		[SerializeField] Image imgScenarioCases;
		//选项信息
		[SerializeField] RectTransform parent;
		[SerializeField] List<RectTransform> rects = new List<RectTransform>();
		List<Image> imgs = new List<Image>();
		List<TextMeshProUGUI> tmps = new List<TextMeshProUGUI>();
		[SerializeField] Button btnLeft;
		[SerializeField] Button btnRight;
		[SerializeField] Button btnConfirm;
		//选择后使用的一些信息
		[SerializeField] Sprite[] sprites;
		[SerializeField] Color selectedColor;
		int selectedIndex = 2;
		List<string> strList = new List<string>() { "传统", "多孩", "单亲", "学习困难", "病残", "心理不健全", "行为偏差", "生活困难", "留守", "外来务工" };
		WaitForSeconds wait1 = new WaitForSeconds(1);
		//不可选提示
		[SerializeField] Image imgBuildTip;
		[SerializeField] Button btnCloseBuildTip;

		//外界获取家访孩子信息
		List<string> homeTypes = new List<string>() { "传统", "多孩", "单亲" };
		List<string> studentNames = new List<string>() { "张守光", "林光美", "秦彦威" };
		public string HomeType 
		{ 
			get 
			{
				if (selectedIndex > 2)
					return "";
				return homeTypes[selectedIndex];
			} 
		}
		public string StudentName 
		{ 
			get 
			{
				if (selectedIndex > 2)
					return "";
				return studentNames[selectedIndex];
			} 
		}

		private void OnEnable()
		{
			imgBuildTip.gameObject.SetActive(false);
		}

		private void OnDisable()
		{
			StopAllCoroutines();
			UIKit.ShowPanel<TopPanel>();
		}

		void Start()
		{
			for (int i = 0; i < rects.Count; i++)
			{
				imgs.Add(rects[i].GetComponent<Image>());
				tmps.Add(rects[i].GetComponentInChildren<TextMeshProUGUI>());
				tmps[i].text = strList[i];
			}
			btnLeft.onClick.AddListener(() => { StartCoroutine(OnLeftAsync()); });
			btnRight.onClick.AddListener(() => { StartCoroutine(OnRightAsync()); });
		}

		IEnumerator OnLeftAsync()
		{
			btnLeft.interactable = false;
			btnRight.interactable = false;
			btnConfirm.interactable = false;

			imgs[2].sprite = sprites[0];
			imgs[3].sprite = sprites[1];
			tmps[2].color = Color.white;
			tmps[3].color = selectedColor;
			rects[2].DOSizeDelta(new Vector2(377, 220), 0.99f);
			parent.DOLocalMoveX(-1233, 0.99f);
			yield return wait1;
			//重新加载数据
			selectedIndex += 1;
			if (selectedIndex >= strList.Count)
				selectedIndex = selectedIndex % strList.Count;
			imgs[2].sprite = sprites[1];
			imgs[3].sprite = sprites[0];
			tmps[2].color = selectedColor;
			tmps[3].color = Color.white;
			for (int i = 0; i < 5; i++)
			{
				int ringIndex = (selectedIndex - 2 + i)%strList.Count;
				ringIndex = ringIndex >= 0 ? ringIndex : strList.Count + ringIndex;
				print($"selectedIndex:{selectedIndex}  i:{i}  ringIndex:{ringIndex}");
				tmps[i].text = strList[ringIndex];
			}
			parent.localPosition = new Vector3(-856,140,0);
			rects[2].DOSizeDelta(new Vector2(480, 280), 0.99f);
			yield return wait1;

			btnLeft.interactable = true;
			btnRight.interactable = true;
			btnConfirm.interactable = true;
		}

		IEnumerator OnRightAsync()
		{
			btnLeft.interactable = false;
			btnRight.interactable = false;
			btnConfirm.interactable = false;

			imgs[2].sprite = sprites[0];
			imgs[1].sprite = sprites[1];
			tmps[2].color = Color.white;
			tmps[1].color = selectedColor;
			rects[2].DOSizeDelta(new Vector2(377, 220), 0.99f);
			parent.DOLocalMoveX(-479, 0.99f);
			yield return wait1;
			//重新加载数据
			selectedIndex -= 1;
			if (selectedIndex < 0)
				selectedIndex = strList.Count + selectedIndex;
			imgs[2].sprite = sprites[1];
			imgs[1].sprite = sprites[0];
			tmps[2].color = selectedColor;
			tmps[1].color = Color.white;
			for (int i = 0; i < 5; i++)
			{
				int ringIndex = (selectedIndex - 2 + i)%strList.Count;
				ringIndex = ringIndex >= 0 ? ringIndex : strList.Count + ringIndex;
				print($"selectedIndex:{selectedIndex}  i:{i}  ringIndex:{ringIndex}");
				tmps[i].text = strList[ringIndex];
			}
			parent.localPosition = new Vector3(-856, 140, 0);
			rects[2].DOSizeDelta(new Vector2(480, 280), 0.99f);
			yield return wait1;

			btnLeft.interactable = true;
			btnRight.interactable = true;
			btnConfirm.interactable = true;
		}
	}
}


