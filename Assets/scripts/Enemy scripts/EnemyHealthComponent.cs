using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthComponent : HealthSystem
{


    protected override void HandleTakeDamage()
    {
        print("enemy took damage");
    }

    protected override void Death()
    {
        print("enemy is dead");
        Destroy(gameObject);
    }
}
