using Cysharp.Threading.Tasks;
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
	public partial class MultipleChildrenTask : MonoSingleton<MultipleChildrenTask>
	{
		OnVisitPanel onVisitPanel = null;
		MainPanel mainPanel = null;
		TopPanel topPanel = null;

		bool isExaming = false;
		string strStudentDoFruitOnTable;
		DateTime startTime;

		private void Start()
		{
			startTime = DateTime.UtcNow;
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

		private void OnEnable()
		{
			if (onVisitPanel == null)
				onVisitPanel = UIKit.GetPanel<OnVisitPanel>();
			if (mainPanel == null)
				mainPanel = UIKit.GetPanel<MainPanel>();
			if (topPanel == null)
				topPanel = UIKit.GetPanel<TopPanel>();
		}

		public IEnumerator StartTask()
		{
			if (Settings.OldRandomScene == "ToBeDeveloped")
			{
				StudentController.Instance.SetTransform(Interactive.Get("学生蜷缩位置").transform);
				yield return StudentController.Instance.PlayAnim("蜷缩", false);
			}
			else
			{
				StudentController.Instance.SetTransform(Interactive.Get("学生房站位_学生").transform);
			}
			Interactive.Get("二胎学生").SetActive(true);

			//观环境
			mainPanel.StartTMP();
			yield return CameraManager.Instance.ThirdPerson(FemaleTeacher.Instance.head, 2f);
			yield return topPanel.OpenEyeAnim();
			yield return FemaleTeacher.Instance.PlayAnim("手机调为静音");
			yield return FemaleTeacher.Instance.PlayAnim("敲门");
			GameObject KeTingMen = Interactive.Get("客厅门");
			yield return KeTingMen.WaitHighlightClick();
			GameObject KeTingMenKouZhanWei = Interactive.Get("客厅门口站位");
			yield return StudentMather.Instance.Walk(KeTingMenKouZhanWei.transform);
			yield return AnimMgr.GetInstance().Play(KeTingMen.GetComponent<Animation>(), "OpenDoor");
			yield return TaskHelper.PlayAudio("母亲欢迎老师进门", 0, "老师，您好！欢迎欢迎，请进，请进！");
			yield return StudentMather.Instance.Walk(Interactive.Get<Transform>("客厅站位_母亲"));
			yield return FemaleTeacher.Instance.Walk(Interactive.Get<Transform>("客厅站位_女老师"));
			yield return AnimMgr.GetInstance().Play(KeTingMen.GetComponent<Animation>(), "CloseDoor");
			FemaleTeacher.Instance.canMove = true;
			FemaleTeacher.Instance.canRotate = true;
			yield return RecordSpeech(new string[] { "学生房间", "参观" });
			yield return TaskHelper.PlayAudio("母亲认可", 0, "好的！没问题");
			Interactive.Get("学生房间高亮正方体").WaitHighlightEnter();
			FemaleTeacher.Instance.PlayAnim("站立");
			GameObject XueShengFangMen = Interactive.Get("学生房门");
			yield return XueShengFangMen.WaitHighlightClick();
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
			#region 贫穷家庭
			if (Settings.OldRandomScene == "ToBeDeveloped")
			{
				yield return onVisitPanel.WaitSelectionOptions(selectionPoor).ToCoroutine();
				yield return Interactive.Get("俄狄浦斯").WaitHighlightClick();
				yield return RecordSpeech(new string[] { "喜欢", "俄狄浦斯", "哪部分" });
				yield return StudentController.Instance.PlayAnim("下床");
				StudentController.Instance.SetTransform(Interactive.Get("学生房站位_学生").transform);
				yield return TaskHelper.PlayAudio("男学生_俄狄浦斯", 2, "俄狄浦斯的命运，我就受到了命运的捉弄。");
			}
			#endregion
			#region 普通家庭
			if (Settings.OldRandomScene == "Developing")
			{
				yield return Interactive.Get("画").WaitHighlightClick();
				yield return onVisitPanel.WaitSelectionOptions(selectionNormal).ToCoroutine();
				yield return RecordSpeech(new string[] { "挺好", "画", "风格明快" });
				yield return FemaleTeacher.Instance.PlayAnim("鼓掌");
				yield return StudentMather.Instance.PlayAnim("鼓掌");
			}
			#endregion
			#region 富裕家庭
			if (Settings.OldRandomScene == "Developed")
			{
				yield return onVisitPanel.WaitSelectionOptions(selectionRice).ToCoroutine();
				Transform GangQinWeiZhi = Interactive.Get("钢琴位置").transform;
				yield return StudentController.Instance.Walk(GangQinWeiZhi);
				yield return StudentController.Instance.SitDown(GangQinWeiZhi);
				StartCoroutine(StudentController.Instance.PlayAnim("弹琴", false));
				yield return new WaitForSeconds(2);
				yield return FemaleTeacher.Instance.PlayAnim("鼓掌");
				yield return RecordSpeech(new string[] { "孩子", "钢琴", "好听" });
				StudentController.Instance.anim.speed = 0;
			}
			#endregion
			onVisitPanel.GenerateReportData("了解情况_展能力", 3);

			//查细节
			mainPanel.NextTmp();
			yield return RecordSpeech(new string[] { "爱好", "全面发展" });
			yield return TaskHelper.PlayAudio("母亲老师您说得是", 0, "老师您说得是");
			yield return RecordSpeech(new string[] { "育人" });
			onVisitPanel.GenerateReportData("了解情况_查细节", 4);
			//说学校
			mainPanel.NextTmp();
			topPanel.tmpTip.text = "请对办学理念，成绩特色，校纪校规进行简单的叙述";
			GameObject XueShengFangWaiChuWeiZhi = Interactive.Get("学生房外出位置");
			yield return StudentMather.Instance.Walk(XueShengFangWaiChuWeiZhi.transform);
			GameObject KeTingZuoWei_MuQing = Interactive.Get("客厅坐位_母亲");
			StudentMather.Instance.SetTransform(KeTingZuoWei_MuQing.transform);
			yield return Interactive.Get("学生房外出高亮正方体").WaitHighlightEnter();
			GameObject XueShengFangMenWaiWeiZhi = Interactive.Get("学生房门外位置");
			yield return FemaleTeacher.Instance.SetTransform(XueShengFangMenWaiWeiZhi.transform);
			yield return StudentMather.Instance.SitDown(KeTingZuoWei_MuQing.transform);
			yield return StudentFather.Instance.SitDown(Interactive.Get("客厅坐位_父亲").transform);
			yield return Interactive.Get("女老师坐下的位置").WaitHighlightEnter();
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
			yield return onVisitPanel.WaitSelectionOptions(selectionFindOutSituation).ToCoroutine();
			topPanel.tmpTip.text = "请询问孩子身体情况，特别需求，明确家长接送时间，工作安排";
			yield return RecordSpeech(new string[] { "身体情况", "特别需求", "上学方式", "家长会" });
			yield return TaskHelper.PlayAudio("母亲说光美需求", 0, "光美身体不太好，容易感冒。平时都是我（母）开车送孩子去学。有家长会我一定到。");
			onVisitPanel.GenerateReportData("建立联系_摸情况", 4);
			//明期望
			mainPanel.NextTmp();
			yield return onVisitPanel.WaitSelectionOptions(selectionClarifyExpectations).ToCoroutine();
			topPanel.tmpTip.text = "请明确家长的教育需求";
			yield return RecordSpeech(new string[] { "班级岗位职务", "多孩", "发展目标" });
			yield return TaskHelper.PlayAudio("母亲对光美的期望", 0, "我们希望孩子尽量不受多孩的影响，提高成绩。");
			onVisitPanel.GenerateReportData("建立联系_明期望", 3);
			//适当指导
			//缓焦虑
			mainPanel.NextStep();
			mainPanel.NextTmp();
			yield return onVisitPanel.WaitSelectionOptions(selectionRelieveAnxiety).ToCoroutine();
			topPanel.tmpTip.text = "引导家长关心孩子心理健康，并提醒家长消除多孩影响不能单靠老师";
			yield return RecordSpeech(new string[] { "心理健康", "单靠老师", "多孩" });
			yield return TaskHelper.PlayAudio("母亲承诺多关心光美心理健康", 0, "您说的是，心理健康很重要。我们之后会尽量抽出时间关心孩子的。");
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
			yield return StudentMather.Instance.PlayAnim("站起");
			yield return FemaleTeacher.Instance.StandUp();
			FemaleTeacher.Instance.transform.LookAt(StudentFather.Instance.transform);
			StudentFather.Instance.transform.LookAt(FemaleTeacher.Instance.transform);
			yield return TaskHelper.PlayAudio("父亲感谢老师", 4, "谢谢老师的指导，希望以后能在学校里多关注孩子");
			yield return StudentFather.Instance.PlayAnim("送礼");
			yield return TaskHelper.PlayAudio("父亲送礼", 4, "这是点小心意，还请务必收下");

			yield return onVisitPanel.ShowExpressGratitude();
			yield return RecordSpeech(new string[] { "不接受任何形式的礼物", "感谢接待和配合", "谢谢" });
			yield return FemaleTeacher.Instance.PlayAnim("拒绝礼品");
			FemaleTeacher.Instance.canMove = true;
			FemaleTeacher.Instance.canRotate = true;
			yield return Interactive.Get("客厅门口高亮正方体").WaitHighlightEnter();
			yield return FemaleTeacher.Instance.PlayAnim("站立", false);
			FemaleTeacher.Instance.canMove = false;
			FemaleTeacher.Instance.canRotate = false;
			if (Settings.OldRandomScene == "Developed")
				StudentController.Instance.SetTransform(Interactive.Get("富孩出门站位").transform);
			else
				StudentController.Instance.SetTransform(XueShengFangMenWaiWeiZhi.transform);
			StudentController.Instance.agent.enabled = true;
			FemaleTeacher.Instance.transform.position = KeTingMenKouZhanWei.transform.position;
			FemaleTeacher.Instance.transform.forward = -1 * KeTingMenKouZhanWei.transform.forward;
			FemaleTeacher.Instance.canRotate = true;
			FemaleTeacher.Instance.canMove = true;
			yield return StudentController.Instance.Walk(Interactive.Get("客厅站位_学生").transform);
			switch (Settings.OldRandomScene)
			{
				case "ToBeDeveloped":
					yield return TaskHelper.PlayAudio("男学生感谢老师", 2, "老师，谢谢你来看我，给你花");
					break;
				case "Developing":
					yield return TaskHelper.PlayAudio("男学生感谢老师", 2, "老师，谢谢你来看我，给你花");
					break;
				case "Developed":
					yield return TaskHelper.PlayAudio("女学生感谢老师", 2, "老师，谢谢你来看我，给你花");
					break;
			}
			yield return StudentController.Instance.PlayAnim("送礼");
			yield return onVisitPanel.ShowExpressGratitude();
			yield return FemaleTeacher.Instance.PlayAnim("拒绝礼品");
			yield return RecordSpeech(new string[] { "祝福", "钢笔" });
			yield return FemaleTeacher.Instance.PlayAnim("送礼");
			if (Settings.OldRandomScene == "Developed")
			{
				yield return TaskHelper.PlayAudio("女学生_谢谢钢笔", 2, "谢谢老师的钢笔");
			}
			else
			{
				yield return TaskHelper.PlayAudio("男学生_谢谢钢笔", 2, "谢谢老师的钢笔");
			}
			onVisitPanel.SetMask(true);
			yield return topPanel.CloseEyeAnim();
			mainPanel.SetBK(true);
			onVisitPanel.SetMask(false);
			yield return LoadStudentInformationPaper();
			onVisitPanel.GenerateReportData("表达感谢", 13);
			onVisitPanel.ShowNext();
		}

		#region 开始语音识别
		IEnumerator RecordSpeech(string[] keywords)
		{
			StartCoroutine(FemaleTeacher.Instance.StartSpeak());
			StartCoroutine(mainPanel.InitKeyWords(keywords));
			onVisitPanel.ShowRecordUI(keywords);
			yield return new WaitUntil(() => { return onVisitPanel.recordState == RecordState.ResultIsRight; });
			onVisitPanel.CloseRecord();
			FemaleTeacher.Instance.StopSpeak();
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
			yield return topPanel.OpenEyeAnim();
			yield return StartExam(datas);
		}
		#endregion
	}
}