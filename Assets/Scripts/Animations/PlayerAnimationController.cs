using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : AnimationController
{
    public void OnWeaponChangedHandle(Weapon _newWeapon)
    {
        ChangeMovementAnimationSet(_newWeapon.MovementAnimations);
    }
}
