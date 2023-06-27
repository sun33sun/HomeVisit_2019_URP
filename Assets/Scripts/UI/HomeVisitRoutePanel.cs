using UnityEngine;
using UnityEngine.UI;
using QFramework;
using HomeVisit.Draw;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using HomeVisit.Effect;
using ProjectBase;

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
				//UIKit.GetPanel<TestReportPanel>().SetTestEvaluate(inputTestEvaluate.text);
			});

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

		IEnumerator LoadSceneAsync()
		{
			if (Settings.BanGongShi)
				yield break;
			yield return CloseEyeAnim();
			UIKit.GetPanel<MainPanel>().SetBK(false);
			AsyncOperation operation = SceneManager.LoadSceneAsync("BanGongShi", LoadSceneMode.Additive);
			yield return new WaitUntil(() => { return operation.isDone; });
			yield return OpenEyeAnim();
			Settings.BanGongShi = true;
			GameObject Teacher_Computer = Interactive.Get("Teacher_Computer");
			EffectManager.Instance.AddEffectAndTarget(Teacher_Computer);
			EventManager.Instance.AddObjClick(Teacher_Computer, () => { 
				svMap.gameObject.SetActive(true);
				imgMid.gameObject.SetActive(true);
			});
			CameraManager.Instance.SetRoamRig(RigidbodyConstraints.FreezeRotation);
			CameraManager.Instance.IsEnable = true;
			Animation anim = Interactive.Get("老师坐下").GetComponent<Animation>();
			//yield return anim.
		}

		IEnumerator CloseEyeAnim()
		{
			imgBlank.gameObject.SetActive(true);
			Material mat = imgBlank.material;
			Vector4 vector = new Vector4(0.6f, 1, 1, 1);
			float duration = 0.1f;
			WaitForSeconds wait01 = new WaitForSeconds(0.1f);
			while (vector.y > 0)
			{
				vector.y -= duration;
				mat.SetVector("_Param", vector);
				yield return wait01;
			}
		}

		IEnumerator OpenEyeAnim()
		{
			imgBlank.gameObject.SetActive(true);
			Material mat = imgBlank.material;
			Vector4 vector = new Vector4(0.6f, 0, 1, 1);
			float duration = 0.1f;
			WaitForSeconds wait01 = new WaitForSeconds(0.1f);
			while (vector.y < 1)
			{
				vector.y += duration;
				mat.SetVector("_Param", vector);
				yield return wait01;
			}
			imgBlank.gameObject.SetActive(false);
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
			imgBlank.gameObject.SetActive(false);
			UIKit.GetPanel<ButtonPanel>().ChangeTip("请根据学校发放给班主任的学生信息，将家庭住址相近的学生圈画在一起，初步列为同一天进行家访的名单");
		}

		protected override void OnHide()
		{
		}

		protected override void OnClose()
		{
		}
	}
}
