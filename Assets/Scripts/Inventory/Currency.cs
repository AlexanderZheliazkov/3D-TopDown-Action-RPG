using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Currency
{
    public int Amount { get { return amount; } }

    private int amount;

    public void AddAmount(int _addedAmount)
    {
        if (_addedAmount < 0)
        {
            Debug.Log($"Cannot add negative amount!");
            return;
        }

        this.amount += _addedAmount;
    }

    private bool TryRemoveAmount(int _removeAmount)
    {
        if (_removeAmount < 0)
        {
            Debug.Log($"Cannot remove negative amount!");
            return false;
        }

        if (this.amount - _removeAmount < 0)
        {
            return false;
        }
        else
        {
            this.amount -= _removeAmount;
            return true;
        }

    }
}
