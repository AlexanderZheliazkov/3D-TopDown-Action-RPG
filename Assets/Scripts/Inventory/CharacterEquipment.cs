using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEquipment : MonoBehaviour
{
    [SerializeField]
    private Transform rightHandPlaceholder;
    [SerializeField]
    private Transform leftHandPlaceholder;

    private GameObject rightHandItem;
    private GameObject leftHandItem;

    public void ChangeWeapon(Weapon _newWeapon)
    {
        if (_newWeapon == null)
            return;

        if (_newWeapon.RightHandPrefab != null)
        {
            if (rightHandItem != null)
            {
                Destroy(rightHandItem);
            }
            rightHandItem = Instantiate(_newWeapon.RightHandPrefab, rightHandPlaceholder);
        }

        if (_newWeapon.LeftHandPrefab != null)
        {
            if (leftHandItem != null)
            {
                Destroy(leftHandItem);
            }
            leftHandItem = Instantiate(_newWeapon.LeftHandPrefab, leftHandPlaceholder);
        }
    }
}
