using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// NOTE! This class is in develop and will be changed!
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    public Weapon TestWeapon;
    public Equipment TestEquipment;

    private Weapon selectedWeapon;
    private Equipment selectedEquipment;

    public UnityEvent<Weapon> OnWeaponChanged;
    public UnityEvent<Equipment> OnEquipmentChanged;

    public void SelectWeapon(Weapon _newWeapon)
    {
        if (_newWeapon == null)
        {
            Debug.LogWarning("Weapon is null!");
            return;
        }

        selectedWeapon = _newWeapon;
        OnWeaponChanged.Invoke(_newWeapon);
    }

    public void SelectEquipment(Equipment _newEquipment)
    {
        if (_newEquipment == null)
        {
            Debug.LogWarning("Equipment is null!");
            return;
        }

        selectedEquipment = _newEquipment;
        OnEquipmentChanged.Invoke(_newEquipment);

    }

    private void Start()
    {
        SelectWeapon(TestWeapon);
        SelectEquipment(TestEquipment);
    }

    private void Update()
    {
        if (selectedWeapon != TestWeapon)
        {
            SelectWeapon(TestWeapon);
        }

        if (selectedEquipment != TestEquipment)
        {
            SelectEquipment(TestEquipment);
        }
    }
}
