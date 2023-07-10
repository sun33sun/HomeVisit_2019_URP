using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBase;
using HomeVisit.Character;
using HighlightPlus;
using HomeVisit.Effect;
using HomeVisit.UI;
using QFramework;
using System.Text;
using ProjectBase.Anim;
using System;

namespace HomeVisit.Task
{
	public class TraditionTask : SingletonMono<TraditionTask>
	{
		[SerializeField] List<Sprite> sprites;

		OnVisitPanel onVisitPanel = null;
		MainPanel mainPanel = null;
		TopPanel buttonPanel = null;

		bool isExaming = false;
		string strStudentDoFruitOnTable;

		private void Start()
		{
			EventCenter.GetInstance().AddEventListener<string>("<b>题目：</b>请输入学生的行为错在那些地方", InputExamCallBack);
		}
		void InputExamCallBack(string newStr)
		{
			strStudentDoFruitOnTable = newStr;
			isExaming = false;
		}

		private void OnDisable()
		{
			StopAllCoroutines();
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
			StudentController.Instance.transform.position = Interactive.Get("学生房站位_学生").transform.position;
			StudentController.Instance.transform.forward = Interactive.Get("学生房站位_学生").transform.forward;

			//观环境
			mainPanel.StartTMP();
			yield return CameraManager.Instance.ThirdPerson(FemaleTeacher.Instance.thirdView, 2f);
			mainPanel.SetBK(false);
			yield return FemaleTeacher.Instance.PlayAnim("手机调为静音");
			yield return FemaleTeacher.Instance.PlayAnim("敲门");
			GameObject KeTingMen = Interactive.Get("客厅门");
			EffectManager.Instance.AddEffectImmediately(KeTingMen);
			yield return EventManager.Instance.AddObjClick(KeTingMen);
			yield return StudentMather.Instance.Walk(Interactive.Get("客厅门口站位").transform);
			yield return AnimationManager.GetInstance().Play(KeTingMen.GetComponent<Animation>(), "OpenDoor");
			yield return StudentMather.Instance.Walk(Interactive.Get<Transform>("客厅站位_母亲"));
			yield return PlayAudio("母亲欢迎老师进门", 0, "老师，您好！欢迎欢迎，请进，请进！");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			yield return FemaleTeacher.Instance.Walk(Interactive.Get<Transform>("客厅站位_女老师"));
			yield return AnimationManager.GetInstance().Play(KeTingMen.GetComponent<Animation>(), "CloseDoor");
			FemaleTeacher.Instance.canMove = true;
			FemaleTeacher.Instance.canRotate = true;
			yield return RecordSpeech(new string[] { "谢谢", "参观" });
			yield return PlayAudio("母亲认可", 0, "好的！没问题");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			yield return Interactive.Get<ObjColliderEvent>("学生房间高亮正方体").AreaHighlight();
			GameObject XueShengFangMen = Interactive.Get("学生房门");
			yield return ObjHighlightClickCallBack(XueShengFangMen, true);
			yield return AnimationManager.GetInstance().Play(XueShengFangMen.GetComponent<Animation>(), "OpenDoor");
			onVisitPanel.GenerateReportData("了解情况_观环境", 2);
			//展能力
			mainPanel.NextTmp();
			yield return ObjHighlightClickCallBack("画", true);
			yield return StudentMather.Instance.Walk(Interactive.Get("学生房站位_母亲").transform);
			yield return PlayAudio("母亲说孩子画画", 0, "这幅画是我们孩子平时自己画的，他比较喜欢画画，所以我们都给挂起来啦！");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			yield return RecordSpeech(new string[] { "挺好", "画" });
			onVisitPanel.CloseRecord();
			yield return FemaleTeacher.Instance.PlayAnim("鼓掌");
			onVisitPanel.GenerateReportData("了解情况_展能力", 2);
			//查细节
			mainPanel.NextTmp();
			buttonPanel.tmpTip.text = "请观察亲子关系，关注礼仪，并进行记录";
			yield return StudentMather.Instance.Walk(Interactive.Get("客厅坐位_母亲").transform);
			StudentController.Instance.agent.enabled = true;
			yield return StudentController.Instance.Walk(Interactive.Get("客厅坐位_学生").transform);
			yield return StudentMather.Instance.SitDown(Interactive.Get("客厅坐位_母亲").transform);
			yield return StudentFather.Instance.PlayAnim("SitDown");
			yield return StudentController.Instance.SitDown(Interactive.Get("客厅坐位_学生").transform);
			yield return Interactive.Get<ObjColliderEvent>("女老师坐下的位置").AreaHighlight();
			FemaleTeacher.Instance.canMove = false;
			yield return FemaleTeacher.Instance.SitDown(Interactive.Get("客厅坐位_女老师").transform);
			StudentController.Instance.PlayAnim("Naughty");
			yield return ObjHighlightClickCallBack(StudentController.Instance.gameObject, true);
			yield return StartInputExam("<b>题目：</b>请输入学生的行为错在那些地方");
			StudentController.Instance.StopAnim();
			onVisitPanel.GenerateReportData("了解情况_查细节", 2);
			//说学校
			mainPanel.NextTmp();
			buttonPanel.tmpTip.text = "请对办学理念，成绩特色，校纪校规进行简单的叙述";
			yield return RecordSpeech(new string[] { "学风", "作息", "学校特色", "校规" });
			onVisitPanel.CloseRecord();
			onVisitPanel.GenerateReportData("了解情况_说学校", 4);
			//聊班级
			mainPanel.NextTmp();
			buttonPanel.tmpTip.text = "请对班级概况，发展目标，任课老师进行简单的叙述";
			yield return RecordSpeech(new string[] { "班级概况", "发展目标", "任课老师" });
			onVisitPanel.CloseRecord();
			onVisitPanel.GenerateReportData("了解情况_聊班级", 3);
			//建立联系
			//摸情况
			mainPanel.NextStep();
			mainPanel.NextTmp();
			buttonPanel.tmpTip.text = "请询问孩子身体情况，特别需求，明确家长接送时间，工作安排";
			yield return RecordSpeech(new string[] { "家庭情况", "个性特点", "情绪", "学习经历", "特长优势", "在家表现", "家长期望", "家长诉求", "特别关注" });//9
			yield return PlayAudio("母亲想孩子坐前排", 0, "因为孩子注意比较不集中，想坐在靠前排。平时上学放学基本都是自己坐公交、地铁。");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			onVisitPanel.GenerateReportData("建立联系_摸情况", 9);
			//明期望
			mainPanel.NextTmp();
			buttonPanel.tmpTip.text = "请明确家长的教育需求";
			yield return RecordSpeech(new string[] { "班级岗位职务", "学习期望", "发展目标", "志愿理想" });
			yield return PlayAudio("母亲说注意方式方法", 0, "嗯嗯，老师说的这些我们明白，之后我们也会注意方式方法。");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			onVisitPanel.GenerateReportData("建立联系_明期望", 4);
			//适当指导
			//缓解焦虑
			mainPanel.NextStep();
			mainPanel.NextTmp();
			buttonPanel.tmpTip.text = "指导家长引导学生进行时间管理，为孩子创造一个安静的学习环境和学习氛围";
			yield return RecordSpeech(new string[] { "学习环境", "家庭氛围", "合理分配时间", "和谐", "学习习惯", "生活习惯", "劳动能力" });//7
			yield return PlayAudio("母亲认可创造学习环境", 0, "嗯嗯，我们以后会为孩子创造一个安静的学习环境和学习氛围。");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);
			onVisitPanel.GenerateReportData("适当指导_缓焦虑", 7);
			//答疑惑
			mainPanel.NextTmp();
			yield return RecordSpeech(new string[] {"鼓励", "及时沟通", "相互理解" });
			onVisitPanel.CloseRecord();
			onVisitPanel.GenerateReportData("适当指导_答疑惑", 3);
			//表达感谢
			mainPanel.NextStep();
			mainPanel.NextTmp();
			buttonPanel.tmpTip.text = "拒绝礼物并送学生钢笔";
			StudentMather.Instance.PlayAnim("StandUp");
			StudentFather.Instance.PlayAnim("StandUp");
			yield return FemaleTeacher.Instance.StandUp();
			yield return StudentFather.Instance.PlayAnim("GiveGift");
			yield return PlayAudio("父亲感谢老师", 4, "谢谢老师的指导，希望以后能在学校里多关注孩子");
			yield return PlayAudio("父亲送礼", 4, "这是点小心意，还请务必收下");
			onVisitPanel.btnDialogue.gameObject.SetActive(false);

			yield return RecordSpeech(new string[] { "不接受任何形式的礼物", "感谢接待和配合", "谢谢" });
			yield return onVisitPanel.ShowExpressGratitude();
			FemaleTeacher.Instance.canMove = true;
			FemaleTeacher.Instance.canRotate = true;
			yield return Interactive.Get("客厅门口高亮正方体").GetComponent<ObjColliderEvent>().AreaHighlight();
			onVisitPanel.GenerateReportData("表达感谢", 6);
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

		WaitUntil ObjHighlightClickCallBack(string objName, bool isCallBack)
		{
			GameObject targetObj = Interactive.Get(objName);
			targetObj.SetActive(true);
			return ObjHighlightClickCallBack(targetObj, isCallBack);
		}
		#endregion

		#region 考核

		WaitUntil StartInputExam(string strTip)
		{
			isExaming = true;
			onVisitPanel.ShowOnVisitInput(strTip);
			return new WaitUntil(() => { return !isExaming; });
		}
		#endregion
	}
}

