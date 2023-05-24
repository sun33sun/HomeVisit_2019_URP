using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using System.Collections;

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

		int dialogueIndex = 0;
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as OnVisitPanelData ?? new OnVisitPanelData();

			btnDialogue.GetComponent<Button>().onClick.AddListener(SwitchDialogue);
			btnCancelObserveDetail.onClick.AddListener(() =>
			{
				imgObserveDetail.gameObject.SetActive(false);
				btnDialogue.gameObject.SetActive(true);
			});
			btnConfirmObserveDetail.onClick.AddListener(() =>
			{
				UIKit.GetPanel<MainPanel>().NextTmp();
				imgObserveDetail.gameObject.SetActive(false);
				imgPreSpeak.gameObject.SetActive(true);
			});
			btnStartRecord.onClick.AddListener(() =>
			{
				imgPreSpeak.gameObject.SetActive(false);
				imgOnSpeak.gameObject.SetActive(true);
				btnEndRecord.interactable = false;
				StartCoroutine(WaveChange());
			});
			btnEndRecord.onClick.AddListener(() =>
			{
				imgOnSpeak.gameObject.SetActive(false);
				imgPostSpeak.gameObject.SetActive(true);
			});
			btnReRecord.onClick.AddListener(() =>
			{
				imgOnSpeak.gameObject.SetActive(true);
				imgPostSpeak.gameObject.SetActive(false);
				btnEndRecord.interactable = false;
				StartCoroutine(WaveChange());
			});
			btnConfirmRecord.onClick.AddListener(() => 
			{
				MainPanel mainPanel = UIKit.GetPanel<MainPanel>();
				mainPanel.NextTmp();
				mainPanel.NextTmp();
				mainPanel.NextStep();
				btnDialogue.gameObject.SetActive(true);
				dialogueIndex = 0;
				btnDialogue.sprite = spriteDialogue1[dialogueIndex];
				txtDialogue.text = strDialogue1[dialogueIndex];
				btnDialogue.GetComponent<Button>().onClick.RemoveListener(SwitchDialogue);
				btnDialogue.GetComponent<Button>().onClick.AddListener(SwitchDialogue1);

				imgPostSpeak.gameObject.SetActive(false);
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

		void SwitchDialogue()
		{
			dialogueIndex++;
			if (dialogueIndex == 3)
				UIKit.GetPanel<MainPanel>().NextTmp();


			if(dialogueIndex >= spriteDialogue.Length)
			{
				imgObserveDetail.gameObject.SetActive(true);
				btnDialogue.gameObject.SetActive(false);
				UIKit.GetPanel<MainPanel>().NextTmp();
			}
			else
			{
				btnDialogue.sprite = spriteDialogue[dialogueIndex];
				txtDialogue.text = strDialogue[dialogueIndex];
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

			if (dialogueIndex >= spriteDialogue1.Length)
			{
				imgExpressGratitude.gameObject.SetActive(true);
				btnDialogue.gameObject.SetActive(false);
			}
			else
			{
				btnDialogue.sprite = spriteDialogue1[dialogueIndex];
				txtDialogue.text = strDialogue1[dialogueIndex];
			}
		}


		public void InitState()
		{
			dialogueIndex = 0;

			btnDialogue.gameObject.SetActive(true);
			imgObserveDetail.gameObject.SetActive(false);
			imgPreSpeak.gameObject.SetActive(false);
			imgOnSpeak.gameObject.SetActive(false);
			imgPostSpeak.gameObject.SetActive(false);
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
