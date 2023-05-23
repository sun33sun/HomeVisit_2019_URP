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
		[SerializeField] List<ClothesTitle> clothesList;
		[SerializeField] List<Sprite> spriteSex;

		DateTime startTime;
		DateTime endTime;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as ClothesPanelData ?? new ClothesPanelData();

			btnMan.onClick.AddListener(Man);
			btnWoman.onClick.AddListener(Woman);
			btnCloseClothes.onClick.AddListener(Hide);
			btnSubmit.onClick.AddListener(() =>
			{
				imgTopBk.gameObject.SetActive(false);
				imgMidBk.gameObject.SetActive(false);
				imgConfirm.gameObject.SetActive(true);
			});
			btnConfirm.onClick.AddListener(Confirm);

			for (int i = 0; i < clothesList.Count; i++)
			{
				int index = i;
				clothesList[i].togClothes.onValueChanged.AddListener((isOn) =>
				{
					clothesList[index].imgMask.gameObject.SetActive(isOn);
				});
			}
		}

		void Man()
		{
			imgTeacher.sprite = spriteSex[0];
			for (int i = 0; i < clothesList.Count; i++)
			{
				clothesList[i].togClothes.isOn = false;
				switch (clothesList[i].state)
				{
					case ClothesTitle.ClothesKind.Man:
						clothesList[i].gameObject.SetActive(true);
						break;
					case ClothesTitle.ClothesKind.Woman:
						clothesList[i].gameObject.SetActive(false);
						break;
					default:
						break;
				}
			}
		}

		void Woman()
		{
			imgTeacher.sprite = spriteSex[1];
			for (int i = 0; i < clothesList.Count; i++)
			{
				clothesList[i].togClothes.isOn = false;
				switch (clothesList[i].state)
				{
					case ClothesTitle.ClothesKind.Man:
						clothesList[i].gameObject.SetActive(false);
						break;
					case ClothesTitle.ClothesKind.Woman:
						clothesList[i].gameObject.SetActive(true);
						break;
					default:
						break;
				}
			}
		}

		void Confirm()
		{
			//生成实验报告
			int totalScore = 0;
			for (int i = 0; i < clothesList.Count; i++)
			{
				if (clothesList[i].isRight == clothesList[i].togClothes.isOn)
					totalScore += 5;
			}
			endTime = DateTime.Now;
			ScoreReportData data = new ScoreReportData()
			{
				strModule = "着装准备",
				strStart = startTime,
				strEnd = endTime,
				strScore = totalScore.ToString()
			};
			UIKit.GetPanel<TestReportPanel>().CreateScoreReport(data);

			//转换UI
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
			for (int i = 0; i < clothesList.Count; i++)
				clothesList[i].imgMask.gameObject.SetActive(false);
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
