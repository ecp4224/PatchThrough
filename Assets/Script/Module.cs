using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Module : MonoBehaviour
{
    public InventoryData inventory;
    
    public ItemData item;
    
    public float pickupRange = 3f;
    public StringVariable actionText;

    private GameObject player;
    private bool didShow = false;

    public bool PlayerInRange
    {
        get { return (player.transform.position - transform.position).magnitude <= (pickupRange * 2f); }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange)
        {
            actionText.Value = "Press SPACE to pick up " + item.fixesStatus.Name;
            didShow = true;

            if (Input.GetKey(KeyCode.Space))
            {
                inventory.items.Add(item);
                Destroy(gameObject);
                actionText.Value = "";
            }
        }
        else if (didShow)
        {
            actionText.Value = "";
        }
    }
}
