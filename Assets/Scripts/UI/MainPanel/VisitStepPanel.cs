/****************************************************************************
 * 2023.5 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	public partial class VisitStepPanel : UIElement
	{
		int stepUIIndex;

		private void Start()
		{
			if (imgSteps == null || imgSteps.Length < 1)
				return;
			for (int i = 0; i < stepUIs.Length; i++)
			{
				int index = i;
				stepUIs[i].GetComponent<Button>().onClick.AddListener(()=> { imgSteps[index].SetActive(imgSteps[index].activeInHierarchy); });
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
		}

		public void NextStep()
		{
			if (stepUIIndex > stepUIs.Length - 2)
			{
				Debug.LogError("StepUIIndex Out Of Bounds : " + stepUIIndex);
				return;
			}
			stepUIs[stepUIIndex].img.sprite = stepUIs[stepUIIndex].spriteC;

			stepUIIndex++;
			stepUIs[stepUIIndex].img.sprite = stepUIs[stepUIIndex].spriteH;
			
			imgProgressBar.sprite = stepUIs[stepUIIndex].spriteProgressBar;
		}
	}
}