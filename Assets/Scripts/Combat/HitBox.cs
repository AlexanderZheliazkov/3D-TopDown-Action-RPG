using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HitBox : MonoBehaviour
{
    [HideInInspector]
    public LayerMask TargetLayerMask;
    [HideInInspector]
    public CharacterStats AttackerStats;
    [HideInInspector]
    public float AttackDamageModifier;

    protected virtual void ApplyDamage(Collider _target)
    {
        var victimStats = _target.GetComponentInChildren<CharacterStats>();
        if (victimStats != null)
        {
            AttackerStats.DealDamage(victimStats, AttackDamageModifier);
        }
    }

    protected virtual void OnTriggerEnter(Collider _other)
    {
        if ((TargetLayerMask.value & (1 << _other.gameObject.layer)) != 0)
        {
            ApplyDamage(_other);
        }
    }
}
