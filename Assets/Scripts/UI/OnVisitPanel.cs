using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using ProjectBase;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.Windows.Speech;
using UnityEngine.Networking;

namespace HomeVisit.UI
{
	public class OnVisitPanelData : UIPanelData
	{

	}
	public partial class OnVisitPanel : UIPanel
	{
		[SerializeField] Sprite[] spriteDialogue;
		[SerializeField] string[] strDialogue;

		[SerializeField] Sprite[] spriteDialogue1;
		[SerializeField] string[] strDialogue1;

		DialogueInfo dialogueInfo;

		bool isLoop = false;

		int dialogueIndex = 0;
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as OnVisitPanelData ?? new OnVisitPanelData();

			btnDialogue.GetComponent<Button>().onClick.AddListener(SwitchDialogue);
			btnCancelObserveDetail.onClick.AddListener(() =>
			{
				imgObserveDetail.gameObject.SetActive(false);
			});
			btnConfirmObserveDetail.onClick.AddListener(() =>
			{
				//关闭自己
				imgObserveDetail.gameObject.SetActive(false);
				UIKit.GetPanel<MainPanel>().NextTmp();
				MainPanel mainPanel = UIKit.GetPanel<MainPanel>();
				mainPanel.NextTmp();
				mainPanel.NextTmp();
				mainPanel.NextStep();

			});
			btnStartRecord.onClick.AddListener(() =>
			{
				AudioManager.Instance.StopAudio();
				CloseRecord();
				imgOnSpeak.gameObject.SetActive(true);
				btnEndRecord.interactable = false;
				StartCoroutine(WaveChange());
				SignalManager.Instance.StartRecorderFunc();
				RecordDebug.Instance.StartRecord(dialogueInfo.dialogues[dialogueIndex].keywords, OnResult);
			});
			btnEndRecord.onClick.AddListener(() =>
			{
				CloseRecord();
				RecordDebug.Instance.EndRecord();
				imgPostSpeak.gameObject.SetActive(true);
			});
			btnReRecord.onClick.AddListener(() =>
			{
				CloseRecord();
				imgOnSpeak.gameObject.SetActive(true);
				btnEndRecord.interactable = false;
				StartCoroutine(WaveChange());
				RecordDebug.Instance.StartRecord(dialogueInfo.dialogues[dialogueIndex].keywords, OnResult);
			});
			btnConfirmRecord.onClick.AddListener(() =>
			{
				if (dialogueIndex >= dialogueInfo.dialogues.Count)
				{
					imgObserveDetail.gameObject.SetActive(true);
				}
				else
				{
					CloseRecord();
					SkipDialogue();
				}

			});
			btnRefuse.onClick.AddListener(() =>
			{
				imgNext.gameObject.SetActive(true);
				imgExpressGratitude.gameObject.SetActive(false);
			});
			btnAccept.onClick.AddListener(() =>
			{
				imgNext.gameObject.SetActive(true);
				imgExpressGratitude.gameObject.SetActive(false);
			});
			btnNext.onClick.AddListener(() =>
			{
				UIKit.OpenPanelAsync<RecordSheetPanel>().ToAction().Start(this);
				Hide();
			});
			InitState();

			StartCoroutine(LoadSceneAsync());
		}

		IEnumerator LoadSceneAsync()
		{
			yield return CloseEyeAnim();
			UnityWebRequest request = UnityWebRequest.Get(ProjectSetting.DialogueInfo);
			yield return request.SendWebRequest();
			string json = request.downloadHandler.text;
			dialogueInfo = JsonConvert.DeserializeObject<DialogueInfo>(json);

			AsyncOperation unLoadOperation = SceneManager.UnloadSceneAsync("BanGongShi");
			yield return new WaitUntil(() => { return unLoadOperation.isDone; });
			isLoop = false;
			ProjectSetting.BanGongShi = false;
			AsyncOperation operation = SceneManager.LoadSceneAsync(ProjectSetting.RandomScene, LoadSceneMode.Additive);
			yield return new WaitUntil(() => { return operation.isDone; });
			isLoop = false;
			GameObject originCube = Interactive.Get("originCube");
			CameraManager.Instance.SetRoamPos(originCube.transform.position);
			CameraManager.Instance.SetRoamForward(originCube.transform.forward);
			yield return OpenEyeAnim();
			StartDialogue();
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


		IEnumerator WaveChange()
		{
			float value = 0;
			WaitForSeconds wait = new WaitForSeconds(0.1f);
			imgFillWave.fillOrigin = 0;

			while (value < 1)
			{
				imgFillWave.fillAmount = value;
				value += 0.1f;
				yield return wait;
			}

			imgFillWave.fillOrigin = 1;
			while (value > 0)
			{
				imgFillWave.fillAmount = value;
				value -= 0.1f;
				yield return wait;
			}
			btnEndRecord.interactable = true;
		}

		void StartDialogue()
		{
			dialogueIndex = -1;
			isRight = true;
			SwitchDialogue();
		}

		void SwitchDialogue()
		{
			if (isRight)
				isRight = false;
			else
				return;
			SkipDialogue();
		}

		void CloseRecord()
		{
			imgPreSpeak.gameObject.SetActive(false);
			imgOnSpeak.gameObject.SetActive(false);
			imgPostSpeak.gameObject.SetActive(false);
		}

		void SkipDialogue()
		{
			btnDialogue.gameObject.SetActive(true);

			dialogueIndex++;
			if (dialogueIndex == 3)
				UIKit.GetPanel<MainPanel>().NextTmp();

			if (dialogueIndex >= dialogueInfo.dialogues.Count)
			{
				btnDialogue.gameObject.SetActive(false);
				CloseRecord();
				MainPanel mainPanel = UIKit.GetPanel<MainPanel>();
				mainPanel.NextTmp();
				imgExpressGratitude.gameObject.SetActive(true);

				return;
			}
			else if (dialogueInfo.dialogues[dialogueIndex].keywords == null)
			{
				isRight = true;
				txtDialogue.text = dialogueInfo.dialogues[dialogueIndex].parent;
				AudioManager.Instance.PlayAudio(dialogueInfo.dialogues[dialogueIndex].parent);
			}
			else if (dialogueInfo.dialogues[dialogueIndex].parent == null)
			{
				isRight = false;
				btnDialogue.gameObject.SetActive(false);
				imgPreSpeak.gameObject.SetActive(true);
			}
			else
			{
				isRight = false;
				txtDialogue.text = dialogueInfo.dialogues[dialogueIndex].parent;
				AudioManager.Instance.PlayAudio(dialogueInfo.dialogues[dialogueIndex].parent);
				ActionKit.Delay(5, () =>
				{
					imgPreSpeak.gameObject.SetActive(true);
					btnDialogue.gameObject.SetActive(false);
				}).Start(this);
			}
		}

		bool isRight = false;
		void OnResult(string newStr, bool newState)
		{
			isRight = newState;
			if (isRight)
			{
				CloseRecord();
				SwitchDialogue();
			}
			else
			{
				CloseRecord();
				imgOnSpeak.gameObject.SetActive(true);
				tmpSpeakText.text = newStr;
				tmpPostText.text = newStr;
			}

		}

		void SwitchDialogue1()
		{
			dialogueIndex++;
			switch (dialogueIndex)
			{
				case 1:
					UIKit.GetPanel<MainPanel>().NextTmp();
					break;
				case 2:
					UIKit.GetPanel<MainPanel>().NextTmp();
					break;
				case 3:
					UIKit.GetPanel<MainPanel>().NextTmp();
					UIKit.GetPanel<MainPanel>().NextTmp();
					UIKit.GetPanel<MainPanel>().NextStep();
					break;
				default:
					break;
			}

			if (dialogueIndex > dialogueInfo.dialogues.Count)
			{
				imgExpressGratitude.gameObject.SetActive(true);
				btnDialogue.gameObject.SetActive(false);
			}
			else if (dialogueInfo.dialogues[dialogueIndex].keywords == null)
			{
				txtDialogue.text = dialogueInfo.dialogues[dialogueIndex].parent;
			}
			else if (dialogueInfo.dialogues[dialogueIndex].parent == null)
			{
				ActionKit.Delay(5, () =>
				{
					imgPreSpeak.gameObject.SetActive(true);
					btnDialogue.gameObject.SetActive(false);
				}).Start(this);
				txtDialogue.text = "请回答";
			}
			else
			{
				txtDialogue.text = dialogueInfo.dialogues[dialogueIndex].parent;
				ActionKit.Delay(5, () =>
				{
					imgPreSpeak.gameObject.SetActive(true);
					btnDialogue.gameObject.SetActive(false);
				}).Start(this);
			}
		}


		public void InitState()
		{
			dialogueIndex = 0;

			btnDialogue.gameObject.SetActive(true);
			imgObserveDetail.gameObject.SetActive(false);
			CloseRecord();
			imgExpressGratitude.gameObject.SetActive(false);
			imgNext.gameObject.SetActive(false);

			UIKit.GetPanel<MainPanel>().StartTMP();
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
		}

		protected override void OnHide()
		{
		}

		protected override void OnClose()
		{
		}
	}
}
