using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private Enemy target;
    private Tower parent;
    [SerializeField] private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        StalkTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (target.gameObject == other.gameObject)
            {
                target.TakeDamage(parent.Damage);
                gameManager.Pool.ReleaseObject(gameObject);
            }
        }
    }

    public void InitializeTower(Tower parent)
    {
        this.target = parent.Target;
        this.parent = parent;
    }
    private void StalkTarget()
    {
        if (target != null && target.IsActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position,
                Time.deltaTime * parent.ProjectileSpeed);
        }
        else if (!target.IsActive)
        {
            gameManager.Pool.ReleaseObject(gameObject);
        }
    }
}
