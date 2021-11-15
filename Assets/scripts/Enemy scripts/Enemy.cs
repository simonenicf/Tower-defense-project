using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    private Path_Follower _pathWalker;
    // enemy Stats
    [SerializeField] private float speed;
    [SerializeField] private float health = 1;
    // check if enemy is active or alive
    private bool isActive = true;
    [SerializeField] private bool isAlive;
    // Element Type of my enemy
    [SerializeField] private Element elementType;

    public Element ElementType => elementType;

    // list of debuffs
    private List<Debuff> debuffs = new List<Debuff>();
    private List<Debuff> debuffsToRemove = new List<Debuff>();
    private List<Debuff> newDebuffs = new List<Debuff>();
    public bool IsAlive
    {
        get { return health > 0; }
    }
    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    public float Health
    {
        get { return health; }
        set { health = value; }
    }
    
    public float Speed
    {
        get => _pathWalker.SpeedOfWalker;
        set => _pathWalker.SpeedOfWalker = value;
    }
    public float MaxSpeed { get; set; }

    public void ClearDebuffs()
    {
        debuffs.Clear();
    }
    
    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        health = 4;
        MaxSpeed = speed;
        _pathWalker = FindObjectOfType<Path_Follower>();
        _pathWalker.SpeedOfWalker = speed;
    }

    private void Update()
    {
        HandleDebuffs();
    }


    public void TakeDamage(float damage, Element dmgSource)
    {
        if (isActiveAndEnabled)
        {
            if (dmgSource == elementType)
            {
                damage = 0;
            }
            health -= damage;
            if (health <= 0)
            {
                Speed = MaxSpeed;
                _pathWalker.Release();
                Debug.Log("I'm dead ");
                manager.Money += 1;
                isActive = false;
                ClearDebuffs();
            }
        }
    }

    public void AddDebuff(Debuff debuff)
    {
        if (!debuffs.Exists(x => x.GetType() == debuff.GetType()))
        {
            newDebuffs.Add(debuff);
        }
    }

    public void RemoveDebuff(Debuff debuff)
    {
        debuffsToRemove.Add(debuff);
    }

    private void HandleDebuffs()
    {
        if (newDebuffs.Count > 0)
        {
            debuffs.AddRange(newDebuffs);
            newDebuffs.Clear();
        }
        
        foreach (Debuff debuff in debuffsToRemove)
        {
            debuffs.Remove(debuff);
        }
        
        debuffsToRemove.Clear();
        
        foreach (Debuff debuff in debuffs)
        {
            debuff.Update();
        }
    }
}
