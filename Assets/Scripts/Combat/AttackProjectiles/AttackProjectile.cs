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
    protected HitBox hitbox;

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

    [Serializable]
    public struct AttackProjectileConfig
    {
        public LayerMask TargetLayerMask;
        public CharacterStats AttackerStats;
        public float AttackDamageModifier;
        public float AttackProjectileSpawnDelay;
    }

    protected abstract void SenderDestroyed();

    protected virtual void Start()
    {
        hitbox.SetConfiguration(AttackConfig.AttackerStats, AttackConfig.AttackDamageModifier, AttackConfig.TargetLayerMask);
        AttackConfig.AttackerStats.OnCharacterDied.AddListener(SenderDestroyed);
    }
}
