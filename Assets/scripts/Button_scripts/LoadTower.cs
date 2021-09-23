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

    public buildtowers towerBuildScript;
    //private GameObject getCanvas;
    //private Transform thisCanvas;
    //private GameObject towerButton1;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            LoadPrefab();
        }
    }

    private void LoadPrefab()
    {
        loadedPrefab = (GameObject) AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/Capsule.prefab");
        Debug.Log(loadedPrefab);
        towerBuildScript.SetPrefab(loadedPrefab);
    }
}
