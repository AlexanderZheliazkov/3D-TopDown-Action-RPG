using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSO<T> : ScriptableObject
{
    public Action<T> Event;
}