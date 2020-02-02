using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public Sprite itemImage;
    public GameObject itemPrefab;
    public Status fixesStatus;

    public float repairTime;
}
