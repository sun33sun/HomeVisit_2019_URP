using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeVisit.UI;
using QFramework;
using UnityEngine;
using UnityEngine.Networking;

namespace HomeVisit
{
	public class ExamManager : MonoSingleton<ExamManager>
	{
		[SerializeField] GameObject singleTitlePrefab;
		[SerializeField] GameObject scoreReportPrefab;
		[SerializeField] GameObject multipleTitlePrefab;
		[SerializeField] GameObject outlineTitlePrefab;
		[SerializeField] GameObject togOutlinePrefab;
		[SerializeField] GameObject judgeTitlePrefab;

		public SingleTitle CreateSingleTitle(SingleTitleData data)
		{
			GameObject gameObj = Instantiate(singleTitlePrefab);
			gameObj.name = singleTitlePrefab.name;
			SingleTitle singleTitle = gameObj.GetComponent<SingleTitle>();
			singleTitle.InitData(data);
			return singleTitle;
		}

		public MultipleTitle CreateMultipleTitle(MultipleTitleData data)
		{
			GameObject gameObj = Instantiate(multipleTitlePrefab);
			gameObj.name = multipleTitlePrefab.name;
			MultipleTitle multipleTitle = gameObj.GetComponent<MultipleTitle>();
			multipleTitle.InitData(data);
			return multipleTitle;
		}

		public OutlineTitle CreateOutlineTitle(OutlineTitleData data)
		{
			GameObject gameObj = Instantiate(outlineTitlePrefab);
			gameObj.name = outlineTitlePrefab.name;
			OutlineTitle outlineTitle = gameObj.GetComponent<OutlineTitle>();
			outlineTitle.InitData(data);
			return outlineTitle;
		}

		public TogOutline CreateTogOutline()
		{
			GameObject gameObj = Instantiate(togOutlinePrefab);
			gameObj.name = togOutlinePrefab.name;
			return gameObj.GetComponent<TogOutline>();
		}

		public ScoreReport CreateScoreReport(ScoreReportData data)
		{
			GameObject gameObj = Instantiate(scoreReportPrefab);
			gameObj.name = scoreReportPrefab.name;
			ScoreReport scoreReport = gameObj.GetComponent<ScoreReport>();
			scoreReport.Init(data);
			return scoreReport;
		}

		public JudgeTitle CreateJudgeTitle(JudgeTitleData data)
		{
			GameObject gameObj = Instantiate(judgeTitlePrefab);
			gameObj.name = judgeTitlePrefab.name;
			JudgeTitle judgeTitle = gameObj.GetComponent<JudgeTitle>();
			judgeTitle.InitData(data);
			return judgeTitle;
		}
	}
}
