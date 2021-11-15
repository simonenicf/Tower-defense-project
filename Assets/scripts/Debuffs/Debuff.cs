using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff
{

    protected Enemy target;
    private float duration;
    private float elapsed;
    
    public Debuff(Enemy target, float duration)
    {
        this.target = target;
        this.duration = duration;
    }

    public virtual void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed >= duration)
        {
            Remove();
        }
    }

    public virtual void Remove()
    {
        if (target != null)
        {
            target.RemoveDebuff(this);
        }
    }
}
