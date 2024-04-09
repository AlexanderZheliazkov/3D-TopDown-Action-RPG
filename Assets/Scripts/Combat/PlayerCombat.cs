using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;

public class PlayerCombat : CombatBehaviour
{
    [SerializeField]
    private Vector3Variable targerPosition;
    [SerializeField]
    private float minLookRotationDot = 0.99f;

    private Coroutine attackCoroutine = null;

    public void OnWeaponChangedHandle(Weapon _newWeapon)
    {
        attacks = _newWeapon.Attacks;
    }

    public override void Attack()
    {
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(AttackCoroutine());
        }
    }

    protected IEnumerator AttackCoroutine()
    {
        yield return StartCoroutine(RotateTowardTargerCoroutine());

        base.Attack();
        attackCoroutine = null;
    }

    private IEnumerator RotateTowardTargerCoroutine()
    {
        if (targerPosition && targerPosition.Value != Vector3.zero)
        {
            base.Movement.DisableMovement();

            //wait until facing desired position
            yield return new WaitUntil(() =>
            {
                Vector3 desiredRotation = targerPosition.Value - transform.position;
                base.Movement.SetLookRotation(desiredRotation.normalized);

                if (Vector3.Dot(transform.forward, desiredRotation.normalized) >= minLookRotationDot)
                    return true;
                else
                    return false;
            });
        }
    }
}