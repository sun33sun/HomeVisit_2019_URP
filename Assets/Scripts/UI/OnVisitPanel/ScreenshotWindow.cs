/****************************************************************************
 * 2023.12 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using HomeVisit.Screenshot;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace HomeVisit.UI
{
	public partial class ScreenshotWindow : UIElement
	{
		public bool ScreenshotTaken { get; private set; }

		float animTime = 0.5f;

		private void Awake()
		{
			btnCloseScreenshot.onClick.AddListener(Hide);

		}

		protected override void OnBeforeDestroy()
		{
		}

		public override void Show()
		{
			if(ScreenshotTaken)
				base.Show();
		}

		public async void Screenshot(Vector3 targetPos)
		{
			ScreenshotTaken = true;
			ScreenshotManager.Instance.CaptureScreenshot(rawImgScreenshot).Forget();
			await UniTask.Yield();
			await UniTask.Yield();
			UIRoot.Instance.GraphicRaycaster.enabled = false;
			imgScreenshotEffect.texture = rawImgScreenshot.texture;
			UIKit.GetPanel<TestReportPanel>().ReloadScreenshot(rawImgScreenshot.texture);
			imgScreenshotEffect.gameObject.SetActive(true);
			imgScreenshotEffect.transform.localScale = Vector3.one;
			imgScreenshotEffect.transform.localPosition = Vector3.zero;
			imgScreenshotEffect.transform.DOLocalMove(targetPos, animTime);
			await imgScreenshotEffect.transform.DOScale(Vector3.zero, animTime).AsyncWaitForCompletion();
			UIRoot.Instance.GraphicRaycaster.enabled = true;
		}

		private void OnEnable()
		{
			imgScreenshotEffect.gameObject.SetActive(false);
		}
	}
}