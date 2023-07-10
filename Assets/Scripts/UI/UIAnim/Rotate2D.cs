using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rotate2D : MonoBehaviour
{
	public Vector3 maxAngle = new Vector3(0, 0, 360);
	RectTransform rect;

	private void Awake()
	{
		rect = transform as RectTransform;
		transform.DORotate(maxAngle, 2, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart);
	}

	private void OnEnable()
	{
		transform.DORestart();
	}

	private void OnDisable()
	{
		transform.DOPause();
	}
}
