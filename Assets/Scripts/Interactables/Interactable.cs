using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected UnityEvent onInteraction;

    public abstract bool Interact();
}