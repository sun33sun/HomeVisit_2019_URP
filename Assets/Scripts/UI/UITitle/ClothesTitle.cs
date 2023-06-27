using UnityEngine;
using UnityEngine.UI;

namespace HomeVisit.UI
{
	public class ClothesTitle : MonoBehaviour, ITitle
	{
		[SerializeField] Toggle togClothes;
		[SerializeField] Image imgMask;
		[SerializeField] bool isRight;

		private void Awake()
		{
			togClothes = GetComponent<Toggle>();
			imgMask = transform.Find("imgMask").GetComponent<Image>();
			isRight = imgMask.sprite.name == "10交流提纲_正确";
		}

		public void CheckTitle()
		{
			imgMask.enabled = true;
		}

		public bool GetInteractable()
		{
			return togClothes.interactable;
		}

		public int GetScore()
		{
			return GetExamResult() ? 5 : 0;
		}

		public bool GetExamResult()
		{
			return isRight == togClothes.isOn;
		}

		public void Reset()
		{
			togClothes.isOn = false;
			imgMask.enabled = false;
			SetInteractable(true);
		}

		public void SetInteractable(bool newState)
		{
			togClothes.interactable = newState;
		}

		public void InitData(TitleData titleData)
		{
			throw new System.NotImplementedException();
		}
	}

}


