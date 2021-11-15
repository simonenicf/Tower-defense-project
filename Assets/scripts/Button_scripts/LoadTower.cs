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

    private void PriceCheck()
    {
        if (price <= gameManagerScript.Money)
        {
            GetComponent<Image>().color = Color.white;
            baseTowerPriceTxt.color = Color.black;
        }
        else
        {
            GetComponent<Image>().color = Color.gray;
            baseTowerPriceTxt.color = Color.gray;
        }
    }

    public void ShowInfo(string type)
    {
        string tooltip = String.Empty;

        switch (type)
        {
            case "Base":
                tooltip = string.Format("<color=#ffffff><size=50><b>Base Tower</b></size></color>\nDamage: 1 \nAttackSpeed: 3 \nCrit chance: 100% \nCrit damage: *2 \nCan crit");
                break;
            case "Aoe":
                tooltip = string.Format("<color=#000000><size=50><b>Aoe Tower</b></size></color>\nDamage: 1 \nAttackSpeed: 3 \nCrit chance: 100% \nCrit damage: *2 \nCan crit");
                break;
            case "Fire":
                tooltip = string.Format("<color=#8a0000><size=50><b>Fire Tower</b></size></color>\nDamage: 1 \nAttackSpeed: 3 \nProc: 100% \nDebuff duration: 1sec \nTick time: 1 sec \nTick damage: 1 \nCan apply a DOT to the target");
                break;
            case "Ice":
                tooltip = string.Format("<color=#00d1d1><size=50><b>Ice Tower</b></size></color>\nDamage: 1 \nProc: 100%\nDebuff duration: 1sec\nSlowing factor: 25%\nHas a chance to slow down the target");
                break;
            case "Acid":
                tooltip = string.Format("<color=#00cc25><size=50><b>Acid Tower</b></size></color>\nDamage: 1 \nProc: 100%\nDebuff duration: 5sec \nTick time: 5 sec \nSplash damage: 1\nCan apply dripping acid");
                break;
            case "Storm":
                tooltip = string.Format("<color=#d8e034><size=50><b>Storm Tower</b></size></color>\nDamage: 1 \nProc: 100%\nDebuff duration: 1sec\n Has a chance to stun the target");
                break;
            default:
                break;
        }
        
        gameManagerScript.SetTooltipText(tooltip);
        gameManagerScript.ShowStats();
    }
    
    // unity functions
    private void Start()
    {
        baseTowerPriceTxt.text = "tower cost: " + price;

        gameManagerScript.Changed += new CurrencyChanged(PriceCheck);
    }

 
    private void Update()
    {
        
        if (Input.GetKeyDown("a"))
        {
            AoeTower();
        }
        if (Input.GetKeyDown("b"))
        {
            BaseTower();
        }
        if (Input.GetKeyDown("i"))
        {
            IceTower();
        }
        if (Input.GetKeyDown("f"))
        {
            FireTower();
        }
        if (Input.GetKeyDown("t"))
        {
            ToxicTower();
        }
        if (Input.GetKeyDown("s"))
        {
            StormTower();
        }
    }
    
    // load tower functions
    public void AoeTower()
    {
        if (gameManagerScript.Money >= 5 && !gameManagerScript.InScreen)
        {
            loadedPrefab = (GameObject) AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/Towers/AoE_tower.prefab");
            Debug.Log(loadedPrefab);
            towerBuildScript.SetPrefab(loadedPrefab);
            CurrentPrice = 5;
        }
    }

    public void BaseTower()
    {
        if (gameManagerScript.Money >= price && !gameManagerScript.InScreen)
        {
            loadedPrefab = (GameObject) AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/Towers/Base_tower.prefab");
            Debug.Log(loadedPrefab);
            towerBuildScript.SetPrefab(loadedPrefab);
            CurrentPrice = 2;
        }
    }

    public void FireTower()
    {
        if (gameManagerScript.Money >= 10 && !gameManagerScript.InScreen)
        {
            loadedPrefab = (GameObject) AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/Towers/FireTower.prefab");
            towerBuildScript.SetPrefab(loadedPrefab);
            CurrentPrice = 10;
        }
    }

    public void IceTower()
    {
        if (gameManagerScript.Money >= 12 && !gameManagerScript.InScreen)
        {
            loadedPrefab =
                (GameObject) AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/Towers/IceTower.prefab");
            towerBuildScript.SetPrefab(loadedPrefab);
            CurrentPrice = 12;
        }
    }

    public void ToxicTower()
    {
        if (gameManagerScript.Money >= 15 && !gameManagerScript.InScreen)
        {
            loadedPrefab =
                (GameObject) AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/Towers/ToxicTower.prefab");
            towerBuildScript.SetPrefab(loadedPrefab);
            CurrentPrice = 15;
        }
    }

    public void StormTower()
    {
        if (gameManagerScript.Money >= 20 && !gameManagerScript.InScreen)
        {
            loadedPrefab =
                (GameObject) AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/Towers/StormTower.prefab");
            towerBuildScript.SetPrefab(loadedPrefab);
            CurrentPrice = 20;
        }
    }
}
