using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillFromVariable : BindableMonoBehavior
{

    [BindComponent()]
    private Image image;

    public FloatVariable fill;
    
    void Update()
    {
        image.fillAmount = fill.Value;
    }
}
