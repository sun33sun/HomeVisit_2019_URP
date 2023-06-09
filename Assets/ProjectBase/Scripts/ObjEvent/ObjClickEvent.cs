namespace ProjectBase
{
	public class ObjClickEvent : BaseEvent
	{
		private void OnMouseUpAsButton()
		{
			SelfDestroy();
		}
		
		public override void SelfDestroy(bool isInvoke = true)
		{
			if (isDestroy)
				return;
			isDestroy = true;
			if(isInvoke)
				OnClick?.Invoke();
			OnClick = null;

			HighlightPlus.HighlightTrigger highlightTrigger = GetComponent<HighlightPlus.HighlightTrigger>();
			if (highlightTrigger != null)
				Destroy(highlightTrigger);
			HighlightPlus.HighlightEffect hightlightEffect = GetComponent<HighlightPlus.HighlightEffect>();
			if (hightlightEffect != null)
				Destroy(hightlightEffect);
			Destroy(this);
		}
	}
}
