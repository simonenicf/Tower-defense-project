using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDebuff : Debuff
{
    private float slowingFactor;
    private bool applied;
    


    public override void Update()
    {
        if (target != null)
        {
            if (!applied)
            {
                applied = true;
                target.Speed -= (target.MaxSpeed * slowingFactor) / 100;
            }
        }
    }

    public IceDebuff(float slowingFactor, float duration ,Enemy target) : base(target, duration)
    {
        this.slowingFactor = slowingFactor;
    }

    public override void Remove()
    {
        target.Speed = target.MaxSpeed;
        base.Remove();
    }
}
