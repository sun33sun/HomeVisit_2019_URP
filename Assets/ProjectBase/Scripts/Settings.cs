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
				switch (DateTime.UtcNow.Ticks % 3)
				{
					case 0:
						OldRandomScene = "Developed";
						break;
					case 1:
						OldRandomScene = "Developing";
						break;
					case 2:
						OldRandomScene = "ToBeDeveloped";
						break;
				}
				return OldRandomScene;
			}
		}
		public const string UI = "Resources/UIPrefab/";
	}
}
