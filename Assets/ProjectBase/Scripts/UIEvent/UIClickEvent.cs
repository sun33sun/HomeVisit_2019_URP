using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ProjectBase
{
	public class UIClickEvent : BaseEvent, IPointerClickHandler
	{
		public void OnPointerClick(PointerEventData eventData)
		{
			OnClick?.Invoke();
			UIOutline uIOutline = GetComponent<UIOutline>();
			if (uIOutline != null)
				Destroy(uIOutline);
			OnClick = null;
			Destroy(this);
		}
	}
}
