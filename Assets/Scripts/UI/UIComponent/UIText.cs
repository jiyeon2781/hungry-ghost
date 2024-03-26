using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIText : UIBase
{
    private TextMesh textMesh;
    protected override void Init()
    {
        textMesh = GetComponent<TextMesh>();
    }

    public void SetText(string str)
    {
        textMesh.text = str;
    }

    public void DeactivateText()
    {
        gameObject.SetActive(false);
    }
}
