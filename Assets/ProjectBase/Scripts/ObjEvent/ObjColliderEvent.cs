using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjColliderEvent : MonoBehaviour
{
	MeshRenderer mr;
	bool isCollision = false;
	public Action<Collider> OnColliderEnterEvent;
	BoxCollider box;

	private void Start()
	{
		mr = GetComponent<MeshRenderer>();
		box = GetComponent<BoxCollider>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.tag.Equals("Teacher"))
			return;
		StopAllCoroutines();
		OnColliderEnterEvent?.Invoke(other);
		isCollision = true;
		mr.enabled = false;
		box.enabled = false;
	}

	public WaitUntil AreaHighlight()
	{
		mr.enabled = true;
		box.enabled = true;

		isCollision = false;
		return new WaitUntil(CheckCollision);
	}

	bool CheckCollision()
	{
		return isCollision;
	}
}
