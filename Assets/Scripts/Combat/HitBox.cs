using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AttackProjectile;

[RequireComponent(typeof(Rigidbody))]
public class HitBox : MonoBehaviour
{
    [HideInInspector]
    public LayerMask TargetLayerMask;
    [HideInInspector]
    public CharacterStats AttackerStats;
    [HideInInspector]
    public float AttackDamageModifier;

    public void SetConfiguration(CharacterStats _attackerStats, float _damageModifier, LayerMask _targerLayerMask)
    {
        AttackerStats = _attackerStats;
        AttackDamageModifier = _damageModifier;
        TargetLayerMask = _targerLayerMask;
    }

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
