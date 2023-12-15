/****************************************************************************
 * 2023.12 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace HomeVisit.UI
{
	public partial class ScreenshotWindow
	{
		[SerializeField] public UnityEngine.UI.Image imgScreenshot;
		[SerializeField] public UnityEngine.UI.Button btnCloseScreenshot;
		[SerializeField] public UnityEngine.UI.RawImage rawImgScreenshot;
		[SerializeField] public UnityEngine.UI.RawImage imgScreenshotEffect;

		public void Clear()
		{
			imgScreenshot = null;
			btnCloseScreenshot = null;
			rawImgScreenshot = null;
			imgScreenshotEffect = null;
		}

		public override string ComponentName
		{
			get { return "ScreenshotWindow";}
		}
	}
}
