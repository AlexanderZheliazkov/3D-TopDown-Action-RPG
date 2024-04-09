using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeProjectile : AttackProjectile
{
    [SerializeField]
    protected UnityEvent onHitboxSpawned;
    [SerializeField]
    protected UnityEvent onSenderDestroyed;

    IEnumerator SpawnHitboxCoroutine()
    {
        yield return new WaitForSeconds(base.AttackConfig.AttackProjectileSpawnDelay);

        onHitboxSpawned.Invoke();
    }

    protected override void SenderDestroyed()
    {
        onSenderDestroyed.Invoke();
        Destroy(this.gameObject);
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(SpawnHitboxCoroutine());
    }

}
