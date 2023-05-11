/****************************************************************************
 * 2023.5 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

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
		public GameObject[] imgSteps;

		public void Clear()
		{
			imgProgressBar = null;
			stepUIs = null;
		}

		public override string ComponentName
		{
			get { return "CommunicationOutlinePanel";}
		}
	}
}
