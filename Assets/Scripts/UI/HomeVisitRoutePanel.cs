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
using HomeVisit.Task;
using UnityEngine.EventSystems;

namespace HomeVisit.UI
{
	public class HomeVisitRoutePanelData : UIPanelData
	{

	}
	public partial class HomeVisitRoutePanel : UIPanel
	{
		[SerializeField] DrawDriver drawDriver;
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
				tmpTip.text = @"���ְ����������ȫ�����˽⺢�ӵ����������ͥ������������ѡ���ͥ��ԱȫԱ�ڼ���һʱ��Σ��ձ�Ϊ˫���գ�
���������ֻ�����˽⺢����ٵ�����ѧϰ�������ѡ���мҳ��ڼ��ɣ������ͥ���֮ͥ���ʱ�䡣";
			});
			btnConfirm.onClick.AddListener(() =>
			{
				imgTip.gameObject.SetActive(false);
				if(!isLoaded)
					StartCoroutine(LoadSceneAsync());
			});
			btnDraw.onClick.AddListener(() => { drawDriver.isEnable = !drawDriver.isEnable; });
			btnErase.onClick.AddListener(drawDriver.Clear);
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

			UIKit.OpenPanelAsync<CommunicateOutlinePanel>(prefabName: Settings.UI + QAssetBundle.Communicateoutlinepanel_prefab.COMMUNICATEOUTLINEPANEL).ToAction().Start(this);
			Hide();
			UIKit.GetPanel<TestReportPanel>().tmpRoute.text =inputTestEvaluate.text;
		}

		private bool isLoaded = false;
		IEnumerator LoadSceneAsync()
		{
			isLoaded = true;
			TopPanel topPanel = UIKit.GetPanel<TopPanel>();
			yield return topPanel.CloseEyeAnim();
			UIKit.GetPanel<MainPanel>().SetBK(false);
			yield return SceneManager.LoadSceneAsync("Office");
			CameraManager.Instance.SetRoamPos(Interactive.Get("��ʼ��").transform.position);
			yield return topPanel.OpenEyeAnim();
			//���ﶯ����ɻص�
			OffiecTask.Instance.DoTask(() =>
			{
				GameObject Teacher_Computer = Interactive.Get("Teacher_Computer");
				EffectManager.Instance.AddEffectImmediately(Teacher_Computer);
				EventManager.Instance.AddObjClick(Teacher_Computer, () =>
				{
					svMap.gameObject.SetActive(true);
					imgMid.gameObject.SetActive(true);
					GetComponent<Image>().enabled = true;
					CameraManager.Instance.IsEnable = false;
					imgTip.gameObject.SetActive(true);
					tmpTip.text = "������Ͻǰ�ťѡ��ҷ�·�ߡ�������½ǰ�ť�Ե�ͼ���л滭��������";
				});
			});
			CameraManager.Instance.SetRoamRig(RigidbodyConstraints.FreezeRotation);
			CameraManager.Instance.IsEnable = true;
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
			GetComponent<Image>().enabled = false;
			UIKit.GetPanel<TopPanel>().ChangeTip("����ͥסַ�����ѧ��Ȧ����һ�𣬳�����Ϊͬһ����мҷõ�����");
		}

		protected override void OnHide()
		{
		}

		protected override void OnClose()
		{
		}
	}
}
