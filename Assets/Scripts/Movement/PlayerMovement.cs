using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    [SerializeField]
    private float minDotRotationForMoving = 0.8f;

    public float SmoothRotationTime = 0.2f;
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
            Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, SmoothRotationTime);
    }

    public override void FaceTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - agent.transform.position).normalized;
        Quaternion LookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, LookRotation, 1 - SmoothRotationTime);
    }

    public override void SetLookRotation(Vector3 _targerRotation)
    {
        Quaternion LookRotation = Quaternion.LookRotation(new Vector3(_targerRotation.x, 0, _targerRotation.z));
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, LookRotation, 1 - SmoothRotationTime);
    }

    private void Update()
    {
        if (moveInput.Value.magnitude >= inputDelay && isMovementEnabled)
        {
            Rotation();

            var targerForward = new Vector3(moveInput.Value.y, 0, moveInput.Value.x);
            Debug.DrawRay(transform.position, targerForward, Color.blue);
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            if (Vector3.Dot(transform.forward, targerForward.normalized) >= minDotRotationForMoving)
            {
                Movement();
            }
        }

        playerPosition.Value = transform.position;
    }
}
