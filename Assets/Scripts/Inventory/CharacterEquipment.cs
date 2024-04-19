using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEquipment : MonoBehaviour
{
    [SerializeField] private EquipedItemsSetSO currentEquipment;

    private Weapon selectedWeapon;
    private Armor selectedArmor;

    public UnityEvent<Weapon> OnWeaponChanged;
    public UnityEvent<Armor> OnEquipmentChanged;

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

    public void SelectArmor(Armor _newEquipment)
    {
        if (_newEquipment == null)
        {
            Debug.LogWarning("Equipment is null!");
            return;
        }

        selectedArmor = _newEquipment;
        OnEquipmentChanged.Invoke(_newEquipment);
    }

    private void Start()
    {
        SelectWeapon(currentEquipment.GetWeapon());
        SelectArmor(currentEquipment.GetArmor());

        currentEquipment.OnWeaponChanged.AddListener(SelectWeapon);
        currentEquipment.OnArmorChanged.AddListener(SelectArmor);
    }

    private void OnDestroy()
    {
        currentEquipment.OnWeaponChanged.RemoveListener(SelectWeapon);
        currentEquipment.OnArmorChanged.RemoveListener(SelectArmor);
    }
}