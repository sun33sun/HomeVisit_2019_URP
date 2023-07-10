using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBase;
using HomeVisit.UI;
using QFramework;
using HomeVisit.Character;
using ProjectBase.Anim;
using HomeVisit.Effect;
using System;

namespace HomeVisit.Task
{
	public class SingleParentTask : SingletonMono<SingleParentTask>
	{
		[SerializeField] List<Sprite> sprites;

		OnVisitPanel onVisitPanel = null;
		MainPanel mainPanel = null;
		TopPanel buttonPanel = null;

		bool isExaming = false;
		string strStudentDoFruitOnTable;

		private void Start()
		{
			EventCenter.GetInstance().AddEventListener<int>("访中过程试题完成", SingleTitleExamCallBack);
			EventCenter.GetInstance().AddEventListener<string>("<b>题目：</b>请输入学生的行为错在那些地方", InputExamCallBack);
		}

		void InputExamCallBack(string newStr)
		{
			strStudentDoFruitOnTable = newStr;
			isExaming = false;
		}
		void SingleTitleExamCallBack(int addScore)
		{
			isExaming = false;
		}

		private void OnDisable()
		{
			StopAllCoroutines();
			EventCenter.GetInstance().RemoveEventListener<int>("访中过程试题完成", SingleTitleExamCallBack);
			EventCenter.GetInstance().RemoveEventListener<string>("<b>题目：</b>请输入学生的行为错在那些地方", InputExamCallBack);
		}

		public IEnumerator StartTask()
		{
			if (onVisitPanel == null)
				onVisitPanel = UIKit.GetPanel<OnVisitPanel>();
			if (mainPanel == null)
				mainPanel = UIKit.GetPanel<MainPanel>();
			if (buttonPanel == null)
				buttonPanel = UIKit.GetPanel<TopPanel>();
			StudentFather.Instance.gameObject.SetActive(false);
			StudentController.Instance.transform.position = Interactive.Get("学生蜷缩位置").transform.position;
			StudentController.Instance.PlayAnim("CurlUp");
			Interactive.Get("平板").SetActive(false);

			//观环境
			mainPanel.StartTMP();
			yield return CameraManager.Instance.ThirdPerson(FemaleTeacher.Instance.thirdView, 2f);
			mainPanel.SetBK(false);
			yield return FemaleTeacher.Instance.PlayAnim("手机调为静音");
			yield return FemaleTeacher.Instance.PlayAnim("敲门");
			GameObject KeTingMen = Interactive.Get("客厅门");
			yield return ObjHighlightClickCallBack(KeTingMen, true);
			yield return StudentMather.Instance.Walk(Interactive.Get("客厅门口站位").transform);
			yield return AnimationManager.GetInstance().Play(KeTingMen.GetComponent<Animation>(), "OpenDoor");
			yield return StudentMather.Instance.Walk(Interactive.Get<Transform>("客厅站位_母亲"));
			yield return PlayAudio("母亲欢迎老师进门", 0, "老师，您好！欢迎欢迎，请进，请进！");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			yield return FemaleTeacher.Instance.Walk(Interactive.Get<Transform>("客厅站位_女老师"));
			yield return AnimationManager.GetInstance().Play(KeTingMen.GetComponent<Animation>(), "CloseDoor");
			FemaleTeacher.Instance.canMove = true;
			FemaleTeacher.Instance.canRotate = true;
			yield return PlayAudio("母亲_让孩子别看书出来见老师", 0, "老师来了，你别看书了，出来见见吧。");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			yield return RecordSpeech(new string[] { "咱们 ", "孩子房间", "看看" });
			yield return PlayAudio("母亲_唉，好吧", 0, "唉，好吧。");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			yield return Interactive.Get<ObjColliderEvent>("学生房间高亮正方体").AreaHighlight();
			GameObject XueShengFangMen = Interactive.Get("学生房门");
			yield return ObjHighlightClickCallBack(XueShengFangMen, true);
			yield return AnimationManager.GetInstance().Play(XueShengFangMen.GetComponent<Animation>(), "OpenDoor");
			onVisitPanel.GenerateReportData("了解情况_观环境", 3);
			//展能力
			mainPanel.NextTmp();
			yield return StudentMather.Instance.Walk(Interactive.Get("学生房站位_母亲").transform);
			yield return PlayAudio("母亲_这孩子就知道看书", 0, "这孩子就知道看书。");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			yield return ObjHighlightClickCallBack(Interactive.Get("俄狄浦斯"), true);
			yield return RecordSpeech(new string[] { "喜欢", "俄狄浦斯", "哪部分" });
			StudentController.Instance.PlayAnim("GetUp");
			StudentController.Instance.transform.position = Interactive.Get("学生房站位_学生").transform.position;
			StudentController.Instance.transform.forward = Interactive.Get("学生房站位_学生").transform.forward;
			yield return PlayAudio("男学生_俄狄浦斯的命运，我就受到了命运的捉弄。", 3, "俄狄浦斯的命运，我就受到了命运的捉弄。");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			onVisitPanel.GenerateReportData("了解情况_展能力", 3);
			//查细节
			mainPanel.NextTmp();
			yield return ObjHighlightClickCallBack(Interactive.Get("书柜"), true);
			yield return RecordSpeech(new string[] { "索福克勒斯", "其他作品" });
			yield return PlayAudio("男学生_想，可是我没有。", 3, "想，可是我没有。");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			yield return RecordSpeech(new string[] { "学校图书馆", "借书证" });
			yield return PlayAudio("男学生_真的吗？老师你真好", 3, "真的吗？老师你真好！");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			onVisitPanel.GenerateReportData("了解情况_查细节", 4);
			//说学校
			mainPanel.NextTmp();
			buttonPanel.tmpTip.text = "请对办学理念，成绩特色，校纪校规进行简单的叙述";
			GameObject KeTingZuoWei_MuQing = Interactive.Get("客厅坐位_母亲");
			yield return StudentMather.Instance.Walk(KeTingZuoWei_MuQing.transform);
			yield return StudentMather.Instance.SitDown(KeTingZuoWei_MuQing.transform);
			yield return Interactive.Get<ObjColliderEvent>("女老师坐下的位置").AreaHighlight();
			FemaleTeacher.Instance.canMove = false;
			yield return FemaleTeacher.Instance.SitDown(Interactive.Get("客厅坐位_女老师").transform);
			yield return RecordSpeech(new string[] { "育才先育人", "校规", "身心健康", "家长会" });
			yield return PlayAudio("母亲老师您说得是", 0, "老师您说得是！");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			onVisitPanel.GenerateReportData("了解情况_说学校", 4);
			//聊班级
			buttonPanel.tmpTip.text = "请对班级概况，任课老师进行简单的叙述，并说明彦威朋友不少却情绪低落。";
			yield return RecordSpeech(new string[] { "班级概况", "任课老师", "情绪低落", "朋友不少" });
			yield return PlayAudio("母亲_嗯嗯嗯，我会更加关心他的", 0, "嗯嗯嗯，我会更加关心他的。");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			onVisitPanel.GenerateReportData("了解情况_聊班级", 4);
			//建立联系
			//摸情况
			mainPanel.NextStep();
			mainPanel.NextTmp();
			buttonPanel.tmpTip.text = "请询问孩子身体情况，特别需求，明确家长接送时间，工作安排";
			yield return LoadStudentInformationPaper();
			yield return RecordSpeech(new string[] { "身体情况", "家长会", "接送时间", "工作安排" });
			yield return PlayAudio("母亲_单亲需求", 0, "没啥特别需求，但是家里有些事忙不过来，没法参与家长会。平时都是孩子自己上下学。");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			onVisitPanel.GenerateReportData("建立联系_摸情况", 4);
			//明期望
			mainPanel.NextTmp();
			buttonPanel.tmpTip.text = "请明确家长的教育需求";
			yield return RecordSpeech(new string[] { "特别需求", "发展目标" });
			yield return PlayAudio("母亲_单亲教育需求", 0, "只要孩子没啥事就行，其他的就全交给老师您了。");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			onVisitPanel.GenerateReportData("建立联系_明期望", 2);
			//适当指导
			//缓焦虑
			mainPanel.NextStep();
			mainPanel.NextTmp();
			buttonPanel.tmpTip.text = "引导家长关心孩子心理健康，并提醒家长消除单亲影响不能单靠老师";
			yield return RecordSpeech(new string[] {"单亲","单独家访", "不能放养" });
			yield return PlayAudio("母亲_太谢谢您了，孩子我会尽量多关注的。", 0, "太谢谢您了，孩子我会尽量多关注的。");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			onVisitPanel.GenerateReportData("适当指导_缓焦虑", 3);
			//答疑惑
			mainPanel.NextTmp();
			yield return RecordSpeech(new string[] { "关心", "爱" });
			onVisitPanel.CloseRecord();
			onVisitPanel.GenerateReportData("适当指导_答疑惑", 2);
			//表达感谢
			//表达感谢
			mainPanel.NextStep();
			mainPanel.NextTmp();
			StudentMather.Instance.PlayAnim("StandUp");
			yield return FemaleTeacher.Instance.StandUp();
			yield return StudentMather.Instance.PlayAnim("GiveGift");
			yield return PlayAudio("母亲_送礼", 0, "这是点小心意，还请务必收下。");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			yield return RecordSpeech(new string[] { "不接受任何形式的礼物", "感谢接待和配合", "谢谢" });
			yield return onVisitPanel.ShowExpressGratitude();
			FemaleTeacher.Instance.canMove = true;
			yield return Interactive.Get("客厅门口高亮正方体").GetComponent<ObjColliderEvent>().AreaHighlight();
			onVisitPanel.GenerateReportData("表达感谢", 1);
			onVisitPanel.ShowNext();
		}

		#region 播放家长语音并显示文字
		WaitUntil PlayAudio(string clipName, int spriteIndex, string strWord)
		{
			Sprite spriteParent = sprites[spriteIndex];
			onVisitPanel.ShowParentWord(spriteParent, strWord);
			return AudioManager.Instance.Play(clipName);
		}
		#endregion

		#region 开始语音识别
		WaitUntil RecordSpeech(string[] keywords)
		{
			StartCoroutine(mainPanel.InitKeyWords(keywords));
			onVisitPanel.ShowRecordUI(keywords);
			return new WaitUntil(() => { return onVisitPanel.recordState == RecordState.ResultIsRight; });
		}
		#endregion

		#region 物体高亮点击回调
		WaitUntil ObjHighlightClickCallBack(GameObject targetObj, bool isCallBack)
		{
			EffectManager.Instance.AddEffectImmediately(targetObj);
			if (isCallBack)
			{
				return EventManager.Instance.AddObjClick(targetObj);
			}
			else
			{
				return null;
			}
		}
		#endregion

		#region 考核
		WaitUntil StartExam(List<SingleTitleData> datas)
		{
			isExaming = true;
			UIKit.GetPanel<KnowledgeExamPanel>().LoadOnVisitPaper(datas);
			return new WaitUntil(() => { return !isExaming; });
		}

		IEnumerator LoadStudentInformationPaper()
		{
			List<SingleTitleData> datas = null;
			yield return WebKit.GetInstance().Read<List<SingleTitleData>>(Settings.PAPER + "Paper_OnVisit3.json", d =>
			{
				datas = d;
			});
			yield return StartExam(datas);
		}
		#endregion
	}
}

