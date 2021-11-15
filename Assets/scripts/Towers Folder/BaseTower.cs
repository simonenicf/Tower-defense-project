using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : Tower
{
    [SerializeField] private float critDamage;

    [SerializeField] private float tickTime;
    // Start is called before the first frame update
    void Start()
    {
        ElementType = Element.NONE;
        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(3, 1, 2.5f, 0, 0.5f, 0, 1),
            new TowerUpgrade(10, 2, 2f, 0f, 0.5f, 0f, 1),
        };
    }

    public override Debuff GetDebuff()
    {
        return new BaseDebuff(critDamage, tickTime, DebuffDuration, Target);
    }
}
