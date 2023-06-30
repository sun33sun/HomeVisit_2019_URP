using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectBase.Anim
{
	public class AnimationManager:Singleton<AnimationManager>
	{
		public WaitUntil Play(Animation anim,string clipName)
		{
			anim.Play(clipName);
			return new WaitUntil(() => { return !anim.isPlaying; });
		}
	}
}
