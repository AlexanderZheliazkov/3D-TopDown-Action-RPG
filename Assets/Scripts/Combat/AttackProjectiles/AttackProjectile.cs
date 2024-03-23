using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AttackProjectile : MonoBehaviour
{
    [HideInInspector]
    public AttackProjectileConfig AttackConfig;

    [SerializeField]
    protected HitBox hitBoxPrefab;

    public static AttackProjectile SpawnAttackProjectile(AttackProjectile _projectile, AttackProjectileConfig _config, Vector3 _startPosition, Quaternion _startRotation)
    {
        if (_projectile == null) return null;
        var proj = Instantiate(_projectile, _startPosition, _startRotation);
        proj.AttackConfig.TargetLayerMask = _config.TargetLayerMask;
        proj.AttackConfig.AttackerStats = _config.AttackerStats;
        proj.AttackConfig.AttackDamageModifier = _config.AttackDamageModifier;
        proj.AttackConfig.AttackProjectileSpawnDelay = _config.AttackProjectileSpawnDelay;

        return proj;
    }

    protected HitBox SpawnHitbox(HitBox _hitBox, Vector3 _position, Quaternion _rotation)
    {
        if (_hitBox == null) return null;

        var hitbox = Instantiate(_hitBox, _position, _rotation);
        hitbox.AttackerStats = AttackConfig.AttackerStats;
        hitbox.AttackDamageModifier = AttackConfig.AttackDamageModifier;
        hitbox.TargetLayerMask = AttackConfig.TargetLayerMask;

        return hitbox;
    }

    [Serializable]
    public struct AttackProjectileConfig
    {
        public LayerMask TargetLayerMask;
        public CharacterStats AttackerStats;
        public float AttackDamageModifier;
        public float AttackProjectileSpawnDelay;
    }
}
