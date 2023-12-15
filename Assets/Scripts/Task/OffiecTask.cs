using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ProjectBase;
using ProjectBase.Anim;
using UnityEngine;
using UnityEngine.AI;

namespace HomeVisit.Task
{
    public class OffiecTask : SingletonMono<OffiecTask>
    {
        public async UniTask DoTask(Action callBack)
        {
            NavMeshAgent agent = Interactive.Get<NavMeshAgent>("女老师");
            Animator animator = agent.gameObject.GetComponent<Animator>();
            animator.Play("走路");
            Vector3 computerPos = Interactive.Get("电脑坐位").transform.position;
            agent.SetDestination(computerPos);
            await UniTask.WaitUntil(() => Vector3.Distance(agent.transform.position, computerPos) < 0.1);
            agent.transform.forward = Interactive.Get("电脑坐位").transform.forward;
            await AnimMgr.GetInstance().Play(animator, "坐下").ToUniTask(this);
            callBack?.Invoke();
        }
    }
}