using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
			if (eventData.clickCount == 2)
			{
				isDoubleClick = true;
				OnDoubleClick?.Invoke();
			}
		}
	}
}


