using HomeVisit;
using HomeVisit.Character;
using HomeVisit.Effect;
using HomeVisit.UI;
using ProjectBase;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeVisit.Task
{
	public class MultipleChildrenTask : MonoBehaviour
	{
		[SerializeField] List<Sprite> sprites;

		OnVisitPanel onVisitPanel = null;
		MainPanel mainPanel = null;
		ButtonPanel buttonPanel = null;

		bool isExaming = false;
		int totalScore = 0;
		string strStudentDoFruitOnTable;


		void Start()
		{
			StartCoroutine(StartTask());
		}

		private void OnDisable()
		{
			StopAllCoroutines();
		}

		IEnumerator StartTask()
		{
			if (onVisitPanel == null)
				onVisitPanel = UIKit.GetPanel<OnVisitPanel>();
			if (mainPanel == null)
				mainPanel = UIKit.GetPanel<MainPanel>();
			if (buttonPanel == null)
				buttonPanel = UIKit.GetPanel<ButtonPanel>();

			//观环境
			mainPanel.StartTMP();
			yield return CameraManager.Instance.ThirdPerson(FemaleTeacher.Instance.thirdView, 2f);
			yield return FemaleTeacher.Instance.PlayAnim("手机调为静音");
			yield return FemaleTeacher.Instance.PlayAnim("敲门");
			GameObject KeTingMen = Interactive.Get("客厅门");
			yield return ObjHighlightClickCallBack(KeTingMen, true);
			yield return StudentMather.Instance.Walk(KeTingMen.transform);
			KeTingMen.SetActive(false);
			yield return StudentMather.Instance.Walk(Interactive.Get<Transform>("客厅站位_母亲"));
			yield return PlayAudio("母亲欢迎老师进门", 0, "老师，您好！欢迎欢迎，请进，请进！");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			yield return FemaleTeacher.Instance.Walk(Interactive.Get<Transform>("客厅站位_女老师"));
			KeTingMen.SetActive(true);
			FemaleTeacher.Instance.canMove = true;
			FemaleTeacher.Instance.canRotate = true;
			yield return RecordSpeech(new string[] { "谢谢", "参观" });
			yield return PlayAudio("母亲认可", 0, "好的！没问题");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			yield return Interactive.Get<ObjColliderEvent>("学生房间高亮正方体").AreaHighlight();
			GameObject XueShengFangMen = Interactive.Get("学生房门");
			yield return ObjHighlightClickCallBack(XueShengFangMen, true);
			XueShengFangMen.SetActive(false);
			//展能力
			mainPanel.NextTmp();
			yield return ObjHighlightClickCallBack(StudentController.Instance.gameObject, true);
			yield return StudentMather.Instance.Walk(Interactive.Get("学生房站位_母亲").transform);
			yield return PlayAudio("母亲抱怨孩子看平板", 0, "这孩子经常呆在家中看平板，一看就是一整天。我们夫妻想再要一个，也没时间管她。");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			yield return RecordSpeech(new string[] { "父母关注", "好看", "画" });
			yield return StudentController.Instance.PlayAnim("StandUp");
			yield return PlayAudio("女学生这是我的得意之作", 0, "这是我的得意之作！");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			//查细节
			mainPanel.NextTmp();
			yield return ObjHighlightClickCallBack(Interactive.Get("平板"), true);
			yield return RecordSpeech(new string[] { "平板都不玩", "关心", "多孩" });
			yield return PlayAudio("母亲老师您说得是", 0, "老师您说得是。");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			//说学校
			mainPanel.NextTmp();
			buttonPanel.tmpTip.text = "请对办学理念，成绩特色，校纪校规进行简单的叙述";
			GameObject KeTingZuoWei_MuQing = Interactive.Get("客厅坐位_母亲");
			yield return StudentMather.Instance.Walk(KeTingZuoWei_MuQing.transform);
			yield return StudentMather.Instance.SitDown(KeTingZuoWei_MuQing.transform);
			yield return StudentFather.Instance.SitDown(Interactive.Get("客厅坐位_父亲").transform);
			yield return Interactive.Get<ObjColliderEvent>("女老师坐下的位置").AreaHighlight();
			FemaleTeacher.Instance.canMove = false;
			yield return FemaleTeacher.Instance.SitDown(Interactive.Get("客厅坐位_女老师").transform);
			yield return RecordSpeech(new string[] {  "育才先育人", "校规","身心健康" });
			//聊班级
			mainPanel.NextTmp();
			buttonPanel.tmpTip.text = "请对班级概况，发展目标，任课老师进行简单的叙述，并讲述光美的班中职务";
			yield return RecordSpeech(new string[] { "班级概况", "发展目标", "任课老师","宣传委员" });
			//建立关系
			//摸情况
			mainPanel.NextStep();
			mainPanel.NextTmp();
			buttonPanel.tmpTip.text = "请询问孩子身体情况，特别需求，明确家长接送时间，工作安排";
			yield return LoadStudentInformationPaper();
			yield return RecordSpeech(new string[] { "家庭情况", "个性特点", "情绪", "学习经历", "特长优势", "学校环境", "在家表现", "自理能力", "家长期望", "家长诉求", "特别关注" });
			yield return PlayAudio("母亲说光美需求", 0, "光美身体不太好，容易感冒。平时都是我（母）开车送她去学。有家长会我一定到。");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			//明期望
			mainPanel.NextTmp();
			buttonPanel.tmpTip.text = "请明确家长的教育需求";
			yield return RecordSpeech(new string[] { "班级岗位职务", "多孩", "发展目标" });
			yield return PlayAudio("母亲对光美的期望", 0, "我们希望她尽量不受多孩的影响，提高成绩。");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			//适当指导
			//缓解焦虑
			mainPanel.NextStep();
			mainPanel.NextTmp();
			buttonPanel.tmpTip.text = "引导家长关心孩子心理健康，并提醒家长消除多孩影响不能单靠老师";
			yield return RecordSpeech(new string[] { "心理健康", "单靠老师", "多孩"});
			yield return PlayAudio("母亲承诺多关心光美心理健康", 0, "您说的是，心里健康很重要。我们之后会尽量抽出时间关心她的。");
			//表达感谢
			mainPanel.NextStep();
			mainPanel.NextTmp();
			yield return StudentFather.Instance.PlayAnim("GiveGift");
			yield return PlayAudio("父亲感谢老师", 0, "谢谢老师的指导，希望以后能在学校里多关注孩子");
			yield return PlayAudio("父亲送礼", 0, "这是点小心意，还请务必收下");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);

			yield return RecordSpeech(new string[] { "不接受任何形式的礼物", "感谢接待和配合", "谢谢" });
			yield return onVisitPanel.ShowExpressGratitude();
			yield return ObjHighlightClickCallBack(KeTingMen, true);
			FemaleTeacher.Instance.canMove = false;
			FemaleTeacher.Instance.canRotate = false;
			FemaleTeacher.Instance.RotateTo(Interactive.Get("客厅门站位_女老师").transform.forward);
			StudentController.Instance.Walk(Interactive.Get("客厅站位_学生").transform);
			yield return PlayAudio("女学生感谢老师", 0, "老师，谢谢你来看我，给你花");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			yield return onVisitPanel.ShowExpressGratitude();
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

		WaitUntil ObjHighlightClickCallBack(string objName, bool isCallBack)
		{
			GameObject targetObj = Interactive.Get(objName);
			targetObj.SetActive(true);
			return ObjHighlightClickCallBack(targetObj, isCallBack);
		}
		#endregion

		#region 考核
		WaitUntil StartExam(List<SingleTitleData> datas)
		{
			isExaming = true;
			UIKit.GetPanel<KnowledgeExamPanel>().LoadOnVisitPaper(datas);
			return new WaitUntil(() => { return !isExaming; });
		}

		WaitUntil StartInputExam(string strTip)
		{
			isExaming = true;
			onVisitPanel.ShowOnVisitInput(strTip);
			return new WaitUntil(() => { return !isExaming; });
		}

		IEnumerator LoadStudentInformationPaper()
		{
			List<SingleTitleData> datas = null;
			yield return WebKit.GetInstance().Read<List<SingleTitleData>>(Settings.PAPER + "Paper_OnVisit2.json", d => 
			{ 
				datas = d;
			});
			yield return StartExam(datas);
		}
		#endregion
	}
}

