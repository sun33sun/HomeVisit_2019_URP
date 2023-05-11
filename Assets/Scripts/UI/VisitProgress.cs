using UnityEngine;
using UnityEngine.UI;

namespace HomeVisit.UI
{
    public class VisitProgress : MonoBehaviour
    {
        public Image imgVisitProgress;
        public Sprite[] sprites;
        public int spriteIndex;

        public void InitState()
		{
            spriteIndex = 0;
            imgVisitProgress.sprite = sprites[0];
		}

        public void NextState()
		{
            if(spriteIndex > sprites.Length - 2)
			{
                Debug.LogError("SpriteIndex Out Of Bounds : " + spriteIndex);
                return;
			}
            spriteIndex++;
            imgVisitProgress.sprite = sprites[spriteIndex];
		}
    }
}

