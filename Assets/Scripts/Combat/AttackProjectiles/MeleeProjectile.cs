using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeProjectile : AttackProjectile
{
    [SerializeField]
    protected UnityEvent onHitboxSpawned;

    IEnumerator SpawnHitboxCoroutine()
    {
        yield return new WaitForSeconds(base.AttackConfig.AttackProjectileSpawnDelay);

        base.SpawnHitbox(hitBoxPrefab, transform.position, transform.rotation);
        onHitboxSpawned.Invoke();
    }

    private void Start()
    {
        StartCoroutine(SpawnHitboxCoroutine());
    }
}
