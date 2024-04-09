using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerTargerFinder : MonoBehaviour
{
    [SerializeField]
    private Vector3Variable targerPosition;

    [SerializeField]
    private GameObject targetIndicator;

    [SerializeField]
    private LayerMask hostileTargetsMask;
    [SerializeField]
    private float searchForTargetRange;
    [SerializeField]
    private float searchInterval;
    private float searchIntervalTimer;


    private void SearchForTarger()
    {
        var potentialTargets = Physics.OverlapSphere(transform.position, searchForTargetRange, hostileTargetsMask);

        if (potentialTargets.Length <= 0)
        {
            targerPosition.Value = Vector3.zero;
            return;
        }

        float closestDistance = float.MaxValue;
        foreach (var potentialTarget in potentialTargets)
        {
            var distance = Vector3.Distance(potentialTarget.transform.position, transform.position);
            if (closestDistance > distance)
            {
                closestDistance = distance;
                targerPosition.Value = potentialTarget.transform.position;
            }
        }
    }

    private void Start()
    {
        searchIntervalTimer = searchInterval;
        targetIndicator.SetActive(false);
    }

    private void Update()
    {
        if (searchIntervalTimer <= 0)
        {
            SearchForTarger();
            searchIntervalTimer = searchInterval;
        }
        else
        {
            searchIntervalTimer -= Time.deltaTime;
        }

        if (targetIndicator != null)
        {
            if (targerPosition.Value != Vector3.zero)
            {
                targetIndicator.SetActive(true);
                targetIndicator.transform.position = targerPosition.Value;
            }
            else
            {
                targetIndicator.SetActive(false);
            }
        }
    }
}
