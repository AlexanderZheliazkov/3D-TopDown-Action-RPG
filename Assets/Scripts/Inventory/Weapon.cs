using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Items/Weapon")]
public class Weapon : Item
{
    public int AttackBonus;
    public int DefenceBonus;
    public int HealthBonus;

    public MovementAnimationSet MovementAnimations;

    public List<Attack> Attacks;

    public GameObject RightHandPrefab;
    public GameObject LeftHandPrefab;
}
