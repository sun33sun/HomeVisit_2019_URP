using UnityEngine;
using QFramework;
using UnityEngine.UI;

namespace HomeVisit.Draw
{
    public class DrawManager : MonoSingleton<DrawManager>
    {
        [SerializeField] Button btnEraser;
        [SerializeField] Button btnPaint;
        [SerializeField] Button btnClear;
        [SerializeField] Texture drawTex;
        [Header("Runtime Editor")]
        [SerializeField] Color color = Color.red;
        [SerializeField] [Range(0.5f, 2f)] float width;

        void Start()
        {

        }
    }
}


