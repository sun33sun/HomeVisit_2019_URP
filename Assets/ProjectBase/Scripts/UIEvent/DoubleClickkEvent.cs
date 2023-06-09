using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ProjectBase
{
	public class DoubleClickkEvent : BaseEvent, IPointerClickHandler
	{
		public UnityAction OnDoubleClick;

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
				return;
			if (eventData.clickCount == 2)
				OnDoubleClick?.Invoke();
		}
	}
}


