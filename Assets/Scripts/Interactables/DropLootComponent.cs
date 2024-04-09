using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropLootComponent : Interactable
{
    public UnityEvent onInteract;

    public override bool Interact()
    {
        onInteract.Invoke();
        return true;
    }
}