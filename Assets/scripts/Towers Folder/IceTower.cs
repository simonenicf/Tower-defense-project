using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Tower
{
    [SerializeField] private float slowingFactor;

    private void Start()
    {
        ElementType = Element.ICE;
        
        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(8, 1, 3, 1f, 2, 10),
            new TowerUpgrade(22, 2, 2.75f, 1f, 2, 20),
        };
    }

    public override Debuff GetDebuff()
    {
        return new IceDebuff(slowingFactor, DebuffDuration, Target);
    }
}
