using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Variable<T> : TextObject
{
    public T Value;
    
    public override string ToString()
    {
        return Value.ToString();
    }
}