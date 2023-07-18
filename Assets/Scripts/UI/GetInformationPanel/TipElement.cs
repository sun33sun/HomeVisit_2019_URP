/****************************************************************************
 * 2023.7 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using DG.Tweening;

namespace HomeVisit.UI
{
	public partial class TipElement : UIElement
	{

		private void Start()
		{
		}

		protected override void OnBeforeDestroy()
		{
		}

		public override void Hide()
		{
			imgTip.transform.DORewind();
		}

		public override void Show()
		{
			Sequence show = DOTween.Sequence();
			show.Append(imgTip.transform.DOLocalMoveX(760, 1));
			show.AppendInterval(2);
			show.Append(imgTip.transform.DOLocalMoveX(1160, 1));
		}
	}
}