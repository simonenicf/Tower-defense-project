using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyHealthComponent enemyHealthSys;
    private Path_Follower _pathWalker;
    [SerializeField] private float health;
    [SerializeField] private float speed;
    private void Start()
    {
        // enemyHealthSys = FindObjectOfType<EnemyHealthComponent>();
        _pathWalker = FindObjectOfType<Path_Follower>();
        
        enemyHealthSys.starthealth = health;
        _pathWalker.speedOfWalker = speed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            enemyHealthSys.TakeDamage(1);
        }
    }
}
