using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjColliderEvent : MonoBehaviour
{
	MeshRenderer mr;
	bool isCollision = false;
	public Action<Collider> OnColliderEnterEvent;
	WaitForSeconds wait = new WaitForSeconds(1);

	private void Awake()
	{
		mr = GetComponent<MeshRenderer>();
		mr.material.color = Color.red;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.tag.Equals("Teacher"))
			return;
		StopAllCoroutines();
		OnColliderEnterEvent?.Invoke(other);
		isCollision = true;
		gameObject.SetActive(false);
	}

	public WaitUntil AreaHighlight()
	{
		gameObject.SetActive(true);
		isCollision = false;
		StartCoroutine(DoAreaHighlight());
		return new WaitUntil(CheckCollision);
	}

	bool CheckCollision()
	{
		return isCollision;
	}

	IEnumerator DoAreaHighlight()
	{
		while (true)
		{
			mr.enabled = !mr.enabled;
			yield return wait;
		}
	}
}
