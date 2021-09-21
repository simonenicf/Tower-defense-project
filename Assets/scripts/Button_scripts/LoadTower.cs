using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LoadTower : MonoBehaviour
{
    private GameObject loadedPrefab;
    
    public void loadPrefab()
    {
        loadedPrefab = (GameObject) AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/prefab-test.prefab");
    }
    
}
