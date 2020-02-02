using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image itemImage;
    public ItemData item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (item == null)
            itemImage.gameObject.SetActive(false);
        else
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = itemImage.sprite;
        }
    }
}
