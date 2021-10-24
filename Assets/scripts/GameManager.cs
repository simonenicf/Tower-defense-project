using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // my variable's

    // variable's for ui and wave spawner
    private int wave;
    [SerializeField] private Text waveTxt;
    [SerializeField] private GameObject wavebtn;
    private bool waveActive;
    private int activeEnemiesInWave;
    private int currentEnemySpawn;
    // my getter's and setters
    public ObjectPool Pool { get; set; }

    public int wavesEnemies
    {
        get { return activeEnemiesInWave;}
        set { activeEnemiesInWave = value; }
    }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (activeEnemiesInWave <= 0)
        {
            wavebtn.SetActive(true);
        }
    }

    // my wave spawning functions
    public void StartWave()
    {
        wave++;
        waveTxt.text = string.Format("Wave: <color=lime>{0}</color>", wave);
        Debug.Log("works");
        StartCoroutine(SpawnWave());
        wavebtn.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        AmountToSpawn();
        for (int i = 0; i < currentEnemySpawn; i++)
        {
            activeEnemiesInWave++;
            int enemyIndex = wave;
            string type = String.Empty;

            switch (enemyIndex)
            {
                // goes of a wave by wave basis
                case 1:
                    type = "spiritRed";
                    break;
                case 2:
                    if (i < 4)
                    {
                        type = "spiritRed";
                    }
                    else
                    {
                        type = "spiritPurple";
                    }
                    break;
                default:
                    break;
            }
        
            Enemy enemy = Pool.GetObject(type).GetComponent<Enemy>();
        
            yield return new WaitForSeconds(1.5f);
        }
    }
    
    // customize the amount of enemies that can be spawned
    private int AmountToSpawn()
    {
        switch (wave)
        {
            case 1:
                currentEnemySpawn = 3;
                break;
            case 2:
                currentEnemySpawn = 5;
                break;
            default:
                break;
        }

        return currentEnemySpawn;
    }
}
