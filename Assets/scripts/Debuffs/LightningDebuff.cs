using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningDebuff : Debuff
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public LightningDebuff(Enemy target, float duration) : base(target,duration )
    {
        if (target != null)
        {
            target.Speed = 0;
        }
    }

    public override void Remove()
    {
        if (target != null)
        {
            target.Speed = target.MaxSpeed;
            base.Remove();
        }
        
    }
}
