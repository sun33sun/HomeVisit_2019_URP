using UnityEngine;
using UnityEngine.UI;
using QFramework;
using HomeVisit.Draw;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using HomeVisit.Effect;
using ProjectBase;
using HomeVisit.Character;
using System;
using UnityEngine.EventSystems;

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
		DateTime startTime;
		private void Start()
		{
			startTime = DateTime.Now;
		}

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as HomeVisitRoutePanelData ?? new HomeVisitRoutePanelData();

			btnClose.onClick.AddListener(Submit);
			btnSubmit.onClick.AddListener(Submit);

			btnDialogue.onClick.AddListener(() =>
			{
				btnDialogue.gameObject.SetActive(false);
				imgTip.gameObject.SetActive(true);
			});
			btnConfirm.onClick.AddListener(() =>
			{
				imgTip.gameObject.SetActive(false);
				StartCoroutine(LoadSceneAsync());
			});
			btnDraw.onClick.AddListener(() => { drawDriver.isEnable = !drawDriver.isEnable; });
			btnErase.onClick.AddListener(drawDriver.Clear);

			for (int i = 0; i < btnRouteList.Count; i++)
			{
				int index = i;
				btnRouteList[i].onClick.AddListener(() => { imgRoute.sprite = spriteRouteList[index]; });
			}
		}

		void Submit()
		{
			ScoreReportData data = new ScoreReportData()
			{
				title = "ȷ�ϼҷ�ʱ��·��",
				startTime = this.startTime,
				endTime = DateTime.Now,
				score = 2
			};
			UIKit.GetPanel<TestReportPanel>().CreateScoreReport(data);

			UIKit.OpenPanelAsync<CommunicateOutlinePanel>().ToAction().Start(this);
			Hide();
			UIKit.GetPanel<MainPanel>().SetBK(true);
			UIKit.GetPanel<TestReportPanel>().SetTestEvaluate(inputTestEvaluate.text);
		}

		IEnumerator LoadSceneAsync()
		{
			TopPanel topPanel = UIKit.GetPanel<TopPanel>();
			yield return topPanel.CloseEyeAnim();
			UIKit.GetPanel<MainPanel>().SetBK(false);
			yield return SceneManager.LoadSceneAsync("Office", LoadSceneMode.Additive);
			yield return topPanel.OpenEyeAnim();
			GameObject Teacher_Computer = Interactive.Get("Teacher_Computer");
			EffectManager.Instance.AddEffectAndTarget(Teacher_Computer);
			EventManager.Instance.AddObjClick(Teacher_Computer, () =>
			{
				svMap.gameObject.SetActive(true);
				imgMid.gameObject.SetActive(true);
			});
			CameraManager.Instance.SetRoamRig(RigidbodyConstraints.FreezeRotation);
			CameraManager.Instance.IsEnable = true;
			Animation anim = Interactive.Get("��ʦ����").GetComponent<Animation>();
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
			UIKit.GetPanel<MainPanel>().NextStep();

			btnDialogue.gameObject.SetActive(true);
			imgTip.gameObject.SetActive(false);
			svMap.gameObject.SetActive(false);
			imgMid.gameObject.SetActive(false);
			imgBlank.gameObject.SetActive(false);
			UIKit.GetPanel<TopPanel>().ChangeTip("�����ѧУ���Ÿ������ε�ѧ����Ϣ������ͥסַ�����ѧ��Ȧ����һ�𣬳�����Ϊͬһ����мҷõ�����");
		}

		protected override void OnHide()
		{
		}

		protected override void OnClose()
		{
		}
	}
}
