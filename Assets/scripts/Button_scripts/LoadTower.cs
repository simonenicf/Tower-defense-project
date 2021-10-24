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

    public TowerSystem towerBuildScript;
    //private GameObject getCanvas;
    //private Transform thisCanvas;
    //private GameObject towerButton1;
    
    private void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            LoadTower1();
        }
        if (Input.GetKeyDown("s"))
        {
            LoadTower2();
        }
    }

    public void LoadTower1()
    {
        loadedPrefab = (GameObject) AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/Towers/AoE_tower.prefab");
        Debug.Log(loadedPrefab);
        towerBuildScript.SetPrefab(loadedPrefab);
    }

    public void LoadTower2()
    {
        loadedPrefab = (GameObject) AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/Towers/Base_tower.prefab");
        Debug.Log(loadedPrefab);
        towerBuildScript.SetPrefab(loadedPrefab);
    }
}
