using HomeVisit.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HomeVisit.Task
{
	[CreateAssetMenu(fileName = "SelectionSO", menuName = "New SelectionSO", order = 1)]
	[System.Serializable]
	public class SelectionSO : ScriptableObject
	{
		public NPCIndex npcIndex;
		public List<OptionData> options = new List<OptionData>();
	}

	[System.Serializable]
	public class OptionData
	{
		public string str;
		public AudioClip clip;
	}
}

