using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public delegate void CurrencyChanged();

public class GameManager : MonoBehaviour
{
    public event CurrencyChanged Changed;
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private Text statText;
    
    // tower select variable's
    private TowerUI towerUI;
    [SerializeField] private GameObject selectedTower;

    public GameObject SelectedTower => selectedTower;

    // health variable's
    private int playerHealth;
    [SerializeField] private Text playerHealthtxt;
    private bool gameOver = false;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject victoryMenu;
    
    // money variable's
    private int money;
    [SerializeField] Text moneytxt;
    [SerializeField] private LoadTower loadTower;
    
    // variable's for ui and wave spawner
    private int wave;
    [SerializeField] private Text waveTxt;
    [SerializeField] private GameObject wavebtn;
    private bool waveActive;
    private int activeEnemiesInWave;
    private int currentEnemySpawn;
    private float setHealth;
    
    // my getter's and setters
    public ObjectPool Pool { get; set; }

    public int Money
    {
        get { return money;}
        set
        {
            this.money = value;
            this.moneytxt.text = value.ToString();
            
            OnCurrencyChanged();
        }
    }

    public bool InScreen { get; set; }
    
    public int PlayerHealth
    {
        get { return playerHealth;}
        set
        {
            this.playerHealth = value;
            if (playerHealth <= 0)
            {
                this.playerHealth = 0;
                GameOver();
            }
            this.playerHealthtxt.text = playerHealth.ToString();
        }
    }
    
    public int wavesEnemies
    {
        get { return activeEnemiesInWave;}
        set { activeEnemiesInWave = value; }
    }
    // standard unity functions
    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        Money = 100;
        PlayerHealth = 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeEnemiesInWave <= 0 && !gameOver)
        {
            wavebtn.SetActive(true);
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            selectTower();
        }
    }

    // playerHealth/gameover functions here
    public void GameOver()
    {
        if (!gameOver)
        {
            InScreen = true;
            gameOver = true;
            gameOverMenu.SetActive(true);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void VictoryScreen()
    {
        InScreen = true;
        victoryMenu.SetActive(true);
    }
    // My TowerUI function


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
        activeEnemiesInWave = currentEnemySpawn;
        for (int i = 0; i < currentEnemySpawn; i++)
        {
            int enemyIndex = wave;
            string type = String.Empty;

            switch (enemyIndex)
            {
                // goes of a wave by wave basis
                case 1:
                    type = "SpiritBasic";
                    break;
                case 2:
                    if (i < 4)
                    {
                        type = "SpiritBasic";
                    }
                    else
                    {
                        type = "SpiritLightning";
                    }
                    break;
                default:
                    VictoryScreen();
                    break;
            }
        
            Enemy enemy = Pool.GetObject(type).GetComponent<Enemy>();
            enemy.Health = 2;
            enemy.IsActive = true;
            
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

    public void BuyTower()
    {
        if (Money >= loadTower.CurrentPrice)
        {
            Money -= loadTower.CurrentPrice;
        }
    }

    public void OnCurrencyChanged()
    {
        if (Changed != null)
        {
            Changed();
        }
    }
    
    private void FindMyTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("tower"))
            {
                selectedTower = hit.collider.gameObject;
            }
        }
    }
    
    private void selectTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("tower"))
            {
                selectedTower = hit.collider.gameObject;
                towerUI = selectedTower.GetComponent<TowerUI>();
            }
        }
    }

    public void SellTowers()
    {
        towerUI.SellTower();
        Destroy(selectedTower);
    }

    public void ShowStats()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
    }

    public void SetTooltipText(string txt)
    {
        statText.text = txt;
    }

    public void UpdateToolTip()
    {
        
    }
}
