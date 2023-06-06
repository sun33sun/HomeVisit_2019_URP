using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using System;

namespace HomeVisit.UI
{
	public class ClothesPanelData : UIPanelData
	{
	}
	public partial class ClothesPanel : UIPanel
	{
		[SerializeField] List<ClothesTitle> bothClothes;
		[SerializeField] List<ClothesTitle> manClothes;
		[SerializeField] List<ClothesTitle> womanClothes;
		[SerializeField] Sprite[] spriteSex;

		DateTime startTime;
		DateTime endTime;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as ClothesPanelData ?? new ClothesPanelData();

			btnMan.onClick.AddListener(Man);
			btnWoman.onClick.AddListener(Woman);
			btnCloseClothes.onClick.AddListener(Hide);
			btnSubmit.onClick.AddListener(()=> 
			{
				imgTopBk.gameObject.SetActive(false);
				imgMidBk.gameObject.SetActive(false);
				imgConfirm.gameObject.SetActive(true);
			});
			btnConfirm.onClick.AddListener(Confirm);
			btnConfirmSubmit.onClick.AddListener(ConfirmSubmit);
		}

		void Man()
		{
			imgTeacher.sprite = spriteSex[0];
			for (int i = 0; i < manClothes.Count; i++)
			{
				manClothes[i].gameObject.SetActive(true);
			}
			for (int i = 0; i < womanClothes.Count; i++)
			{
				womanClothes[i].Reset();
				womanClothes[i].gameObject.SetActive(false);
			}
			for (int i = 0; i < bothClothes.Count; i++)
			{
				bothClothes[i].Reset();
			}
		}

		void Woman()
		{
			imgTeacher.sprite = spriteSex[1];
			for (int i = 0; i < manClothes.Count; i++)
			{
				womanClothes[i].gameObject.SetActive(true);
			}
			for (int i = 0; i < manClothes.Count; i++)
			{
				manClothes[i].Reset();
				manClothes[i].gameObject.SetActive(false);
			}
			for (int i = 0; i < bothClothes.Count; i++)
			{
				bothClothes[i].Reset();
			}
		}

		void Confirm()
		{
			btnMan.interactable = false;
			btnWoman.interactable = false;
			btnSubmit.transform.SetAsLastSibling();
			for (int i = 0; i < manClothes.Count; i++)
			{
				womanClothes[i].gameObject.SetActive(true);
				womanClothes[i].CheckTitle();
				womanClothes[i].SetInteractable(false);
			}
			for (int i = 0; i < manClothes.Count; i++)
			{
				manClothes[i].gameObject.SetActive(true);
				manClothes[i].CheckTitle();
				manClothes[i].SetInteractable(false);
			}
			for (int i = 0; i < bothClothes.Count; i++)
			{
				bothClothes[i].gameObject.SetActive(true);
				bothClothes[i].CheckTitle();
				bothClothes[i].SetInteractable(false);
			}
		}

		void ConfirmSubmit()
		{
			//生成实验报告
			int totalScore = 0;
			for (int i = 0; i < bothClothes.Count; i++)
				totalScore += bothClothes[i].GetScore();
			for (int i = 0; i < manClothes.Count; i++)
				totalScore += manClothes[i].GetScore();
			for (int i = 0; i < womanClothes.Count; i++)
				totalScore += womanClothes[i].GetScore();
			endTime = DateTime.Now;
			ScoreReportData data = new ScoreReportData()
			{
				strModule = "着装准备",
				strStart = startTime,
				strEnd = endTime,
				strScore = totalScore.ToString()
			};
			UIKit.GetPanel<TestReportPanel>().CreateScoreReport(data);

			UIKit.GetPanel<MainPanel>().NextVisitStepPanel();
			UIKit.OpenPanelAsync<OnVisitPanel>().ToAction().Start(this);
			Hide();
		}

		protected override void OnOpen(IUIData uiData = null)
		{

		}

		protected override void OnShow()
		{
			startTime = DateTime.Now;

			imgTopBk.gameObject.SetActive(true);
			imgMidBk.gameObject.SetActive(true);
			imgConfirm.gameObject.SetActive(false);
			imgTeacher.sprite = spriteSex[0];

			for (int i = 0; i < bothClothes.Count; i++)
				bothClothes[i].Reset();
			for (int i = 0; i < manClothes.Count; i++)
			{
				manClothes[i].gameObject.SetActive(true);
				manClothes[i].Reset();
			}
			for (int i = 0; i < womanClothes.Count; i++)
			{
				womanClothes[i].gameObject.SetActive(false);
				womanClothes[i].Reset();
			}

			btnConfirm.transform.SetAsLastSibling();
		}

		protected override void OnHide()
		{
		}

		protected override void OnClose()
		{
		}
	}
}
