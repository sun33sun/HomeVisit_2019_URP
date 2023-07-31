using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HighlightPlus;
using ProjectBase;
using UnityEngine;

namespace HomeVisit.Effect
{
	public class EffectManager : SingletonMono<EffectManager>
	{
		public List<HighlightEffect> effectList;

		public HighlightEffect AddEffectAndTarget(GameObject newObj)
		{
			HighlightEffect newHighlight = newObj.AddComponent<HighlightEffect>();
			effectList.Add(newHighlight);
			newHighlight.targetFX = true;
			newHighlight.outlineColor = Color.red;

			newObj.AddComponent<HighlightTrigger>();
			return newHighlight;
		}

		public HighlightEffect AddEffectImmediately(GameObject newObj, bool haveInnerGlow = true)
		{
			HighlightEffect newHighlight = newObj.AddComponent<HighlightEffect>();
			effectList.Add(newHighlight);
			newHighlight.outlineColor = Color.red;
			newHighlight.highlighted = true;
			if (haveInnerGlow)
			{
				newHighlight.innerGlow = 0.5f;
				newHighlight.innerGlowWidth = 2;
				newHighlight.innerGlowColor = Color.red;
			}
			return newHighlight;
		}

		public void Remove(GameObject oldObj)
		{
			HighlightEffect oldHighlight = oldObj.GetComponent<HighlightEffect>();
			if (effectList.Contains(oldHighlight))
				effectList.Remove(oldHighlight);
			Destroy(oldHighlight);
			Destroy(oldObj.GetComponent<HighlightTrigger>());
		}
	}
}
