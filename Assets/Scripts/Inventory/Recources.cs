using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recources Container", menuName = "Inventory/Recources")]
public class Recources : ScriptableObject
{
    public Currency Gold;
    public Currency WeaponParts;
    public Currency ArmorParts;
}
