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
    private Weapon selectedWeapon;

    public UnityEvent<Weapon> OnWeaponChanged;

    public void SelectWeapon(Weapon _newWeapon)
    {
        if (_newWeapon == null)
        {
            Debug.LogWarning("Weapon is null!");
            return;
        }

        selectedWeapon = _newWeapon;
        OnWeaponChanged!.Invoke(_newWeapon);
    }

    private void Start()
    {
        SelectWeapon(TestWeapon);
    }

    private void Update()
    {
        if (selectedWeapon != TestWeapon)
        {
            SelectWeapon(TestWeapon);
        }
    }
}
