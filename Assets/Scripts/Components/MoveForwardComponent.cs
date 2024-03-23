using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardComponent : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float moveSpeed;

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * moveSpeed;
    }
}
