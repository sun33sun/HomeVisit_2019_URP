using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeVisit.Task
{
	public partial class MultipleChildrenTask
	{
		[SerializeField] SelectionSO selectionFindOutSituation;
		[SerializeField] SelectionSO selectionClarifyExpectations;
		[SerializeField] SelectionSO selectionRelieveAnxiety;

		[Header("家庭情况")]
		[SerializeField] SelectionSO selectionRice;
		[SerializeField] SelectionSO selectionNormal;
		[SerializeField] SelectionSO selectionPoor;
	}
}

