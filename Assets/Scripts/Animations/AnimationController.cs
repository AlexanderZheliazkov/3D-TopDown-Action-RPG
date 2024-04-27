using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationController : MonoBehaviour
{
    private const float SMOOTH_TIME = .1f;
    private const float MIN_SPEED_TO_BE_MOVING = 2f;

    private const string BASE_IDLE_ANIMATION_CLIP_NAME = "Base_Idle";
    private const string BASE_RUN_ANIMATION_CLIP_NAME = "Base_Run";
    private const string BASE_ATTACK1_ANIMATION_CLIP_NAME = "Base_Attack_1";
    private const string BASE_ATTACK2_ANIMATION_CLIP_NAME = "Base_Attack_2";

    private bool useBonusAttackState = false;

    [SerializeField]
    private NavMeshAgent navMeshAgent;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private MovementAnimationSet baseMovementAnimSet;

    private MovementAnimationSet currentMovementAnimationSet;
    private AnimatorOverrideController overrideController;

    public void DoAttackAnimation(Attack _attack)
    {
        if (_attack == null) return;
        if (_attack.AttackAnimation == null)
        {
            Debug.LogError("MissingAttackAnimation!");
            return;
        }

        anim.SetBool("UseBonusAttackState", !useBonusAttackState);
        useBonusAttackState = anim.GetBool("UseBonusAttackState");

        anim.SetFloat("AttackSpeedMultiplayer", _attack.AnimationSpeedModifier);

        if (!useBonusAttackState)
        {
            overrideController[BASE_ATTACK1_ANIMATION_CLIP_NAME] = _attack.AttackAnimation;
        }
        else
        {
            overrideController[BASE_ATTACK2_ANIMATION_CLIP_NAME] = _attack.AttackAnimation;
        }

        anim.SetTrigger("AttackTrigger");
    }

    public void ChangeMovementAnimationSet(MovementAnimationSet _animationSet)
    {
        if (_animationSet == null)
        {
            currentMovementAnimationSet = baseMovementAnimSet;
        }
        else
        {
            currentMovementAnimationSet = _animationSet;
        }

        overrideController[BASE_IDLE_ANIMATION_CLIP_NAME] = currentMovementAnimationSet.Idle;
        overrideController[BASE_RUN_ANIMATION_CLIP_NAME] = currentMovementAnimationSet.Run;
    }

    public void SetIsAttacking(bool _isAttacking)
    {
        anim.SetBool("IsAttacking", _isAttacking);
    }

    protected virtual void Awake()
    {
        overrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = overrideController;

        overrideController[BASE_IDLE_ANIMATION_CLIP_NAME] = baseMovementAnimSet.Idle;
        overrideController[BASE_RUN_ANIMATION_CLIP_NAME] = baseMovementAnimSet.Run;
    }

    protected virtual void Update()
    {
        if (navMeshAgent != null)
        {
            float curSpeed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
            anim.SetBool("IsMoving", curSpeed >= MIN_SPEED_TO_BE_MOVING);
            anim.SetFloat("SpeedPercent", curSpeed, SMOOTH_TIME, Time.deltaTime);
        }
    }
}