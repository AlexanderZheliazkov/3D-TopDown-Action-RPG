using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "New Equipment Set", menuName = "Inventory/Equipment set")]
public class EquipedItemsSetSO : ScriptableObject
{
    [HideInInspector] public UnityEvent<Weapon> OnWeaponChanged = new UnityEvent<Weapon>();
    [HideInInspector] public UnityEvent<Armor> OnArmorChanged = new UnityEvent<Armor>();

    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Armor currnetArmor;

    public Weapon GetWeapon() => currentWeapon;
    public Armor GetArmor() => currnetArmor;

    /// <summary>
    /// Handle internally the type of equipment
    /// </summary>
    /// <param name="_newEquipment"></param>
    public void EquipItem(Equipment _newEquipment)
    {
        if (_newEquipment is Weapon)
        {
            SetWeapon(_newEquipment as Weapon);
        }
        else if (_newEquipment is Armor)
        {
            SetArmor(_newEquipment as Armor);
        }
    }

    public void SetWeapon(Weapon _newWeapon)
    {
        currentWeapon = _newWeapon;
        OnWeaponChanged.Invoke(_newWeapon);

        Debug.Log($"SetWeapon: {_newWeapon}");
    }

    public void SetArmor(Armor _newArmor)
    {
        currnetArmor = _newArmor;
        OnArmorChanged.Invoke(_newArmor);

        Debug.Log($"SetArmor: {_newArmor}");
    }

    private void OnValidate()
    {
        if (currentWeapon != null)
        {
            SetWeapon(currentWeapon);
        }

        if (currnetArmor != null)
        {
            SetArmor(currnetArmor);
        }
    }
}