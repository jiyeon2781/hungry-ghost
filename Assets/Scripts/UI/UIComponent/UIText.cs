using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIText : UIBase
{
    private TMP_Text textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TMP_Text>();
    }

    protected override void Init()
    {
        
    }

    public void SetText(string str)
    {
        textMeshPro.text = str;
    }

    public void DeactivateText()
    {
        gameObject.SetActive(false);
    }
}
