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
using Cysharp.Threading.Tasks;

namespace HomeVisit.Task
{
    public partial class SingleParentTask : MonoSingleton<SingleParentTask>
    {
        OnVisitPanel onVisitPanel = null;
        MainPanel mainPanel = null;
        TopPanel topPanel = null;

        bool isExaming = false;
        string strStudentDoFruitOnTable;

        private void Start()
        {
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
            StudentFather.Instance.gameObject.SetActive(false);
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
            yield return KeTingMen.WaitHighlightClick(true);
            yield return StudentMather.Instance.Walk(Interactive.Get("客厅门口站位").transform);
            yield return AnimMgr.GetInstance().Play(KeTingMen.GetComponent<Animation>(), "OpenDoor");
            yield return StudentMather.Instance.Walk(Interactive.Get<Transform>("客厅站位_母亲"));
            yield return FemaleTeacher.Instance.Walk(Interactive.Get<Transform>("客厅站位_女老师"));
            yield return AnimMgr.GetInstance().Play(KeTingMen.GetComponent<Animation>(), "CloseDoor");
            FemaleTeacher.Instance.canMove = true;
            FemaleTeacher.Instance.canRotate = true;
			yield return onVisitPanel.WaitSelectionOptions(selectionObservingEnvironment).ToCoroutine();
            yield return RecordSpeech(new string[] { "咱们 ", "孩子房间", "看看" });
            yield return TaskHelper.PlayAudio("母亲_唉，好吧", 0, "唉，好吧。");
            yield return Interactive.Get("学生房间高亮正方体").WaitHighlightEnter();
            GameObject XueShengFangMen = Interactive.Get("学生房门");
            XueShengFangMen.WaitHighlightClick(true);
            FemaleTeacher.Instance.canMove = false;
            FemaleTeacher.Instance.canRotate = false;
            yield return topPanel.CloseEyeAnim();
            StudentMather.Instance.SetTransform(Interactive.Get("学生房站位_母亲").transform);
            yield return FemaleTeacher.Instance.SetTransform(Interactive.Get("学生房站位_女老师").transform);
            yield return topPanel.OpenEyeAnim();
            FemaleTeacher.Instance.canMove = true;
            FemaleTeacher.Instance.canRotate = true;
            onVisitPanel.GenerateReportData("了解情况_观环境", 3);
            //展能力
            mainPanel.NextTmp();

            #region 贫穷家庭

            if (Settings.OldRandomScene == "ToBeDeveloped")
            {
				yield return onVisitPanel.WaitSelectionOptions(selectionPoor).ToCoroutine();
                yield return Interactive.Get("俄狄浦斯").WaitHighlightClick(true);

                yield return RecordSpeech(new string[] { "喜欢", "俄狄浦斯", "哪部分" });
                yield return StudentController.Instance.PlayAnim("下床");
                StudentController.Instance.SetTransform(Interactive.Get("学生房站位_学生").transform);
                yield return TaskHelper.PlayAudio("男学生_俄狄浦斯", 2, "俄狄浦斯的命运，我就受到了命运的捉弄。");
                onVisitPanel.GenerateReportData("了解情况_展能力", 3);
            }

            #endregion

            #region 普通家庭
            if (Settings.OldRandomScene == "Developing")
            {
                yield return Interactive.Get("画").WaitHighlightClick(true);
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
                yield return FemaleTeacher.Instance.PlayAnim("鼓掌");
                yield return RecordSpeech(new string[] { "孩子", "钢琴", "好听" });
                StudentController.Instance.anim.speed = 0;
            }

            #endregion

            onVisitPanel.GenerateReportData("了解情况_展能力", 3);
            //查细节
            mainPanel.NextTmp();
            yield return RecordSpeech(new string[] { "爱好很重要", "全面发展" });
            yield return TaskHelper.PlayAudio("母亲老师您说得是", 0, "老师您说得是");
            yield return RecordSpeech(new string[] { "素质", "育人" });
            onVisitPanel.GenerateReportData("了解情况_查细节", 4);
            //说学校
            mainPanel.NextTmp();
            topPanel.tmpTip.text = "请对办学理念，成绩特色，校纪校规进行简单的叙述";
            GameObject XueShengFangWaiChuWeiZhi = Interactive.Get("学生房外出位置");
            yield return StudentMather.Instance.Walk(XueShengFangWaiChuWeiZhi.transform);
            GameObject KeTingZuoWei_MuQing = Interactive.Get("客厅坐位_母亲");
            StudentMather.Instance.SetTransform(KeTingZuoWei_MuQing.transform);
            StudentController.Instance.agent.enabled = true;
            yield return StudentController.Instance.Walk(XueShengFangWaiChuWeiZhi.transform);
            GameObject KeTingZuoWei_XueSheng = Interactive.Get("客厅坐位_学生");
            StudentController.Instance.SetTransform(KeTingZuoWei_XueSheng.transform);
            
            yield return Interactive.Get("学生房外出高亮正方体").WaitHighlightEnter();
            yield return FemaleTeacher.Instance.SetTransform(Interactive.Get("学生房门外位置").transform);
            yield return StudentMather.Instance.SitDown(KeTingZuoWei_MuQing.transform);
            yield return StudentController.Instance.SitDown(KeTingZuoWei_XueSheng.transform);
            yield return Interactive.Get("女老师坐下的位置").WaitHighlightEnter();
            yield return FemaleTeacher.Instance.SitDown(Interactive.Get("客厅坐位_女老师").transform);
            yield return RecordSpeech(new string[] { "育才先育人", "校规", "身心健康", "家长会" });
            yield return TaskHelper.PlayAudio("母亲老师您说得是", 0, "老师您说得是！");
            onVisitPanel.GenerateReportData("了解情况_说学校", 4);
            //聊班级
            yield return onVisitPanel.WaitSelectionOptions(selectionExplainClass).ToCoroutine();
            topPanel.tmpTip.text = "请对班级概况，任课老师进行简单的叙述，并说明彦威朋友不少却情绪低落。";
            yield return RecordSpeech(new string[] { "班级概况", "任课老师", "情绪低落", "朋友不少" });
            yield return TaskHelper.PlayAudio("母亲_嗯嗯嗯，我会更加关心他的", 0, "嗯嗯嗯，我会更加关心孩子的。");
            onVisitPanel.GenerateReportData("了解情况_聊班级", 4);
            //建立联系
            //摸情况
            mainPanel.NextStep();
            mainPanel.NextTmp();
			yield return onVisitPanel.WaitSelectionOptions(selectionFindOutSituation).ToCoroutine();
			topPanel.tmpTip.text = "请询问孩子身体情况，特别需求，明确家长接送时间，工作安排";
            yield return RecordSpeech(new string[] { "身体情况", "家长会", "接送时间", "工作安排" });
            yield return TaskHelper.PlayAudio("母亲_单亲需求", 0, "没啥特别需求，但是家里有些事忙不过来，没法参与家长会。平时都是孩子自己上下学。");
            onVisitPanel.GenerateReportData("建立联系_摸情况", 4);
            //明期望
            mainPanel.NextTmp();
			yield return onVisitPanel.WaitSelectionOptions(selectionClarifyExpectations).ToCoroutine();
			topPanel.tmpTip.text = "请明确家长的教育需求";
            yield return RecordSpeech(new string[] { "特别需求", "发展目标" });
            yield return TaskHelper.PlayAudio("母亲_单亲教育需求", 0, "只要孩子没啥事就行，其他的就全交给老师您了。");
            onVisitPanel.GenerateReportData("建立联系_明期望", 2);
            //适当指导
            //缓焦虑
            mainPanel.NextStep();
            mainPanel.NextTmp();
            topPanel.tmpTip.text = "引导家长关心孩子心理健康，并提醒家长消除单亲影响不能单靠老师";
            yield return RecordSpeech(new string[] { "单亲", "单独家访", "不能放养" });
            yield return TaskHelper.PlayAudio("母亲_太谢谢您了，孩子我会尽量多关注的。", 0, "太谢谢您了，孩子我会尽量多关注的。");
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
            yield return StudentMather.Instance.PlayAnim("站起");
            yield return StudentController.Instance.PlayAnim("站起");
            yield return FemaleTeacher.Instance.StandUp();
            FemaleTeacher.Instance.transform.LookAt(StudentMather.Instance.transform);
            StudentMather.Instance.transform.LookAt(FemaleTeacher.Instance.transform);
            yield return TaskHelper.PlayAudio("母亲_送礼", 0, "这是点小心意，还请务必收下。");
            yield return StudentMather.Instance.PlayAnim("送礼");
            yield return onVisitPanel.ShowExpressGratitude();
            yield return RecordSpeech(new string[] { "不接受任何形式的礼物", "感谢接待和配合", "谢谢" });
            yield return FemaleTeacher.Instance.PlayAnim("拒绝礼品");
            FemaleTeacher.Instance.canMove = true;
            FemaleTeacher.Instance.canRotate = true;
            yield return Interactive.Get("客厅门口高亮正方体").WaitHighlightEnter();
            onVisitPanel.SetMask(true);
            yield return topPanel.CloseEyeAnim();
            mainPanel.SetBK(true);
            onVisitPanel.SetMask(false);
            yield return LoadStudentInformationPaper();
            onVisitPanel.GenerateReportData("表达感谢", 13);
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
       IEnumerator StartExam(List<SingleTitleData> datas)
        {
            isExaming = true;
            UIKit.GetPanel<KnowledgeExamPanel>().LoadOnVisitPaper(datas);
            while (isExaming)
            {
                yield return null;
            }
        }

        IEnumerator LoadStudentInformationPaper()
        {
            List<SingleTitleData> datas = null;
            yield return WebKit.GetInstance()
                .Read<List<SingleTitleData>>(Settings.PAPER + "Paper_OnVisit3.json", d => { datas = d; });
            yield return topPanel.OpenEyeAnim();
            yield return StartExam(datas);
        }
    }
}