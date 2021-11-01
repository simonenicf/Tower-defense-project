using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LoadTower : MonoBehaviour
{
    private GameObject loadedPrefab;
    [SerializeField] private GameManager gameManagerScript;
    public TowerSystem towerBuildScript;

    [SerializeField] private int price;
    private int currentPrice;
    [SerializeField] private Text baseTowerPriceTxt;
    
    // getters and setters
    public int Price
    {
        get { return price; }
    }

    public int CurrentPrice
    {
        get { return currentPrice;}
        set { currentPrice = value; }
    }

    // unity functions
    private void Start()
    {
        baseTowerPriceTxt.text = "tower cost: " + price;
    }

 
    private void Update()
    {
        
        if (Input.GetKeyDown("b"))
        {
            AoeTower();
        }
        if (Input.GetKeyDown("s"))
        {
            BaseTower();
        }
    }
    
    // load tower functions
    public void AoeTower()
    {
        if (gameManagerScript.Money >= price)
        {
            loadedPrefab = (GameObject) AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/Towers/AoE_tower.prefab");
            Debug.Log(loadedPrefab);
            towerBuildScript.SetPrefab(loadedPrefab);
            CurrentPrice = 5;
        }
    }

    public void BaseTower()
    {
        if (gameManagerScript.Money >= price)
        {
            loadedPrefab = (GameObject) AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/Towers/Base_tower.prefab");
            Debug.Log(loadedPrefab);
            towerBuildScript.SetPrefab(loadedPrefab);
            CurrentPrice = 2;
        }
    }
}
