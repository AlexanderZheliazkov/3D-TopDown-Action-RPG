using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Container", menuName = "Inventory/EquipmentContainer")]
public class EquipmentContainer : ScriptableObject
{
    [SerializeField] private List<Equipment> Items;

    public Action<Equipment> OnItemAdded;
    public Action<Equipment> OnItemRemoved;

    public List<Equipment> GetItems() => Items;

    public void AddItem(Equipment _equipmentToAdd)
    {
        Items.Add(_equipmentToAdd);
        OnItemAdded.Invoke(_equipmentToAdd);
    }

    public bool TryRemoveItem(Equipment _equipmentToRemove)
    {
        if (Items.Remove(_equipmentToRemove))
        {
            OnItemRemoved.Invoke(_equipmentToRemove);
            return true;
        }

        return false;
    }
}