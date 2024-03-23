using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class CombatBehaviour : MonoBehaviour
{
    [Header("Requred Components")]
    [SerializeField]
    protected CharacterStats stats;
    [SerializeField]
    public MovementBehaviour Movement;
    [Space(10)]
    public UnityEvent<Attack> OnAttackEvent;
    public UnityEvent<Attack, Attack> OnAttackEnd;
    public UnityEvent OnComboDone;

    protected List<Attack> attacks;
    protected int currentAttackIndex = 0;

    protected bool isAttacking = false;

    private Coroutine handleCharacterAttackStateCoroutine;
    private Coroutine handleCharacterMovementCoroutine;
    private Coroutine handleAttackComboCoroutine;

    public virtual void Attack()
    {
        if (attacks == null) return;
        if (isAttacking) return;

        var curAttack = attacks[currentAttackIndex];

        //handle movement state
        if (handleCharacterMovementCoroutine != null)
        {
            StopCoroutine(handleCharacterMovementCoroutine);
        }
        handleCharacterMovementCoroutine = StartCoroutine(HandleCharacterMovement(curAttack));

        //handle attacking state
        if (handleCharacterAttackStateCoroutine != null)
        {
            StopCoroutine(handleCharacterAttackStateCoroutine);
        }
        handleCharacterAttackStateCoroutine = StartCoroutine(HandleCharacterAttackState(curAttack));

        //handle attack combo 
        if (handleAttackComboCoroutine != null)
        {
            StopCoroutine(handleAttackComboCoroutine);
        }
        handleAttackComboCoroutine = StartCoroutine(HandleAttackComboCoroutine(curAttack));

    }

    protected virtual IEnumerator HandleAttackComboCoroutine(Attack _attack)
    {
        yield return new WaitForSeconds(_attack.AttackComboResetTime);
        ResetCombo();
    }

    protected virtual IEnumerator HandleCharacterAttackState(Attack _attack)
    {
        isAttacking = true;

        _attack.InitiateAttack(transform.position, transform.rotation, stats);
        OnAttackEvent.Invoke(_attack);

        yield return new WaitForSeconds(_attack.AttackDurationTime);

        //update attack index
        currentAttackIndex++;
        if (currentAttackIndex >= attacks.Count)
        {
            ResetCombo();
        }
        OnAttackEnd.Invoke(_attack, attacks[currentAttackIndex]);

        isAttacking = false;
    }

    protected virtual IEnumerator HandleCharacterMovement(Attack _attack)
    {
        Movement.DisableMovement();
        yield return new WaitForSeconds(_attack.FreezeMovementTime);
        //yield return new WaitForEndOfFrame();
        Movement.EnableMovement();
    }

    protected void ResetCombo()
    {
        OnComboDone.Invoke();
        currentAttackIndex = 0;

        //stop if combo coroutine is still running
        if (handleAttackComboCoroutine != null)
        {
            StopCoroutine(handleAttackComboCoroutine);
        }
    }

    protected virtual void Start()
    {

    }
}
