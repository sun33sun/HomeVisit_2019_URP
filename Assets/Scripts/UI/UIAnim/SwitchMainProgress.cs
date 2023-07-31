using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeVisit.UI
{
	public class SwitchMainProgress : MonoBehaviour
	{
		MainPanel mainPanel;

		private void OnEnable()
		{
			if (mainPanel == null)
				mainPanel = UIKit.GetPanel<MainPanel>();
			mainPanel?.SetProgress(false);
		}

		private void OnDisable()
		{
			if (mainPanel == null)
				mainPanel = UIKit.GetPanel<MainPanel>();
			mainPanel?.SetProgress(true);
		}
	}
}

