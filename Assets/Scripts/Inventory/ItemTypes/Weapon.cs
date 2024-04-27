using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Items/Weapon")]
public class Weapon : Equipment
{
    public MovementAnimationSet MovementAnimations;

    public List<Attack> Attacks;

    public GameObject RightHandPrefab;
    public GameObject LeftHandPrefab;
}