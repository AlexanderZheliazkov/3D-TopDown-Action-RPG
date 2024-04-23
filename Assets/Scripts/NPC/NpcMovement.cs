using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcMovement : MovementBehaviour
{
    [SerializeField]
    protected NavMeshAgent agent;
    [SerializeField]
    private float rotationLerp;

    protected bool isFreezed = false;
    protected Vector3 mainPosition;

    protected MovementMode movementMode;

    public override void EnableMovement()
    {
        agent.ResetPath();
        agent.isStopped = false;
        isFreezed = false;

    }

    public override void DisableMovement()
    {
        isFreezed = true;
        agent.isStopped = true;
    }

    public void StopMovement()
    {
        agent.ResetPath();
        agent.isStopped = true;
    }

    public void MoveToTarget(Vector3 targetPosition)
    {
        if (isFreezed)
        {
            return;
        }

        if (movementMode != MovementMode.TargetDriven)
        {
            agent.ResetPath();
            movementMode = MovementMode.TargetDriven;
        }

        if (Vector3.Distance(agent.transform.position, targetPosition) <= 0)
        {
            StopMovement();
        }
        else
        {
            if (agent.isStopped)
            {
                agent.isStopped = false;
            }
            agent.SetDestination(targetPosition);
        }
    }

    public void MoveForward()
    {
        if (isFreezed) return;

        if (movementMode != MovementMode.ForwardDriven)
        {
            agent.ResetPath();
            agent.isStopped = false;
            movementMode = MovementMode.ForwardDriven;
        }

        agent.velocity = transform.TransformDirection(Vector3.forward * agent.speed);
    }

    public void FreezMovementFor(float time)
    {
        StartCoroutine(FreezeMovementCoroutine(time));
    }

    IEnumerator FreezeMovementCoroutine(float time)
    {
        StopMovement();
        isFreezed = true;
        yield return new WaitForSeconds(time);
        isFreezed = false;
    }

    public override void FaceTarget(Vector3 _targetPosition)
    {
        if (isFreezed) return;

        Vector3 direction = (_targetPosition - agent.transform.position).normalized;
        Quaternion LookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, LookRotation, rotationLerp);
    }

    public override void SetLookRotation(Vector3 _targetRotation)
    {
        if (isFreezed) return;

        Quaternion LookRotation = Quaternion.LookRotation(new Vector3(_targetRotation.x, 0, _targetRotation.z));
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, LookRotation, rotationLerp);
    }

    void Start()
    {

    }

    public enum MovementMode
    {
        TargetDriven = 1,
        ForwardDriven = 2,
    }
}
