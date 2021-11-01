using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    private Path_Follower _pathWalker;
    [SerializeField] private float speed;
    [SerializeField] private int health = 1;
    private bool isActive = true;
    [SerializeField] private bool isAlive;

    public bool IsAlive
    {
        get { return health > 0; }
    }
    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    
    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        health = 2;
        _pathWalker = FindObjectOfType<Path_Follower>();
        _pathWalker.speedOfWalker = speed;
    }
    

    public void TakeDamage(int damage)
    {
        if (isActiveAndEnabled)
        {
            health -= damage;
            if (health <= 0)
            {
                _pathWalker.Release();
                Debug.Log("I'm dead ");
                manager.Money += 1;
                isActive = false;
            }
        }
    }

}
