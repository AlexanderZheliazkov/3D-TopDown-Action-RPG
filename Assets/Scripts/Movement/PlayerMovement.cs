using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MovementBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private Vector2Variable moveInput;
    [SerializeField]
    private float inputDelay = 0.1f;

    public float smoothRotationTime = 0.2f;
    float turnSmoothVelocity;

    [SerializeField]
    private Vector3Variable playerPosition;
    [SerializeField]
    private QuaternionVariable cameraRotation;

    private bool isMovementEnabled = true;

    public override void DisableMovement()
    {
        isMovementEnabled = false;
    }

    public override void EnableMovement()
    {
        isMovementEnabled = true;
    }

    private void Movement()
    {
        agent.velocity = transform.TransformDirection(Vector3.forward * agent.speed);
    }

    private void Rotation()
    {
        float targetRotation = Mathf.Atan2(moveInput.Value.y, moveInput.Value.x) * Mathf.Rad2Deg + cameraRotation.Value.eulerAngles.y;
        agent.transform.eulerAngles =
            Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, smoothRotationTime);
    }

    private void Update()
    {
        if (moveInput.Value.magnitude >= inputDelay && isMovementEnabled)
        {
            Movement();
            Rotation();
        }

        playerPosition.Value = transform.position;
    }
}
