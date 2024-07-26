using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour
{
    public Stat MaxHealth;
    public float CurrentHealth { get; private set; }
    public Stat Defence;
    public Stat Damage;
    public Stat DamageAmplitudePercent;
    public Stat HealthRegen;
    public Stat LifeSteal;

    public UnityEvent<int> OnDamageTaken;
    public UnityEvent<float> OnHealthPercentChanged;
    public UnityEvent OnCharacterDied;


    /// <summary>
    /// Returns the damage dealt
    /// </summary>
    /// <param name="_victimStats"></param>
    /// <param name="_damageModifier"></param>
    /// <returns></returns>
    public virtual int DealDamage(CharacterStats _victimStats, float _damageModifier = 1)
    {
        float dmgAplitudeValue = UnityEngine.Random.Range(1 - (float)DamageAmplitudePercent.GetValue() / 100, 1 + (float)DamageAmplitudePercent.GetValue() / 100);
        int damageAmount = Mathf.RoundToInt(Damage.GetValue() * _damageModifier * dmgAplitudeValue);

        Heal(damageAmount * LifeSteal.GetValue());

        return _victimStats.TakeDamage(damageAmount);
    }

    /// <summary>
    /// Returns the damage taken
    /// </summary>
    /// <param name="_amount"></param>
    /// <returns></returns>
    public virtual int TakeDamage(int _amount)
    {
        _amount -= (int)(_amount * ((float)Defence.GetValue() / 100));
        CurrentHealth -= _amount;
        OnDamageTaken?.Invoke(_amount);
        OnHealthPercentChanged?.Invoke((float)CurrentHealth / (float)MaxHealth.GetValue());

        Debug.Log($"{this.gameObject.name} took {_amount} damage!");
        if (CurrentHealth <= 0)
        {
            OnCharacterDied?.Invoke();
        }
        return _amount;
    }

    public void Heal(float _amount)
    {
        CurrentHealth += _amount;
        if (CurrentHealth >= MaxHealth.GetValue())
        {
            CurrentHealth = MaxHealth.GetValue();
        }
        OnHealthPercentChanged?.Invoke((float)CurrentHealth / (float)MaxHealth.GetValue());
    }

    private void Start()
    {
        CurrentHealth = MaxHealth.GetValue();
    }

    private void FixedUpdate()
    {
        //Only for fun. TO BE CHANGED!
        if (CurrentHealth <= MaxHealth.GetValue())
        {
            CurrentHealth += HealthRegen.GetValue() * Time.deltaTime;
            OnHealthPercentChanged?.Invoke((float)CurrentHealth / (float)MaxHealth.GetValue());
        }
    }
}
