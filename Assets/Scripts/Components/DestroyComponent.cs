using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DestroyComponent : MonoBehaviour
{
    [SerializeField]
    private float destroyDelay;

    [SerializeField]
    private UnityEvent onDestroy;

    public void Destroy()
    {
        onDestroy?.Invoke();
        Destroy(this.gameObject);
    }

    private void Start()
    {
        StartCoroutine(DelayedDestroyCoroutine());
    }

    IEnumerator DelayedDestroyCoroutine()
    {
        yield return new WaitForSeconds(destroyDelay);

        Destroy();
    }
}
