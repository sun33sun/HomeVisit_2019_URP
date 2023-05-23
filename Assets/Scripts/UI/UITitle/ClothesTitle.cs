using UnityEngine;
using UnityEngine.UI;


public class ClothesTitle : MonoBehaviour
{
	public enum ClothesKind { Man, Both, Woman }
	public ClothesKind state;
	public bool isRight;
	public Image imgMask;
	public Toggle togClothes;
}

