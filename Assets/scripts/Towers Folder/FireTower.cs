using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : Tower
{
    [SerializeField] private float tickTime;
    [SerializeField] private float tickDamage;
    // getters and setters
    public float TickTime => tickTime;
    public float TickDamage => tickDamage;
    
    private void Start()
    {
        ElementType = Element.FIRE;
        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(5, 2, 3, 0.5f, 5, -0.1f, 1),
            new TowerUpgrade(15, 3, 2.5f, .5f, 5, 0.1f, 1),
        };
    }

    public override Debuff GetDebuff()
    {
        return new FireDebuff(tickDamage, tickTime, DebuffDuration, Target);
    }
}
