using HomeVisit;
using HomeVisit.Character;
using HomeVisit.Effect;
using HomeVisit.UI;
using ProjectBase;
using ProjectBase.Anim;
using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeVisit.Task
{
	public class MultipleChildrenTask : SingletonMono<MultipleChildrenTask>
	{
		[SerializeField] List<Sprite> sprites;

		OnVisitPanel onVisitPanel = null;
		MainPanel mainPanel = null;
		TopPanel topPanel = null;

		bool isExaming = false;
		string strStudentDoFruitOnTable;
		DateTime startTime;

		private void Start()
		{
			startTime = DateTime.Now;
			EventCenter.GetInstance().AddEventListener<int>("访中过程试题完成", SingleTitleExamCallBack);
			EventCenter.GetInstance().AddEventListener<string>("请输入学生的行为错在那些地方", InputExamCallBack);
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
			EventCenter.GetInstance().RemoveEventListener<string>("请输入学生的行为错在那些地方", InputExamCallBack);
		}

		public IEnumerator StartTask()
		{
			if (onVisitPanel == null)
				onVisitPanel = UIKit.GetPanel<OnVisitPanel>();
			if (mainPanel == null)
				mainPanel = UIKit.GetPanel<MainPanel>();
			if (topPanel == null)
				topPanel = UIKit.GetPanel<TopPanel>();
			FemaleStudent.Instance.SetTransform(Interactive.Get("学生蜷缩位置").transform);
			StartCoroutine(FemaleStudent.Instance.PlayAnim("蜷缩", false));
			Interactive.Get("俄狄浦斯").SetActive(false);
			MaleStudent.Instance.gameObject.SetActive(false);

			//观环境
			mainPanel.StartTMP();
			yield return CameraManager.Instance.ThirdPerson(FemaleTeacher.Instance.transform, 2f);
			mainPanel.SetBK(false);
			yield return FemaleTeacher.Instance.PlayAnim("手机调为静音");
			yield return FemaleTeacher.Instance.PlayAnim("敲门");
			GameObject KeTingMen = Interactive.Get("客厅门");
			yield return ObjHighlightClickCallBack(KeTingMen, true);
			GameObject KeTingMenKouZhanWei = Interactive.Get("客厅门口站位");
			yield return StudentMather.Instance.Walk(KeTingMenKouZhanWei.transform);
			yield return AnimationManager.GetInstance().Play(KeTingMen.GetComponent<Animation>(), "OpenDoor");
			yield return StudentMather.Instance.Walk(Interactive.Get<Transform>("客厅站位_母亲"));
			yield return PlayAudio("母亲欢迎老师进门", 0, "老师，您好！欢迎欢迎，请进，请进！");
			yield return FemaleTeacher.Instance.Walk(Interactive.Get<Transform>("客厅站位_女老师"));
			yield return AnimationManager.GetInstance().Play(KeTingMen.GetComponent<Animation>(), "CloseDoor");
			FemaleTeacher.Instance.canMove = true;
			FemaleTeacher.Instance.canRotate = true;
			yield return RecordSpeech(new string[] { "谢谢", "参观" });
			yield return PlayAudio("母亲认可", 0, "好的！没问题");
			yield return AreaHighlight(Interactive.Get<ObjColliderEvent>("学生房间高亮正方体"));
			FemaleTeacher.Instance.PlayAnim("站立");
			GameObject XueShengFangMen = Interactive.Get("学生房门");
			yield return ObjHighlightClickCallBack(XueShengFangMen, true);
			FemaleTeacher.Instance.canMove = false;
			FemaleTeacher.Instance.canRotate = false;
			yield return topPanel.CloseEyeAnim();
			yield return FemaleTeacher.Instance.SetTransform(Interactive.Get("学生房站位_女老师").transform);
			StudentMather.Instance.SetTransform(Interactive.Get("学生房站位_母亲").transform);
			yield return topPanel.OpenEyeAnim();
			FemaleTeacher.Instance.canMove = true;
			FemaleTeacher.Instance.canRotate = true;
			onVisitPanel.GenerateReportData("了解情况_观环境", 2);
			//展能力
			mainPanel.NextTmp();
			yield return ObjHighlightClickCallBack(FemaleStudent.Instance.gameObject, true);
			yield return PlayAudio("母亲抱怨孩子看平板", 0, "这孩子经常呆在家中看平板，一看就是一整天。我们夫妻想再要一个，也没时间管她。");
			yield return RecordSpeech(new string[] { "父母关注", "好看", "画" });
			yield return FemaleStudent.Instance.PlayAnim("下床");
			FemaleStudent.Instance.SetTransform(Interactive.Get("学生房站位_学生").transform);
			yield return PlayAudio("女学生这是我的得意之作", 2, "这是我的得意之作！");
			onVisitPanel.GenerateReportData("了解情况_展能力", 3);
			//查细节
			mainPanel.NextTmp();
			yield return ObjHighlightClickCallBack(Interactive.Get("平板"), true);
			yield return RecordSpeech(new string[] { "平板都不玩", "关心", "多孩" });
			yield return PlayAudio("母亲老师您说得是", 0, "老师您说得是。");
			onVisitPanel.GenerateReportData("了解情况_查细节", 3);
			//说学校
			mainPanel.NextTmp();
			topPanel.tmpTip.text = "请对办学理念，成绩特色，校纪校规进行简单的叙述";
			GameObject XueShengFangWaiChuWeiZhi = Interactive.Get("学生房外出位置");
			yield return StudentMather.Instance.Walk(XueShengFangWaiChuWeiZhi.transform);
			GameObject KeTingZuoWei_MuQing = Interactive.Get("客厅坐位_母亲");
			StudentMather.Instance.SetTransform(KeTingZuoWei_MuQing.transform);
			yield return AreaHighlight(Interactive.Get("学生房外出高亮正方体").GetComponent<ObjColliderEvent>());
			GameObject XueShengFangMenWaiWeiZhi = Interactive.Get("学生房门外位置");
			yield return FemaleTeacher.Instance.SetTransform(XueShengFangMenWaiWeiZhi.transform);
			yield return StudentMather.Instance.SitDown(KeTingZuoWei_MuQing.transform);
			yield return StudentFather.Instance.SitDown(Interactive.Get("客厅坐位_父亲").transform);
			yield return AreaHighlight(Interactive.Get<ObjColliderEvent>("女老师坐下的位置"));
			yield return FemaleTeacher.Instance.SitDown(Interactive.Get("客厅坐位_女老师").transform);
			yield return RecordSpeech(new string[] { "以人为本", "校规", "素质教育" });
			onVisitPanel.GenerateReportData("了解情况_说学校", 4);
			//聊班级
			mainPanel.NextTmp();
			topPanel.tmpTip.text = "请对班级概况，发展目标，任课老师进行简单的叙述，并讲述光美的班中职务";
			yield return RecordSpeech(new string[] { "班级概况", "发展目标", "任课老师", "宣传委员" });
			onVisitPanel.GenerateReportData("了解情况_聊班级", 4);
			//建立联系
			//摸情况
			mainPanel.NextStep();
			mainPanel.NextTmp();
			topPanel.tmpTip.text = "请询问孩子身体情况，特别需求，明确家长接送时间，工作安排";
			yield return RecordSpeech(new string[] { "身体情况", "特别需求", "上学方式", "家长会" });
			yield return PlayAudio("母亲说光美需求", 0, "光美身体不太好，容易感冒。平时都是我（母）开车送她去学。有家长会我一定到。");
			onVisitPanel.GenerateReportData("建立联系_摸情况", 4);
			//明期望
			mainPanel.NextTmp();
			topPanel.tmpTip.text = "请明确家长的教育需求";
			yield return RecordSpeech(new string[] { "班级岗位职务", "多孩", "发展目标" });
			yield return PlayAudio("母亲对光美的期望", 0, "我们希望她尽量不受多孩的影响，提高成绩。");
			onVisitPanel.GenerateReportData("建立联系_明期望", 3);
			//适当指导
			//缓焦虑
			mainPanel.NextStep();
			mainPanel.NextTmp();
			topPanel.tmpTip.text = "引导家长关心孩子心理健康，并提醒家长消除多孩影响不能单靠老师";
			yield return RecordSpeech(new string[] { "心理健康", "单靠老师", "多孩" });
			yield return PlayAudio("母亲承诺多关心光美心理健康", 0, "您说的是，心里健康很重要。我们之后会尽量抽出时间关心她的。");
			onVisitPanel.GenerateReportData("适当指导_缓焦虑", 3);
			//答疑惑
			mainPanel.NextTmp();
			yield return RecordSpeech(new string[] { "自信心", "父母支持", "开朗" });
			onVisitPanel.GenerateReportData("适当指导_答疑惑", 3);
			//表达感谢
			//表达感谢
			mainPanel.NextStep();
			mainPanel.NextTmp();
			topPanel.tmpTip.text = "拒绝礼物并送学生钢笔";
			yield return StudentFather.Instance.PlayAnim("站起");
			yield return FemaleTeacher.Instance.StandUp();
			yield return PlayAudio("父亲感谢老师", 4, "谢谢老师的指导，希望以后能在学校里多关注孩子");
			yield return StudentFather.Instance.PlayAnim("送礼");
			yield return PlayAudio("父亲送礼", 4, "这是点小心意，还请务必收下");

			yield return onVisitPanel.ShowExpressGratitude();
			yield return RecordSpeech(new string[] { "不接受任何形式的礼物", "感谢接待和配合", "谢谢" });
			yield return FemaleTeacher.Instance.PlayAnim("拒绝礼品");
			FemaleTeacher.Instance.canMove = true;
			FemaleTeacher.Instance.canRotate = true;
			yield return AreaHighlight(Interactive.Get("客厅门口高亮正方体").GetComponent<ObjColliderEvent>());
			yield return FemaleTeacher.Instance.PlayAnim("站立",false);
			FemaleTeacher.Instance.canMove = false;
			FemaleTeacher.Instance.canRotate = false;
			yield return topPanel.CloseEyeAnim();
			FemaleStudent.Instance.SetTransform(XueShengFangMenWaiWeiZhi.transform);
			FemaleStudent.Instance.agent.enabled = true;
			FemaleTeacher.Instance.transform.position = KeTingMenKouZhanWei.transform.position;
			FemaleTeacher.Instance.transform.forward = -1 * KeTingMenKouZhanWei.transform.forward;
			yield return topPanel.OpenEyeAnim();
			FemaleTeacher.Instance.canRotate = true;
			yield return FemaleStudent.Instance.Walk(Interactive.Get("客厅站位_学生").transform);
			yield return PlayAudio("女学生感谢老师", 2, "老师，谢谢你来看我，给你花");
			yield return FemaleStudent.Instance.PlayAnim("送礼");
			yield return onVisitPanel.ShowExpressGratitude();
			yield return RecordSpeech(new string[] { "祝福", "钢笔" });
			yield return FemaleTeacher.Instance.PlayAnim("送礼");
			onVisitPanel.SetMask(true);
			yield return topPanel.CloseEyeAnim();
			mainPanel.SetBK(true);
			onVisitPanel.SetMask(false);
			yield return topPanel.OpenEyeAnim();
			yield return LoadStudentInformationPaper();
			onVisitPanel.GenerateReportData("表达感谢", 13);
			onVisitPanel.ShowNext();
		}

		IEnumerator AreaHighlight(ObjColliderEvent obj)
		{
			yield return obj.AreaHighlight();
			yield return FemaleTeacher.Instance.PlayAnim("站立",false);
		}

		#region 播放家长语音并显示文字
		IEnumerator PlayAudio(string clipName, int spriteIndex, string strWord)
		{
			onVisitPanel.ShowParentWord(spriteIndex, strWord);
			yield return AudioManager.Instance.Play(clipName);
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
		}
		#endregion

		#region 开始语音识别
		IEnumerator RecordSpeech(string[] keywords)
		{
			StartCoroutine(mainPanel.InitKeyWords(keywords));
			onVisitPanel.ShowRecordUI(keywords);
			yield return new WaitUntil(() => { return onVisitPanel.recordState == RecordState.ResultIsRight; });
			onVisitPanel.CloseRecord();
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
			yield return WebKit.GetInstance().Read<List<SingleTitleData>>(Settings.PAPER + "Paper_OnVisit2.json", d =>
			{
				datas = d;
			});
			yield return StartExam(datas);
		}
		#endregion
	}
}

