using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private EquipmentSlotUI equipmentSlotUIPrefab;
    [SerializeField] private EquipmentContainer equipmentContainer;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private EquipedItemsSetSO currentEquipment;
    [SerializeField] private ToggleGroup togglesGroup;

    private Dictionary<Equipment, EquipmentSlotUI> displayedEquipment = new Dictionary<Equipment, EquipmentSlotUI>();

    public void DisplayEquipment(Equipment _equipmentToDisplay)
    {
        if (displayedEquipment.TryGetValue(_equipmentToDisplay, out var value))
        {
            Destroy(value.gameObject);
        }

        displayedEquipment.Add(_equipmentToDisplay, SpawnEquipmentSlotUiObjet(_equipmentToDisplay));
    }

    public bool TryRemoveEquipment(Equipment _equipmentToRemove)
    {
        if (_equipmentToRemove == null) return false;
        if (!displayedEquipment.ContainsKey(_equipmentToRemove)) return false;

        RemoveEquipment(_equipmentToRemove);
        return true;
    }

    public void RemoveEquipment(Equipment _equipmentToRemove)
    {
        Destroy(displayedEquipment[_equipmentToRemove].gameObject);
        displayedEquipment.Remove((_equipmentToRemove));
    }

    public void UpdateAllUIEquipmentSlots()
    {
        ClearAllSlots();

        foreach (var equipment in equipmentContainer.GetItems())
        {
            DisplayEquipment(equipment);
        }
    }

    public void ClearAllSlots()
    {
        if (displayedEquipment != null)
        {
            foreach (var equipment in displayedEquipment)
            {
                Destroy(equipment.Value.gameObject);
            }

            displayedEquipment.Clear();
        }
        else
        {
            displayedEquipment = new Dictionary<Equipment, EquipmentSlotUI>();
        }
    }

    private EquipmentSlotUI SpawnEquipmentSlotUiObjet(Equipment _equipment)
    {
        var equipmentUiSlot = Instantiate(equipmentSlotUIPrefab, gridLayoutGroup.transform);
        equipmentUiSlot.UpdateItemInfo(_equipment);
        var toggle = equipmentUiSlot.SelectToggle;
        toggle.group = togglesGroup;
        toggle.onValueChanged.AddListener((bool _value) =>
        {
            if (_value)
            {
                currentEquipment.EquipItem(equipmentUiSlot.CurrentEquipment);
            }
        });

        return equipmentUiSlot;
    }

    private void Start()
    {
        displayedEquipment = new Dictionary<Equipment, EquipmentSlotUI>();
        UpdateAllUIEquipmentSlots();

        equipmentContainer.OnItemAdded += DisplayEquipment;
        equipmentContainer.OnItemRemoved += RemoveEquipment;
    }

    private void OnDestroy()
    {
        equipmentContainer.OnItemAdded -= DisplayEquipment;
        equipmentContainer.OnItemRemoved -= RemoveEquipment;
    }
}