using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TogOutline : MonoBehaviour
{
	public Toggle tog;
	public Image imgBk;
	public Image imgCk;
	public Sprite[] sprites;
	public Image imgIsRight;
	public TextMeshProUGUI tmpDescribe;

	public void ShowState(int state)
	{
		imgIsRight.gameObject.SetActive(true);
		tog.interactable = false;
		switch (state)
		{
			case -1:
				imgBk.color = Color.red;
				imgCk.color = Color.red;
				tmpDescribe.color = Color.red;
				imgIsRight.sprite = sprites[1];
				break;
			case 0:
				imgBk.color = Color.white;
				imgCk.color = Color.white;
				tmpDescribe.color = Color.white;
				break;
			case 1:
				imgBk.color = Color.green;
				imgCk.color = Color.green;
				tmpDescribe.color = Color.green;
				imgIsRight.sprite = sprites[0];
				break;
			default:
				break;
		}
	}

}

