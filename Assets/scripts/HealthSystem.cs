using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* to do:
 add heal health function
 
 */
public class HealthSystem : MonoBehaviour
{
    private float _currentHealth;
    private float _startHealth;

    public float getCurrentHealth
    {
        get { return _currentHealth; }
    }

    public float starthealth
    {
        get => _startHealth;
        set => _startHealth = value;
    }

    private void Awake()
    {
        // on the moment the script gets called set currenthealth equal to starthealth
        _currentHealth = _startHealth;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        HandleTakeDamage();

        if (_currentHealth <= 0)
        {
            Death();
        }
    }
    
    // these functions can be overriden
    protected virtual void HandleTakeDamage()
    {
        print("ouch that hurts");
    }

    protected virtual void Death()
    {
        print("whelp, I'm dead");
    }
}
