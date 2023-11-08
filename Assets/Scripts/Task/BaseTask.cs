using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using HomeVisit.Character;
using HomeVisit.UI;
using ProjectBase;
using QFramework;
using UnityEngine;

public class BaseTask
{
    protected OnVisitPanel _onVisitPanel;
    protected MainPanel _mainPanel;
    protected TopPanel _topPanel;

    public virtual async UniTask StartTask()
    {
        _onVisitPanel = UIKit.GetPanel<OnVisitPanel>();
        _mainPanel = UIKit.GetPanel<MainPanel>();
        _topPanel = UIKit.GetPanel<TopPanel>();
        await UniTask.Yield();
    }
    
    //播放家长语音并显示文字
    IEnumerator PlayAudio(string clipName, int spriteIndex, string strWord)
    {
        switch (spriteIndex)
        {
            case 0:
                MonoMgr.GetInstance().StartCoroutine(StudentMather.Instance.StartSpeak());
                break;
            case 2:
            case 3:
                MonoMgr.GetInstance().StartCoroutine(StudentController.Instance.StartSpeak());
                break;
            case 4:
                MonoMgr.GetInstance().StartCoroutine(StudentFather.Instance.StartSpeak());
                break;
        }
        _onVisitPanel.AddHistoryDialogue(spriteIndex, strWord);
        yield return AudioManager.Instance.Play(clipName);
        switch (spriteIndex)
        {
            case 0:
                StudentMather.Instance.StopSpeak();
                break;
            case 2:
            case 3:
                StudentController.Instance.StopSpeak();
                break;
            case 4:
                StudentFather.Instance.StopSpeak();
                break;
        }
        _onVisitPanel.btnDialogue.gameObject.SetActive(false);
    }

    protected IEnumerator RecordSpeech(string[] keywords)
    {
        MonoMgr.GetInstance().StartCoroutine(FemaleTeacher.Instance.StartSpeak());
        MonoMgr.GetInstance().StartCoroutine(_mainPanel.InitKeyWords(keywords));
        _onVisitPanel.ShowRecordUI(keywords);
        yield return new WaitUntil(() =>  _onVisitPanel.recordState == RecordState.ResultIsRight);
        _onVisitPanel.CloseRecord();
        FemaleTeacher.Instance.StopSpeak();
    }
}
