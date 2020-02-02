using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeBaseStatusText : BindableMonoBehavior
{
    public Status status;

    [BindComponent()]
    private Text nameText;

    public Text statusText;

    public Color missingText = Color.red;
    
    void Update()
    {
        nameText.text = status.Name;
        
        if (status.IsDisconnected)
        {
            statusText.color = missingText;
            statusText.text = "DISCONNECTED";
        }
        else
        {
            statusText.text = status.Value.ToString() + " " + status.Unit;
            statusText.color = Color.white;
        }
    }
}
