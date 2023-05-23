/****************************************************************************
 * 2023.5 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;
using TMPro;
using System.Collections.Generic;

namespace HomeVisit.UI
{
	public partial class VisitStepPanel
	{
		[SerializeField] 
		public UnityEngine.UI.Image imgProgressBar;
		[SerializeField]
		public Sprite spriteProgress;
		[SerializeField]
		public StepUI[] stepUIs;
		[SerializeField]
		public GameObject[] objSteps;
		[SerializeField]
		public List<TextMeshProUGUI> tmpList;
	}
}
