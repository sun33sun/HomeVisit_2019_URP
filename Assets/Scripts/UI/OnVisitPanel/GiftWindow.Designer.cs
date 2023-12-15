/****************************************************************************
 * 2023.12 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	public partial class GiftWindow
	{
		[SerializeField] public UnityEngine.UI.Image imgExpressGratitude;
		[SerializeField] public UnityEngine.UI.Button btnRefuse;
		[SerializeField] public UnityEngine.UI.Button btnAccept;
		[SerializeField] public UnityEngine.UI.Image imgGratitudeTip;
		[SerializeField] public UnityEngine.UI.Button btnConfirmGratitudeTip;

		public void Clear()
		{
			imgExpressGratitude = null;
			btnRefuse = null;
			btnAccept = null;
			imgGratitudeTip = null;
			btnConfirmGratitudeTip = null;
		}

		public override string ComponentName
		{
			get { return "GiftWindow";}
		}
	}
}
