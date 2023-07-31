using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProjectBase
{
	public class DoubleClickEvent : BaseEvent, IPointerClickHandler
	{
		public UnityAction OnDoubleClick;
		public bool isDoubleClick = false;

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
				return;
				isDoubleClick = true;
			OnDoubleClick?.Invoke();
		}
	}
}


