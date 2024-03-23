using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCombat : CombatBehaviour
{
    [SerializeField]
    private List<Attack> attacksSet;

    protected override void Start()
    {
        base.Start();
        this.attacks = attacksSet;
    }
}
