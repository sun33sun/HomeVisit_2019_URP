using System;
using System.Collections;
using UnityEngine;

namespace ProjectBase
{
	public class WaitForDistance : IEnumerator
	{
		Transform t1;
		Transform t2;

		public WaitForDistance(Transform t1,Transform t2)
		{
			this.t1 = t1;
			this.t2 = t2;
		}
		public object Current
		{
			get
			{
				return null;
			}
		}
		public bool MoveNext()
		{
			return Vector3.Distance(t1.position,t2.position) > 0.1f;
		}
		public void Reset()
		{
		}
	}
}
