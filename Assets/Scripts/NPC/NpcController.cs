using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;

public class NpcController : MonoBehaviour
{
    public const int MAX_SEARCHING_TARGETS_COUNT = 16;

    public enum CombatState
    {
        Idle,
        Attacking,
        Routing
    }

    public CombatState CurrentCombatState { get; private set; } = CombatState.Idle;
    public Action<CombatState, CombatState> OnStateChanged;

    [SerializeField] private NpcMovement movement;
    [SerializeField] private CombatBehaviour combat;
    [SerializeField] private float searchTargetInterval = 0.1f;
    private float searchTargetTimer;

    [SerializeField] private LayerMask hostileTargetsMask;

    [Header("Combat State Params")]
    [SerializeField] private float searchForTargetRange;

    [SerializeField] private float attackRange;
    [SerializeField] private bool firstSightRouting = false;
    [SerializeField] private Vector2 routingRangeMinMax;
    [SerializeField] private Vector2 routingTimeMinMax;
    private float routingTimer = 0;

    [SerializeField]
    private Vector2 changeRoutingDirTimeMinMax;

    private float changeRoutingDirTimer = 0;

    [SerializeField]
    private float maxDistanceFromMainPos;

    [SerializeField]
    private float minDeltaDistanceFromMainPos;

    /// <summary>
    /// Dot product of facing direction and target direction
    /// </summary>
    [SerializeField]
    private float requiredAngleForAttack = 0.9f;

    private RoutingDirection routingDirection;

    private Transform target = null;

    private Vector3 mainPosition;
    private Vector3 lastPosition;

    private void ChangeCombatState(CombatState _newState)
    {
        OnStateChanged?.Invoke(CurrentCombatState, _newState);
        CurrentCombatState = _newState;
    }

    private void SearchForTarget()
    {
        // var potentialTargets = Physics.OverlapSphere(transform.position, searchForTargetRange, hostileTargetsMask);
        var foundColliders = new Collider[MAX_SEARCHING_TARGETS_COUNT];
        var size = Physics.OverlapSphereNonAlloc(transform.position, searchForTargetRange, foundColliders,
            hostileTargetsMask);

        if (size <= 0)
        {
            target = null;
            ChangeCombatState(CombatState.Idle);
            return;
        }

        var closestDistance = float.MaxValue;

        for (var i = 0; i < size; i++)
        {
            var potentialTarget = foundColliders[i];
            var distance = Vector3.Distance(potentialTarget.transform.position, transform.position);

            if (closestDistance > distance)
            {
                closestDistance = distance;
                target = potentialTarget.transform;
            }
        }
    }

    private void HandleCombatStance()
    {
        switch (CurrentCombatState)
        {
            case CombatState.Idle:
                IdleStateHandle();
                break;
            case CombatState.Attacking:
                AttackStateHandle();
                break;
            case CombatState.Routing:
                RoutingStateHandle();
                break;
            default:
                break;
        }
    }

    #region StanceLogic

    protected virtual void IdleStateHandle()
    {
        var distanceToMain = Vector3.Distance(transform.position, mainPosition);
        if (distanceToMain >= minDeltaDistanceFromMainPos)
        {
            movement.MoveToTarget(mainPosition);
        }
        else
        {
            movement.StopMovement();
        }

        if (target != null)
        {
            ChangeCombatState(firstSightRouting ? CombatState.Routing : CombatState.Attacking);
        }
    }

    protected virtual void AttackStateHandle()
    {
        var distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget <= attackRange)
        {
            Attacking();
        }
        else if (distanceToTarget <= searchForTargetRange)
        {
            Chasing();
        }
    }

    protected virtual void RoutingStateHandle()
    {
        var distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget < routingRangeMinMax.x)
        {
            Retreat();
        }
        else if (distanceToTarget >= routingRangeMinMax.x && distanceToTarget <= routingRangeMinMax.y)
        {
            Routing();
        }
        else if (distanceToTarget > routingRangeMinMax.y)
        {
            Chasing();
        }

        if (routingTimer <= 0)
        {
            ChangeCombatState(CombatState.Attacking);
        }
        else
        {
            routingTimer -= Time.deltaTime;
        }
    }

    protected virtual void Chasing()
    {
        //TODO: Advanced target chasing

        //movement.FaceTarget(target.position);
        //movement.MoveForward();

        movement.MoveToTarget(target.position);

        ////Cast ray to targer location
        //var ray = new Ray(transform.position, CalculateRotationToTarget());
        //if (Physics.Raycast(ray, out var rayInfo, attackRange))
        //{
        //    Routing();
        //}
        //else
        //{
        //    movement.FaceTarget(target.position);
        //    movement.MoveForward();
        //}
    }

    protected virtual void Attacking()
    {
        movement.StopMovement();
        movement.FaceTarget(target.position);


        if (GetDot() >= requiredAngleForAttack)
        {
            combat.Attack();
        }
    }

    protected virtual void Retreat()
    {
        var eulerToTarget = CalculateRotationToTarget();
        Quaternion rotationToTarget = Quaternion.Euler(eulerToTarget);

        var desiredRot = (transform.position - target.position).normalized;
        var dot = Vector3.Dot(transform.forward, eulerToTarget);
        if (dot > -0.8f)
        {
            movement.SetLookRotation(desiredRot);
        }

        movement.MoveForward();
    }

    protected virtual void Routing()
    {
        var rotToTarget = CalculateRotationToTarget();
        Vector3 desiredRotation;

        if (changeRoutingDirTimer <= 0)
        {
            changeRoutingDirTimer =
                UnityEngine.Random.Range(changeRoutingDirTimeMinMax.x, changeRoutingDirTimeMinMax.y);
            routingDirection = (UnityEngine.Random.value > 0.5f) ? RoutingDirection.Right : RoutingDirection.Left;
        }

        switch (routingDirection)
        {
            case RoutingDirection.Left:
                desiredRotation = new Vector3(-rotToTarget.z, rotToTarget.y, rotToTarget.x);
                break;
            case RoutingDirection.Right:
                desiredRotation = new Vector3(rotToTarget.z, rotToTarget.y, -rotToTarget.x);
                break;
            default:
                desiredRotation = Vector3.zero;
                break;
        }

        Debug.DrawRay(transform.position, desiredRotation, Color.red);

        movement.SetLookRotation(desiredRotation);
        movement.MoveForward();

        changeRoutingDirTimer -= Time.deltaTime;
    }

    #endregion


    protected Vector3 CalculateRotationToTarget()
    {
        return (target.position - transform.position).normalized;
    }

    protected float GetDot()
    {
        var rotationToTarget = target.position - transform.position;

        return Vector3.Dot(transform.forward, rotationToTarget.normalized);
    }

    private void StartRouting()
    {
        routingTimer = UnityEngine.Random.Range(routingTimeMinMax.x, routingTimeMinMax.y);

        routingDirection = (UnityEngine.Random.value > 0.5f) ? RoutingDirection.Right : RoutingDirection.Left;
        ChangeCombatState(CombatState.Routing);
    }

    protected void Start()
    {
        ChangeCombatState(CombatState.Idle);

        mainPosition = transform.position;
        searchTargetTimer = searchTargetInterval;

        combat.OnComboDone.AddListener(StartRouting);
    }

    protected void OnDestroy()
    {
        combat.OnComboDone.RemoveListener(StartRouting);
    }

    protected void Update()
    {
        if (searchTargetTimer <= 0)
        {
            SearchForTarget();
            searchTargetTimer = searchTargetInterval;
        }
        else
        {
            searchTargetTimer -= Time.deltaTime;
        }

        HandleCombatStance();
    }

    public enum RoutingDirection
    {
        Left,
        Right
    }
}