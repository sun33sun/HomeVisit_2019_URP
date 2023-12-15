/****************************************************************************
 * 2023.12 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections;

namespace HomeVisit.UI
{
	public partial class GiftWindow : UIElement
	{
		bool isRefuseGift = false;

		private void Awake()
		{
			btnRefuse.onClick.AddListener(() =>
			{
				imgExpressGratitude.gameObject.SetActive(false);
				isRefuseGift = true;
			});
			btnAccept.onClick.AddListener(() =>
			{
				imgGratitudeTip.gameObject.SetActive(true);
				imgExpressGratitude.gameObject.SetActive(false);
			});
			btnConfirmGratitudeTip.onClick.AddListener(() =>
			{
				imgGratitudeTip.gameObject.SetActive(false);
				imgExpressGratitude.gameObject.SetActive(true);
			});
		}

		public IEnumerator ShowExpressGratitude()
		{
			gameObject.SetActive(true);
			imgExpressGratitude.gameObject.SetActive(true);
			while (!isRefuseGift)
			{
				yield return null;
			}
		}

		protected override void OnBeforeDestroy()
		{
		}
	}
}