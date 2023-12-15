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
		//if(Vec)
		StopAllCoroutines();
		OnColliderEnterEvent?.Invoke(other);
		isCollision = true;
		mr.enabled = false;
		box.enabled = false;
	}

	public IEnumerator AreaHighlight()
	{
		mr.enabled = true;
		box.enabled = true;

		isCollision = false;
		while (!isCollision)
		{
			yield return null;
		}
	}
}
