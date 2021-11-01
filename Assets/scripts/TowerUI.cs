using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    private Tower myTower;
    private Renderer rendererUI;
    [SerializeField] private GameObject upgradePanel;
    private Text sellText;
    private GameObject txtButton;
    private bool selected = false;
    private bool onHover;
    private GameManager myManager;

    private void Awake()
    {
        upgradePanel = GameObject.Find("Canvas");
        upgradePanel = upgradePanel.transform.GetChild(3).gameObject;
        txtButton = upgradePanel.transform.GetChild(1).gameObject;
        sellText = txtButton.transform.GetChild(0).GetComponent<Text>();

    }

    private void Update()
    {
        if (selected == true && !onHover && !IsOverUI())
        {
            if (Input.GetMouseButtonDown(0))
            {
                DeselectTower();
            }
        }
    }

    private void OnMouseOver()
    {
        onHover = true;
    }

    private void OnMouseExit()
    {
        onHover = false;
    }

    private void OnMouseDown()
    {
        SelectTower();
    }

    private void SelectTower()
    {
        myTower = gameObject.transform.GetChild(0).GetComponent<Tower>();
        rendererUI = gameObject.transform.GetChild(0).GetComponent<Renderer>();
        sellText.text = "+ " + (myTower.Price / 2).ToString();
        
        rendererUI.enabled = true;
        selected = true;
        upgradePanel.SetActive(true);
    }

    private void DeselectTower()
    {
        rendererUI.enabled = false;
        selected = false;
        upgradePanel.SetActive(false);

        myTower = null;
        rendererUI = null;
    }

    private bool IsOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
