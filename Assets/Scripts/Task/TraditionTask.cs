using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBase;
using HomeVisit.Character;
using HighlightPlus;
using HomeVisit.Effect;
using HomeVisit.UI;
using QFramework;
using ProjectBase.Anim;
using System;
using Cysharp.Threading.Tasks;

namespace HomeVisit.Task
{
	public partial class TraditionTask : MonoSingleton<TraditionTask>
	{
		OnVisitPanel onVisitPanel = null;
		MainPanel mainPanel = null;
		TopPanel topPanel = null;

		bool isExaming = false;
		string strStudentDoFruitOnTable;

		private void Start()
		{
			EventCenter.GetInstance().AddEventListener<string>("请输入学生的行为错在那些地方", InputExamCallBack);
		}
		void InputExamCallBack(string newStr)
		{
			strStudentDoFruitOnTable = newStr;
			isExaming = false;
		}

		private void OnDisable()
		{
			StopAllCoroutines();
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

			//观环境
			mainPanel.StartTMP();
			yield return CameraManager.Instance.ThirdPerson(FemaleTeacher.Instance.head, 2f);
			yield return topPanel.OpenEyeAnim();
			yield return FemaleTeacher.Instance.PlayAnim("手机调为静音");
			yield return FemaleTeacher.Instance.PlayAnim("敲门");
			GameObject KeTingMen = Interactive.Get("客厅门");
			EffectManager.Instance.AddEffectImmediately(KeTingMen);
			yield return EventManager.Instance.AddObjClick(KeTingMen);
			yield return StudentMather.Instance.Walk(Interactive.Get("客厅门口站位").transform);
			yield return AnimMgr.GetInstance().Play(KeTingMen.GetComponent<Animation>(), "OpenDoor");
			yield return TaskHelper.PlayAudio("母亲欢迎老师进门", 0, "老师，您好！欢迎欢迎，请进，请进！");
			yield return StudentMather.Instance.Walk(Interactive.Get<Transform>("客厅站位_母亲"));
			yield return FemaleTeacher.Instance.Walk(Interactive.Get<Transform>("客厅站位_女老师"));
			yield return AnimMgr.GetInstance().Play(KeTingMen.GetComponent<Animation>(), "CloseDoor");
			FemaleTeacher.Instance.canMove = true;
			FemaleTeacher.Instance.canRotate = true;
			yield return RecordSpeech(new string[] { "学生房间", "参观" });
			yield return TaskHelper.PlayAudio("母亲认可", 0, "好的！没问题");
			yield return Interactive.Get("学生房间高亮正方体").WaitHighlightEnter();
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
				yield return TaskHelper.PlayAudio("母亲_这孩子就知道看书", 0, "这孩子就知道看书。");
				yield return Interactive.Get("俄狄浦斯").WaitHighlightClick();
				yield return RecordSpeech(new string[] { "俄狄浦斯", "哪部分" });
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
				yield return RecordSpeech(new string[] { "挺好", "画" });
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
				yield return StudentController.Instance.PlayAnim("弹琴", false);
				yield return FemaleTeacher.Instance.PlayAnim("鼓掌");
				yield return RecordSpeech(new string[] { "钢琴", "好听" });
				StudentController.Instance.anim.speed = 0;
			}
			#endregion

			onVisitPanel.GenerateReportData("了解情况_展能力", 2);
			//查细节
			mainPanel.NextTmp();
			topPanel.tmpTip.text = "请观察亲子关系，关注礼仪，并进行记录";
			GameObject WaiChuWeiZhi = Interactive.Get("学生房外出位置");
			yield return StudentMather.Instance.Walk(WaiChuWeiZhi.transform);
			StudentMather.Instance.SetTransform(Interactive.Get("客厅坐位_母亲").transform);
			StudentController.Instance.agent.enabled = true;
			yield return StudentController.Instance.Walk(WaiChuWeiZhi.transform);
			StudentController.Instance.SetTransform(Interactive.Get("客厅坐位_学生").transform);
			yield return Interactive.Get("学生房外出高亮正方体").WaitHighlightEnter();
			yield return FemaleTeacher.Instance.SetTransform(Interactive.Get("学生房门外位置").transform);
			yield return StudentMather.Instance.SitDown(Interactive.Get("客厅坐位_母亲").transform);
			yield return StudentController.Instance.SitDown(Interactive.Get("客厅坐位_学生").transform);
			yield return StudentFather.Instance.SitDown(Interactive.Get("客厅坐位_父亲").transform);
			yield return Interactive.Get("女老师坐下的位置").WaitHighlightEnter();
			yield return FemaleTeacher.Instance.SitDown(Interactive.Get("客厅坐位_女老师").transform);
			StartCoroutine(StudentController.Instance.PlayAnim("乱动水果", false));

			yield return StudentController.Instance.gameObject.WaitHighlightClick(false);
			CameraManager.Instance.IsEnable = false;
			yield return StartInputExam("请输入学生的行为错在那些地方");
			CameraManager.Instance.IsEnable = true;
			yield return StudentController.Instance.PlayAnim("不再乱动", false);
			onVisitPanel.GenerateReportData("了解情况_查细节", 2);
			////说学校
			mainPanel.NextTmp();
			topPanel.tmpTip.text = "请对办学理念，成绩特色，校纪校规进行简单的叙述";
			yield return RecordSpeech(new string[] { "学风", "作息", "学校特色", "校规" });
			onVisitPanel.GenerateReportData("了解情况_说学校", 4);
			//聊班级
			mainPanel.NextTmp();
			topPanel.tmpTip.text = "请对班级概况，发展目标，任课老师进行简单的叙述";
			yield return RecordSpeech(new string[] { "班级概况", "发展目标", "任课老师" });
			onVisitPanel.GenerateReportData("了解情况_聊班级", 3);
			//建立联系
			//摸情况
			mainPanel.NextStep();
			mainPanel.NextTmp();
			topPanel.tmpTip.text = "请询问孩子身体情况，特别需求，明确家长接送时间，工作安排";
			yield return RecordSpeech(new string[] { "家庭情况", "个性特点", "情绪", "学习经历", "特长优势", "在家表现", "家长期望", "家长诉求", "特别关注" });//9
			yield return TaskHelper.PlayAudio("母亲想孩子坐前排", 0, "因为孩子注意比较不集中，想坐在靠前排。平时上学放学基本都是自己坐公交、地铁。");
			onVisitPanel.GenerateReportData("建立联系_摸情况", 9);
			//明期望
			mainPanel.NextTmp();
			topPanel.tmpTip.text = "请明确家长的教育需求";
			yield return onVisitPanel.WaitSelectionOptions(selectionClarifyExpectations).ToCoroutine();
			yield return RecordSpeech(new string[] { "班级岗位职务", "学习期望", "发展目标", "志愿理想" });
			yield return TaskHelper.PlayAudio("母亲说注意方式方法", 0, "嗯嗯，老师说的这些我们明白，之后我们也会注意方式方法。");
			onVisitPanel.GenerateReportData("建立联系_明期望", 4);
			//适当指导
			//缓焦虑
			mainPanel.NextStep();
			mainPanel.NextTmp();
			topPanel.tmpTip.text = "指导家长引导学生进行时间管理，为孩子创造一个安静的学习环境和学习氛围";
			yield return onVisitPanel.WaitSelectionOptions(selectionRelieveAnxiety).ToCoroutine();
			yield return RecordSpeech(new string[] { "学习环境", "家庭氛围", "合理分配时间", "和谐", "学习习惯", "生活习惯", "劳动能力" });//7
			yield return TaskHelper.PlayAudio("母亲认可创造学习环境", 0, "嗯嗯，我们以后会为孩子创造一个安静的学习环境和学习氛围。");
			onVisitPanel.GenerateReportData("适当指导_缓焦虑", 7);
			//答疑惑
			mainPanel.NextTmp();
			yield return RecordSpeech(new string[] { "鼓励", "及时沟通", "相互理解" });
			onVisitPanel.GenerateReportData("适当指导_答疑惑", 3);
			//表达感谢
			mainPanel.NextStep();
			mainPanel.NextTmp();
			topPanel.tmpTip.text = "拒绝礼物并送学生钢笔";
			yield return StudentFather.Instance.PlayAnim("站起");
			yield return StudentMather.Instance.PlayAnim("站起");
			yield return StudentController.Instance.PlayAnim("站起");
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
			mainPanel.SetBK(true);
			onVisitPanel.GenerateReportData("表达感谢", 6);
			yield return FemaleTeacher.Instance.PlayAnim("站立");
			onVisitPanel.ShowNext();
		}

		//开始语音识别
		IEnumerator RecordSpeech(string[] keywords)
		{
			StartCoroutine(FemaleTeacher.Instance.StartSpeak());
			StartCoroutine(mainPanel.InitKeyWords(keywords));
			onVisitPanel.ShowRecordUI(keywords);
			yield return new WaitUntil(() => { return onVisitPanel.recordState == RecordState.ResultIsRight; });
			onVisitPanel.CloseRecord();
			FemaleTeacher.Instance.StopSpeak();
		}

		//考核
		IEnumerator StartInputExam(string strTip)
		{
			isExaming = true;
			onVisitPanel.ShowOnVisitInput(strTip);

			while (isExaming)
			{
				yield return null;
			}
		}
	}
}

