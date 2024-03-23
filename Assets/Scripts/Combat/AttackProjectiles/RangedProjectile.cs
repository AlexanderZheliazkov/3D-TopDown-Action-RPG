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
    private GameObject projectileObject;

    IEnumerator SpawnProjectileCoroutine()
    {
        yield return new WaitForSeconds(AttackConfig.AttackProjectileSpawnDelay);

        projectileObject.gameObject.SetActive(true);
        onProjectileSpawned.Invoke();
    }

    private void OnTriggerEnter(Collider _other)
    {
        if ((AttackConfig.TargetLayerMask.value & (1 << _other.gameObject.layer)) != 0)
        {
            SpawnHitbox(hitBoxPrefab, projectileObject.transform.position, projectileObject.transform.rotation);
            onHitboxEnabled.Invoke();
            Destroy(projectileObject.gameObject);
        }
    }

    private void Start()
    {
        projectileObject.gameObject.SetActive(false);
        StartCoroutine(SpawnProjectileCoroutine());
    }
}
