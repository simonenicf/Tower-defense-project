using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;

public class Tower : MonoBehaviour
{
    [SerializeField] string projectileType;
    
    private Queue<Enemy> enemy = new Queue<Enemy>();
    private Enemy target;
    private bool canAttack = true;
    private float attackTimer;
    [SerializeField] float attackCooldown;
    [SerializeField] float projectileSpeed;
    [SerializeField] private int damage;

    // variable's that access other scripts
    [SerializeField] private EnemyHealthComponent enemyHealth;

    [SerializeField] private GameManager gameManager;
    
    // getters and setters
    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
    }

    public Enemy Target
    {
        get { return target; }
    }

    public int Damage 
    {
        get { return damage; }
    }
    
    public int Price { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy.Enqueue(other.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            target = null;
        }
    }

    private void Attack()
    {
        // if you can't attack timer counts up till its higher or equal to attackCooldown
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }
        // if there are still enemies pick the next enemy in queue as target
        if (target == null && enemy.Count > 0)
        {
            target = enemy.Dequeue();
        }
        // check if the tower has a target and if that target is active
        if (target != null && target.IsActive)
        {
            if (canAttack)
            {
                shoot();
                canAttack = false;
            }
        }
        else if (enemy.Count > 0)
        {
            target = enemy.Dequeue();
        }

        if (target != null && !target.IsAlive)
        {
            target = null;
        }
    }

    private void shoot()
    {
        projectile projectile = gameManager.Pool.GetObject(projectileType).GetComponent<projectile>();

        projectile.transform.position = transform.position;
        projectile.InitializeTower(this);
    }
    
}
