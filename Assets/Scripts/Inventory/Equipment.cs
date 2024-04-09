using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Items/Equipment")]
public class Equipment : Item
{
    public int HealthModifier;
    public int ArmorModifier;
    public int DamageModifier;
    public float SpeedModifier;

    public List<SkinnedMeshRenderer> skinnedMeshRenderers;
}
