using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int Health = 100;
    public Image healthbar;

    private bool wasFlipped;
    
    public bool IsFlipped
    {
        get
        {
            var dot = Vector3.Dot(transform.up, Vector3.down);

            return dot > -0.4f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = Health / 100f;

        if (IsFlipped && !wasFlipped)
        {
            Health -= 15;
            wasFlipped = true;
        } 
        else if (!IsFlipped)
        {
            wasFlipped = false;
        }
    }
}
