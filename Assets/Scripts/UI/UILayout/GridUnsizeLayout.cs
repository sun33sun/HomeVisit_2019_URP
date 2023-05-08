using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HomeVisit_URP.UI
{
	[ExecuteAlways]
	public class GridUnsizeLayout : LayoutGroup
	{
		public override void CalculateLayoutInputVertical()
		{
			throw new System.NotImplementedException();
		}

		public override void SetLayoutHorizontal()
		{
			throw new System.NotImplementedException();
		}

		public override void SetLayoutVertical()
		{
			throw new System.NotImplementedException();
		}

		protected void CalcPos()
		{
			Rect rect = rectTransform.rect;
			//已知(0,0)点为锚点位置
			//去除pading后的大小，如果pading过大会出现问题
			Vector2 size = rect.size - new Vector2(padding.horizontal, padding.vertical);
			//Vector2 leftUpPos = rect.size 8(Vector2.up - rectTransform.pivot) - new Vector2(-padding.left, padding.top);
		}
	}
}

