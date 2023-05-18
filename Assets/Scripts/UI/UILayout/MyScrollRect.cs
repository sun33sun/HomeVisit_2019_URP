using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HomeVisit.UI
{
	public class MyScrollRect : ScrollRect
	{
		public enum MouseButton { Left, Right, Middle }

		public MouseButton mouseButton = MouseButton.Left;
		public override void OnBeginDrag(PointerEventData eventData)
		{
			CheckInput(eventData);
			base.OnBeginDrag(eventData);
		}

		void CheckInput(PointerEventData eventData)
		{
			switch (mouseButton)
			{
				case MouseButton.Right:
					if (eventData.button == PointerEventData.InputButton.Right)
						eventData.button = PointerEventData.InputButton.Left;
					else
						eventData.button = PointerEventData.InputButton.Right;
					break;
				case MouseButton.Middle:
					if (eventData.button == PointerEventData.InputButton.Middle)
						eventData.button = PointerEventData.InputButton.Left;
					else
						eventData.button = PointerEventData.InputButton.Right;
					break;
			}
		}

		public override void OnDrag(PointerEventData eventData)
		{
			CheckInput(eventData);
			base.OnDrag(eventData);
		}

		public override void OnEndDrag(PointerEventData eventData)
		{
			CheckInput(eventData);
			base.OnEndDrag(eventData);
		}
	}
}


