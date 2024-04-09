using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class RangedProjectile : AttackProjectile
{
    [SerializeField]
    protected UnityEvent onProjectileSpawned;
    [SerializeField]
    protected UnityEvent onHitboxEnabled;
    [SerializeField]
    protected UnityEvent onSenderDestroy;

    IEnumerator SpawnProjectileCoroutine()
    {
        yield return new WaitForSeconds(AttackConfig.AttackProjectileSpawnDelay);

        onProjectileSpawned.Invoke();
    }

    private void OnTriggerEnter(Collider _other)
    {
        if ((AttackConfig.TargetLayerMask.value & (1 << _other.gameObject.layer)) != 0)
        {
            onHitboxEnabled.Invoke();
        }
    }
    protected override void SenderDestroyed()
    {
        onSenderDestroy.Invoke();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(SpawnProjectileCoroutine());
    }

}
