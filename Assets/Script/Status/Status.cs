using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class Status : ScriptableObject
{
    public string Name;
    public TextObject Value;
    public string Unit;
    public bool IsDisconnected;
    public StatusEvent onReturn = new StatusEvent();
}
