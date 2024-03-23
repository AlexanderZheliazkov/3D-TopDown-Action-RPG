using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : CombatBehaviour
{
    public void OnWeaponChangedHandle(Weapon _newWeapon)
    {
        attacks = _newWeapon.Attacks;
    }
}