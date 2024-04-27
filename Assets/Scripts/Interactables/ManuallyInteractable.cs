using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManuallyInteractable : Interactable
{
    public override bool Interact()
    {
        base.onInteraction.Invoke();
        return true;
    }
}