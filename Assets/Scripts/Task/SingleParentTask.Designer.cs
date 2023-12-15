using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeVisit.Task
{
	public partial class SingleParentTask
	{
		[SerializeField] SelectionSO selectionObservingEnvironment;
		[SerializeField] SelectionSO selectionExplainClass;
		[SerializeField] SelectionSO selectionFindOutSituation;
		[SerializeField] SelectionSO selectionClarifyExpectations;
		[Header("家庭情况")]
		[SerializeField] SelectionSO selectionRice;
		[SerializeField] SelectionSO selectionNormal;
		[SerializeField] SelectionSO selectionPoor;
	}
}

