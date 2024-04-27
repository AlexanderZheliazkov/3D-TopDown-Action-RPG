using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Items/Equipment")]
public class Armor : Equipment
{
    public List<SkinnedMeshRenderer> skinnedMeshRenderers;
}