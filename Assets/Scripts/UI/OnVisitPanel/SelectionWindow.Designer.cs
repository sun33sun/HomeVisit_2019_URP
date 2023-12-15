/****************************************************************************
 * 2023.12 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	public partial class SelectionWindow
	{
		[SerializeField] public RectTransform optionParent;
		[SerializeField] public UnityEngine.UI.Button btnConfirm;

		public void Clear()
		{
			optionParent = null;
			btnConfirm = null;
		}

		public override string ComponentName
		{
			get { return "SelectionWindow";}
		}
	}
}
