using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MovementBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Vector2Variable moveInput;
    [SerializeField] private float inputDelay = 0.1f;
    [SerializeField] private float minDotRotationForMoving = 0.8f;

    [SerializeField] private Vector3Variable playerPosition;
    [SerializeField] private QuaternionVariable cameraRotation;

    public float SmoothRotationTime = 0.2f;
    private float turnSmoothVelocity;


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
        var targetRotation = Mathf.Atan2(moveInput.Value.y, moveInput.Value.x) * Mathf.Rad2Deg +
                             cameraRotation.Value.eulerAngles.y;
        agent.transform.eulerAngles =
            Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity,
                SmoothRotationTime);
    }

    public override void FaceTarget(Vector3 _targetPosition)
    {
        Vector3 direction = (_targetPosition - agent.transform.position).normalized;
        SetLookRotation(direction);
    }

    public override void SetLookRotation(Vector3 _targetRotation)
    {
        var lookRotation = Quaternion.LookRotation(new Vector3(_targetRotation.x, 0, _targetRotation.z));
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, 1 - SmoothRotationTime);
    }

    private void Update()
    {
        if (moveInput.Value.magnitude >= inputDelay && isMovementEnabled)
        {
            Rotation();

            var targetForward = new Vector3(moveInput.Value.y, 0, moveInput.Value.x);
            if (Vector3.Dot(transform.forward, targetForward.normalized) >= minDotRotationForMoving)
            {
                Movement();
            }
        }

        playerPosition.Value = transform.position;
    }
}