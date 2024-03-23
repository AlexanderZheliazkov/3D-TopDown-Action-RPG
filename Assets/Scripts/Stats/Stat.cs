using System;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private int baseValue;

    public Stat()
    {
        baseValue = 0;
        //modifiers = new Dictionary<Item, int>();
    }

    public Stat(int baseValue)
    {
        this.baseValue = baseValue;
        //modifiers = new Dictionary<Item, int>();
    }

    public int GetValue()
    {
        int finalValue = baseValue;
        //foreach (KeyValuePair<Item, int> mod in modifiers)
        //{
        //   finalValue += mod.Value;
        //}
        return finalValue;
    }

    public void SetBaseValue(int _baseValue)
    {
        baseValue = _baseValue;
    }
}
