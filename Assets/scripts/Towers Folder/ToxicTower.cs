using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ToxicTower : Tower
{
    [SerializeField] private int splashDamage;
    [SerializeField] private float tickTime;
    [SerializeField] private PosionSplash splashPrefab;
    // getters
    public int SplashDamage => splashDamage;
    public float TickTime => tickTime;
    
    private void Start()
    {
        ElementType = Element.TOXIC;
        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(15, 2, 2.5f, 0.5f, 2, -0.1f, 2),
            new TowerUpgrade(10, 2, 2f, 0.5f, 0.5f, -0.1f, 1),
        };
    }

    public override Debuff GetDebuff()
    {
        return new AcidDebuff(splashDamage, tickTime, splashPrefab, DebuffDuration ,Target);
    }
}
