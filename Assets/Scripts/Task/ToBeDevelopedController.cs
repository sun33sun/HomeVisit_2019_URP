using System;
using ProjectBase;
using UnityEngine;
using QFramework;

namespace HomeVisit.Task
{
	public partial class ToBeDevelopedController : ViewController
	{
		private void Awake()
		{
			IOCMgr.GetInstance().Set(this);
		}

		private void OnDestroy()
		{
			IOCMgr.GetInstance().Remove<ToBeDevelopedController>();
		}
	}
}
