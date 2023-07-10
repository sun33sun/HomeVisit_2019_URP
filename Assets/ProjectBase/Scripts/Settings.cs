using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HomeVisit
{
	public class Settings
	{
		public static string PAPER { get { return Application.streamingAssetsPath + "/ExamJson/"; } }

		public static string DialogueInfo { get { return Application.streamingAssetsPath + "/DialogueInfo.json"; } }

		public static string OldRandomScene = null;
		public static string RandomScene
		{
			get 
			{
				switch (DateTime.Now.Ticks % 3)
				{
					case 0:
						OldRandomScene = "ToBeDeveloped";
						break;
					case 1:
						OldRandomScene = "ToBeDeveloped";
						break;
					case 2:
						OldRandomScene = "ModelTest2";
						break;
				}
				return OldRandomScene;
			}
		}
	}
}
