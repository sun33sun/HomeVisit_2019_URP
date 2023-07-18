/****************************************************************************
 * 2023.7 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	public partial class TipElement
	{
		[SerializeField] public UnityEngine.UI.Image imgTip;
		[SerializeField] public TMPro.TextMeshProUGUI tmpTip;

		public void Clear()
		{
			imgTip = null;
			tmpTip = null;
		}

		public override string ComponentName
		{
			get { return "TipElement";}
		}
	}
}
