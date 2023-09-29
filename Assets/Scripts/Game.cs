using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    
    [SerializeField] private Transform startSpawnPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private GameObject balloonPreFab;
    [SerializeField] private GameObject[] towers;
    [SerializeField] private TMP_Text roundTxt;
    [SerializeField] private TMP_Text moneyTxt;
    [SerializeField] private TMP_Text livesTxt;
    [SerializeField] private Button startRoundBtn;
    [SerializeField] private Button[] menuBtns;
    [SerializeField] private Transform towerSpawnPos;
    
    private int _lives = 50;
    private int _round = 1;
    private int _money = 1000;
    private int _pointsToWinRound;
    private int _pointsCollectedThisRound;
    
    private List<Color> _balloons;
    private int[] costs = { 600, 700, 100 };
    
    // Static reference to the instance of the GameLogic script.
    public static Game Instance { get; private set; }

    private void Awake()
    {
        // Ensure there's only one instance of the GameLogic script.
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Keep the GameLogic GameObject persistent across scenes.
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        // Clean up the reference when the GameObject is destroyed.
        if (Instance == this)
        {
            Instance = null;
        }
    }
    
    void Start()
    {
        roundTxt.text = "1";
        moneyTxt.text = "1000";
        livesTxt.text = "50";
        
        LoadRound(1);
    }

    public void StartRound()
    {
        startRoundBtn.enabled = false;
        foreach (var btn in menuBtns)
        {
            btn.enabled = false;
        }
        _ = SpawnBalloons();
    }
    async Task SpawnBalloons()
    {
        foreach (var balloonColor in _balloons)
        {
            await Task.Delay(1000);
            GameObject tmpBalloon = Instantiate(balloonPreFab); 
            tmpBalloon.transform.position = startSpawnPos.position;
            tmpBalloon.GetComponent<SpriteRenderer>().color = balloonColor;
        }
    }
    void LoadRound(int roundNumber)
    {
        _balloons = new List<Color>();
        
        switch (roundNumber)
        {
            case 1:
                AddBalloons(20, Color.red);
                _pointsToWinRound = 20;
                _pointsCollectedThisRound = 0;
                break;
            case 2:
                AddBalloons(35, Color.red);
                _pointsToWinRound = 35;
                _pointsCollectedThisRound = 0;
                break;
            case 3:
                AddBalloons(25, Color.red);
                AddBalloons(5, Color.blue);
                _pointsToWinRound = 30;
                _pointsCollectedThisRound = 0;
                break;
            case 4:
                AddBalloons(35, Color.red);
                AddBalloons(18, Color.blue);
                _pointsToWinRound = 53;
                _pointsCollectedThisRound = 0;
                break;
            case 5:
                AddBalloons(5, Color.red);
                AddBalloons(27, Color.blue);
                _pointsToWinRound = 32;
                _pointsCollectedThisRound = 0;
                break;
            case 6:
                AddBalloons(15, Color.red);
                AddBalloons(15, Color.blue);
                AddBalloons(4, Color.green);
                _pointsToWinRound = 34;
                _pointsCollectedThisRound = 0;
                break;
            case 7:
                AddBalloons(20, Color.red);
                AddBalloons(20, Color.blue);
                AddBalloons(5, Color.green);
                _pointsToWinRound = 45;
                _pointsCollectedThisRound = 0;
                break;
            case 8:
                AddBalloons(10, Color.red);
                AddBalloons(20, Color.blue);
                AddBalloons(14, Color.green);
                _pointsToWinRound = 44;
                _pointsCollectedThisRound = 0;
                break;
            case 9:
                AddBalloons(30, Color.green);
                _pointsToWinRound = 30;
                _pointsCollectedThisRound = 0;
                break;
            case 10:
                AddBalloons(100, Color.blue);
                _pointsToWinRound = 100;
                _pointsCollectedThisRound = 0;
                break;
            default:
                break;
        }
    }
    private void AddBalloons(int amount, Color color)
    {
        for (int i = 0; i < amount; i++)
        {
            _balloons.Add(color);
        }
    }
    public void SpawnTower(int i)
    {
        if (_money < costs[i])
        {
            //no money to buy tower
        }
        else
        {
            GameObject selectedTower = towers[i];
            if (selectedTower != null)
            {
                GameObject tower = Instantiate(selectedTower);
                tower.transform.position = towerSpawnPos.position;
                _money -= costs[i];
                moneyTxt.text = _money.ToString();
            }
        }
        
    }

    public void BalloonPassed()
    {
        _lives--;
        livesTxt.text = _lives.ToString();
        if (_lives == 0)
        {
            PlayerPrefs.SetString("WinLose", "LOSER HAHA");
            SceneManager.LoadScene("Scenes/End");
        }
    }

    public void Hit()
    {
        _money += 10;
        moneyTxt.text = _money.ToString();
    }

    public void AddPoint()
    {
        _pointsCollectedThisRound++;
        if (_pointsCollectedThisRound == _pointsToWinRound)
        {
            if (_round == 10)
            {
                PlayerPrefs.SetString("WinLose", "You are the man, you WIN!");
                SceneManager.LoadScene("Scenes/End");
            }
            roundTxt.text = (++_round).ToString();
            LoadRound(_round);
            startRoundBtn.enabled = true;
            foreach (var btn in menuBtns)
            {
                btn.enabled = true;
            }
        }
    }
}
