using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidDebuff : Debuff
{
    private float tickTime;
    private float timeSinceTick;
    private PosionSplash splashPrefab;
    private int splashDamage;
    
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
                Splash();
            }
        }
        
        base.Update();
    }

    private void Splash()
    {
        PosionSplash tmp = GameObject.Instantiate(splashPrefab, target.transform.position, Quaternion.identity);
        tmp.Damage = splashDamage;
        Physics.IgnoreCollision(target.GetComponent<Collider>(), tmp.GetComponent<Collider>());
    }

    public AcidDebuff(int splashDamage, float tickTime, PosionSplash splashPrefab, float duration ,Enemy target) : base(target, duration)
    {
        this.splashDamage = splashDamage;
        this.tickTime = tickTime;
        this.splashPrefab = splashPrefab;
    }
}
