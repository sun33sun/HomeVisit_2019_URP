using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using HomeVisit.Character;
using HomeVisit.Task;
using ProjectBase;
using ProjectBase.Anim;
using UnityEngine;

public class TraditionToBeDevelopedTask : BaseTask
{
    private ToBeDevelopedController _controller;
    private CancellationToken token;
    
    public TraditionToBeDevelopedTask()
    {
        this._controller = IOCMgr.GetInstance().Get<ToBeDevelopedController>() as ToBeDevelopedController;
        token = _controller.GetCancellationTokenOnDestroy();
    }
    
    public override async UniTask StartTask()
    {
        if(token.IsCancellationRequested)
            return;
        await base.StartTask();
        StudentController.Instance.SetTransform(_controller.学生蜷缩位置.transform);
        await StudentController.Instance.PlayAnim("蜷缩", false);
        //观环境
        await CameraManager.Instance.ThirdPerson(FemaleTeacher.Instance.head, 2f);
        await _topPanel.OpenEyeAnim();
        await FemaleTeacher.Instance.PlayAnim("手机调为静音");
        await FemaleTeacher.Instance.PlayAnim("敲门");
        await ExtensionFunction.HightlightClickAsync(_controller.客厅门, _controller.GetCancellationTokenOnDestroy());
        await StudentMather.Instance.Walk(_controller.客厅门口站位.transform);
        await AnimMgr.GetInstance().Play(_controller.客厅门.GetComponent<Animator>(), "OpenDoor");
        // await PlayAudio("母亲欢迎老师进门", 0, "老师，您好！欢迎欢迎，请进，请进！");
        await StudentMather.Instance.Walk(_controller.客厅站位_母亲.transform);
        await FemaleTeacher.Instance.Walk(_controller.客厅站位_女老师.transform);
        await AnimMgr.GetInstance().Play(_controller.客厅门.GetComponent<Animation>(), "CloseDoor");
        FemaleTeacher.Instance.canMove = true;
        FemaleTeacher.Instance.canRotate = true;
        await RecordSpeech(new string[] { "学生房间", "参观" });
        
    }
}
