using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class renderobject : MonoBehaviour
{
    [SerializeField]
    private Renderer prefabload;

    private void Start()
    {
        prefabload = GetComponent<Renderer>();
        
    }
}
