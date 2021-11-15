using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDebuff : Debuff
{
    private float critDamage;
    private float timeSinceTick;
    private float tickTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        if (target != null)
        {
            timeSinceTick += Time.deltaTime;

            if (timeSinceTick >= tickTime)
            {
                timeSinceTick = 0;
                target.TakeDamage(critDamage, Element.NONE);
            }
        }
    }

    public BaseDebuff(float critDamage, float tickTime,float duration, Enemy target) : base(target, duration)
    {
        this.critDamage = critDamage;
        this.tickTime = tickTime;
    }
}
