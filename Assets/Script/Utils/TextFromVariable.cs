using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFromVariable : BindableMonoBehavior
{
    [BindComponent()]
    private Text text;

    public StringVariable variable;
    
    void Update()
    {
        text.text = variable.Value;
    }
}
