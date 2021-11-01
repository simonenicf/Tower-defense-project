using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* to do:
 add heal health function
 
 */
public class HealthSystem : MonoBehaviour
{
    private int _currentHealth;
    private int _startHealth;

    public int CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    public int starthealth
    {
        get => _startHealth;
        set => _startHealth = value;
    }
    

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
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

    public virtual void Death()
    {
        print("whelp, I'm dead");
    }
}
