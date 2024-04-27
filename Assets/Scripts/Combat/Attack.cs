using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New attack", menuName = "ScriptableObjects/Attack")]
public class Attack
{
    public float AttackProjectileSpawnDelay
    {
        get { return attackProjectileSpawnDelay; }
        private set { }
    }

    public AnimationClip AttackAnimation;
    public float AnimationSpeedModifier = 1;
    public LayerMask TargetMask;

    [SerializeField]
    private List<AttackProjectile> projectiles;

    [Min(0)] public float damageModifier;
    public float AttackDurationTime;
    public float FreezeMovementTime;
    public float AttackComboResetTime;
    //[Min(0)] public int energyCost;

    [Min(0)]
    [SerializeField]
    private float attackProjectileSpawnDelay = 0;

    public void InitiateAttack(Vector3 _startPosition, Quaternion _startRotattion, CharacterStats _attackerStats)
    {
        if (_attackerStats == null) return;
        if (this.projectiles == null) return;

        foreach (var projectile in this.projectiles)
        {
            var spawnedProjectile = AttackProjectile.SpawnAttackProjectile(
                projectile,
                new AttackProjectile.AttackProjectileConfig()
                {
                    AttackDamageModifier = damageModifier,
                    TargetLayerMask = TargetMask,
                    AttackerStats = _attackerStats,
                    AttackProjectileSpawnDelay = attackProjectileSpawnDelay
                },
                _startPosition,
                _startRotattion);
        }
    }
}