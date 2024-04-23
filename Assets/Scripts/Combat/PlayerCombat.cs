using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerCombat : CombatBehaviour
{
    [SerializeField] private Vector3Variable targetPosition;
    [SerializeField] private float minLookRotationDot = 0.99f;

    private Coroutine attackCoroutine = null;

    public void OnWeaponChangedHandle(Weapon _newWeapon)
    {
        attacks = _newWeapon.Attacks;
    }

    public override void Attack()
    {
        if (attackCoroutine == null && !base.isAttacking)
        {
            attackCoroutine = StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        yield return StartCoroutine(RotateTowardTargetCoroutine());

        base.Attack();
        attackCoroutine = null;
    }

    private IEnumerator RotateTowardTargetCoroutine()
    {
        if (targetPosition && targetPosition.Value != Vector3.zero)
        {
            base.Movement.DisableMovement();

            //wait until facing desired position
            yield return new WaitUntil(() =>
            {
                if (targetPosition.Value == Vector3.zero) return true;

                var desiredRotation = targetPosition.Value - transform.position;
                base.Movement.SetLookRotation(desiredRotation.normalized);

                return Vector3.Dot(transform.forward, desiredRotation.normalized) >= minLookRotationDot;
            });
        }
    }
}