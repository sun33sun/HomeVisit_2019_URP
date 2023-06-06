using UnityEngine;
using UnityEngine.UI;
using QFramework;
using HomeVisit.Draw;
using System.Collections.Generic;

namespace HomeVisit.UI
{
	public class HomeVisitRoutePanelData : UIPanelData
	{

	}
	public partial class HomeVisitRoutePanel : UIPanel
	{
		[SerializeField] DrawDriver drawDriver;
		[SerializeField] List<Button> btnRouteList;
		[SerializeField] List<Sprite> spriteRouteList;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as HomeVisitRoutePanelData ?? new HomeVisitRoutePanelData();

			btnClose.onClick.AddListener(Hide);
			btnSubmit.onClick.AddListener(() =>
			{
				UIKit.OpenPanelAsync<CommunicateOutlinePanel>().ToAction().Start(this);
				UIKit.GetPanel<MainPanel>().NextStep();
				Hide();
				UIKit.GetPanel<TestReportPanel>().SetTestEvaluate(inputTestEvaluate.text);
			});

			btnDialogue.onClick.AddListener(() =>
			{
				btnDialogue.gameObject.SetActive(false);
				imgTip.gameObject.SetActive(true);
			});
			btnConfirm.onClick.AddListener(() =>
			{
				imgTip.gameObject.SetActive(false);
				svMap.gameObject.SetActive(true);
			});
			btnDraw.onClick.AddListener(() => { drawDriver.isEnable = !drawDriver.isEnable; });
			btnErase.onClick.AddListener(drawDriver.Clear);

			for (int i = 0; i < btnRouteList.Count; i++)
			{
				int index = i;
				btnRouteList[i].onClick.AddListener(() => { imgRoute.sprite = spriteRouteList[index]; });
			}
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
			btnDialogue.gameObject.SetActive(true);
			imgTip.gameObject.SetActive(false);
			svMap.gameObject.SetActive(false);
			imgMid.gameObject.SetActive(false);
			imgRoute.sprite = null;
			UIKit.GetPanel<ButtonPanel>().ChangeTip("�����ѧУ���Ÿ������ε�ѧ����Ϣ������ͥסַ�����ѧ��Ȧ����һ�𣬳�����Ϊͬһ����мҷõ�����");
		}

		protected override void OnHide()
		{
		}

		protected override void OnClose()
		{
		}
	}
}
