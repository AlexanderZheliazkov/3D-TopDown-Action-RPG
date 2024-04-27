using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TouchInteractable : Interactable
{
    [SerializeField] private Collider triggerCollider;
    [SerializeField] private LayerMask interactionMask;

    public override bool Interact()
    {
        base.onInteraction.Invoke();
        return true;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if ((interactionMask.value & (1 << _other.gameObject.layer)) != 0)
        {
            Interact();
        }
    }

    private void Start()
    {
        triggerCollider = GetComponent<Collider>();
    }
}