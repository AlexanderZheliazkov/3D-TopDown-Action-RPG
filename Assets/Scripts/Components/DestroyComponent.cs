using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyComponent : MonoBehaviour
{
    [SerializeField]
    private float destroyDelay;

    private void Start()
    {
        StartCoroutine(DelayedDestroyCoroutine());
    }

    IEnumerator DelayedDestroyCoroutine()
    {
        yield return new WaitForSeconds(destroyDelay);

        Destroy(this.gameObject);
    }
}
