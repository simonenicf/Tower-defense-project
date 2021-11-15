using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormTower : Tower
{
    
    private void Start()
    {
        ElementType = Element.STORM;
        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(25, 1, 4.5f, 0.1f, 2f),
            new TowerUpgrade(40, 1, 4.0f, 0.1f, 2f),
        };
    }

    public override Debuff GetDebuff()
    {
        return new LightningDebuff(Target, DebuffDuration);
    }
}
