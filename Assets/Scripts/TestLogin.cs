using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestLogin : MonoBehaviour
{
    public Image imgMask;
    public Button btnConfirm;
    void Start()
    {
        btnConfirm.onClick.AddListener(() => { imgMask.gameObject.SetActive(true); });   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
