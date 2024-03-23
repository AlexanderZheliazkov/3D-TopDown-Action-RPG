//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public enum CombatState
//{
//    Idle,
//    Chasing,
//    Attacking,
//    Returning
//}

//[RequireComponent(typeof(NavMeshAgent))]
//public class EnemyController : MonoBehaviour
//{
//    public CombatState CurrentCombatState { get; private set; }

//    [SerializeField]
//    private NpcMovement movement;
//    [SerializeField]
//    private LayerMask hostileTargetsMask;
//    [SerializeField]
//    private float searchForTargetRange;
//    [SerializeField]
//    private float attackRange;
//    [SerializeField]
//    private float maxDistanceFromMainPos;
//    [SerializeField]
//    private float minDeltaDistanceFromMainPos;

//    [SerializeField]
//    private float waitBeforeBack;

//    private Transform target;

//    private Vector3 mainPosition;
//    private Vector3 lastPosition;
//    private float waitTimer;

//    private NavMeshAgent agent;


//    void Start()
//    {
//        agent = GetComponent<NavMeshAgent>();
//        mainPosition = transform.position;
//        waitTimer = waitBeforeBack;
//    }

//    void Update()
//    {
//        SerachForTarger();
//        SetCombatStance();
//        HandleCombatStance();
//    }

//    private void SerachForTarger()
//    {
//        var potentialTargets = Physics.OverlapSphere(transform.position, searchForTargetRange, hostileTargetsMask);

//        if (potentialTargets.Length <= 0)
//        {
//            target = null;
//            return;
//        }

//        foreach (var potentialTarget in potentialTargets)
//        {
//            float distance = float.MaxValue;
//            if (distance > Vector3.Distance(potentialTarget.transform.position, transform.position))
//            {
//                target = potentialTarget.transform;
//            }
//        }
//    }

//    private void SetCombatStance()
//    {
//        if (target != null)
//        {
//            if (Vector3.Distance(target.position, mainPosition) >= (maxDistanceFromMainPos + attackRange))
//                CurrentCombatState = CombatState.Returning;

//            var distanceToTarget = Vector3.Distance(transform.position, target.position);
//            if (distanceToTarget <= attackRange) CurrentCombatState = CombatState.Attacking;
//            if (distanceToTarget >= attackRange && distanceToTarget <= searchForTargetRange) CurrentCombatState = CombatState.Chasing;
//        }
//        else
//        {
//            CurrentCombatState = CombatState.Returning;
//        }
//    }

//    private void HandleCombatStance()
//    {
//        switch (CurrentCombatState)
//        {
//            case CombatState.Idle:
//                Idle();
//                break;
//            case CombatState.Chasing:
//                Chasing();
//                break;
//            case CombatState.Attacking:
//                Attacking();
//                break;
//            case CombatState.Returning:
//                Returning();
//                break;
//            default:
//                break;
//        }
//    }

//    #region StanceLogic
//    protected virtual void Idle()
//    {
//        var distanceToMain = Vector3.Distance(transform.position, mainPosition);
//        if (distanceToMain >= minDeltaDistanceFromMainPos)
//        {
//            movement.MoveToTarget(mainPosition);
//        }
//        else
//        {
//            movement.StopMovement();
//        }
//    }

//    protected virtual void Chasing()
//    {
//        movement.MoveToTarget(target.position);
//    }

//    protected virtual void Attacking()
//    {
//        movement.StopMovement();
//        //attacking
//    }

//    protected virtual void Returning()
//    {
//        movement.MoveToTarget(mainPosition);
//    }
//    #endregion




//    //private void HandleMovementStance(MovementState movementState)
//    //{
//    //    if (target == null)
//    //    {
//    //        if (lastPosition == transform.position)
//    //        {
//    //            if (transform.position != mainPosition)
//    //            {
//    //                //StopAndWait();

//    //                BackToMainPosition();
//    //            }
//    //        }
//    //    }
//    //    else if (target != null)
//    //    {
//    //        if (waitTimer != waitBeforeBack)
//    //            waitTimer = waitBeforeBack;

//    //        var distance = Vector3.Distance(target.position, transform.position);

//    //        if (distance <= attackRange)
//    //        {
//    //            agent.isStopped = true;
//    //            Debug.Log($"{transform.name} is attacking {target.name}");
//    //            //attack logic
//    //        }

//    //        if (!(Vector3.Distance(target.position, mainPosition) >= (maxDistanceFromMainPos + attackRange)))
//    //        {
//    //            if (distance > attackRange)
//    //            {
//    //                agent.isStopped = false;
//    //                agent.SetDestination(target.position);
//    //            }
//    //        }
//    //        else
//    //        {
//    //            BackToMainPosition();
//    //        }

//    //    }

//    //    lastPosition = transform.position;
//    //}



//    //private void BackToMainPosition()
//    //{
//    //    if (agent.destination != mainPosition)
//    //    {
//    //        agent.isStopped = false;
//    //        agent.SetDestination(mainPosition);
//    //    }
//    //}

//    //private void StopAndWait()
//    //{
//    //    if (!agent.isStopped)
//    //        agent.isStopped = true;
//    //}
//}
