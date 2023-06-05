using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeVisit.UI;
using QFramework;
using UnityEngine;

namespace HomeVisit
{
	public class ExamManager : MonoSingleton<ExamManager>
	{
		[SerializeField] GameObject singlePrefab;
		[SerializeField] List<ITitle> titles = new List<ITitle>();

		public GameObject CreateSingleTitle(SingleTitleData data)
		{
			GameObject gameObj = Instantiate(singlePrefab);
			gameObj.name = singlePrefab.name;
			SingleTitle singleTitle = gameObj.GetComponent<SingleTitle>();
			titles.Add(singleTitle);
			singleTitle.Init(data);
			gameObj.transform.localScale = Vector3.one;
			return gameObj;
		}
	}
}
