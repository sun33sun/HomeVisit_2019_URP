using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HomeVisit
{
	public class ProjectSetting
	{
		public static string PAPER_FORM { get { return Application.streamingAssetsPath + "/Paper_Form.json"; } }

		public static string DialogueInfo { get { return Application.streamingAssetsPath + "/DialogueInfo.json"; } }

		public static bool BanGongShi = false;

		public static string OldRandomScene = null;
		public static string RandomScene
		{
			get 
			{
				switch (UnityEngine.Random.Range(0,3))
				{
					case 0:
						OldRandomScene = "JuJia";
						break;
					case 1:
						OldRandomScene = "JuJia";
						break;
					case 2:
						OldRandomScene = "JiaTing";
						break;
				}
				return OldRandomScene;
			}
		}
	}
}
