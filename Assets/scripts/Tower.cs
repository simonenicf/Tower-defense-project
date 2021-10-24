using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;

public class Tower : MonoBehaviour
{
    private Queue<Enemy> enemy = new Queue<Enemy>();
    private Enemy target;

    [SerializeField] private EnemyHealthComponent enemyHealth;
    // private int damage = 1;
    // private float radius;
    // private int cost;

    // Update is called once per frame
    void Update()
    {
        Attack();
        
        Debug.Log(target);
    }

    private void Attack()
    {
        if (target == null && enemy.Count > 0)
        {
            target = enemy.Dequeue();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hallo");
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
