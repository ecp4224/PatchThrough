using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : BindableMonoBehavior
{
    [BindComponentsInChildren()]
    public List<Slot> slots;

    public InventoryData inventory;
    
    // Update is called once per frame
    void Update()
    {
        for (var i = 0; i < slots.Count; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].item = inventory.items[i];
            }
            else
            {
                slots[i].item = null;
            }
        }
    }
}
