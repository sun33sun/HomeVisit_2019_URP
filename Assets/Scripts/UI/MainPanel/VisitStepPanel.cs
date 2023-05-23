/****************************************************************************
 * 2023.5 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using TMPro;

//31 + 18 * 5 + 32 * 4 + 30
//31 + 90 + 128 + 30 = 279


namespace HomeVisit.UI
{
	public partial class VisitStepPanel : MonoBehaviour
	{
		int stepUIIndex = 0;
		int tmpIndex = 0;

		private void Start()
		{
			if (objSteps.Length < 1)
				return;
			for (int i = 0; i < stepUIs.Length; i++)
			{
				int index = i;
				stepUIs[i].btnStep.onClick.AddListener(() => { objSteps[index].SetActive(!objSteps[index].activeInHierarchy); });
			}
		}

		public void InitState()
		{
			stepUIIndex = 0;
			imgProgressBar.sprite = stepUIs[0].spriteProgressBar;
			for (int i = 0; i < stepUIs.Length; i++)
			{
				stepUIs[i].img.sprite = stepUIs[i].spriteD;
			}
			for (int i = 1; i < stepUIs.Length; i++)
				stepUIs[i].btnStep.interactable = false;
		}

		public void NextStep()
		{
			if (stepUIIndex >= stepUIs.Length - 1)
			{
				Debug.LogError("StepUIIndex Out Of Bounds : " + stepUIIndex);
				return;
			}
			stepUIs[stepUIIndex].img.sprite = stepUIs[stepUIIndex].spriteC;
			stepUIIndex++;
			stepUIs[stepUIIndex].img.sprite = stepUIs[stepUIIndex].spriteH;
			
			imgProgressBar.sprite = stepUIs[stepUIIndex].spriteProgressBar;
			stepUIs[stepUIIndex].btnStep.interactable = true;
		}

		public void NextTmp()
		{
			if (tmpList.Count < 1)
			{
				Debug.LogError("tmpIndex Out Of Bonds : " + tmpIndex);
				return;
			}
			tmpIndex++;
			tmpList[tmpIndex].fontStyle = TMPro.FontStyles.Bold;
			tmpList[tmpIndex].transform.Find("imgSelected").GetComponent<Image>().enabled = true;
		}

		public void StartTMP()
		{
			tmpIndex = 0;
			tmpList[tmpIndex].fontStyle = TMPro.FontStyles.Bold;
			tmpList[tmpIndex].transform.Find("imgSelected").GetComponent<Image>().enabled = true;
		}
	}
}