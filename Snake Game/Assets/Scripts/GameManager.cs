using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _bonusText;
    [SerializeField] private Spawner _spawner;

    [SerializeField] private int _maxAmountOfFood;
    [SerializeField] private int _minTreshold;

    private int _numberOfFoodOnTheField;
    private int _respawnTreshold;

    private int _score;
    private int _multiplier = 1;
    public int Multiplier
    {
        get { return _multiplier; }
        set
        {
            if (value >= 10)
                value = 10;
            _multiplier = value;
        }
    }
    private float _bonusTime;
    public float BonusTime
    {
        get { return _bonusTime; }
        set
        {
            if (value >= 10)
                value = 10;
            _bonusTime = value;
            UpdateBonusText();
        }
    }


    private bool _gameOver;
    public bool GameOver
    {
        get { return _gameOver; }
        set 
        { 
            _gameOver = value;
            if (_gameOver)
                ShowGameOverPanel();
        }
    }

    void Start()
    {
        Init();
    }

    private void Update()
    {
        if (BonusTime > 0)
            BonusTime -= (Time.deltaTime);
        if (BonusTime <= 0)
        {
            BonusTime = 0;
            Multiplier = 1;
        }
    }

    private void Init()
    {
        var amountToSpawn = Random.Range(_minTreshold, _maxAmountOfFood);
        _spawner.SpawnAwayFrom(amountToSpawn, new Vector3(0,0,0));
        _numberOfFoodOnTheField = amountToSpawn;

        _respawnTreshold = Random.Range(_minTreshold, _maxAmountOfFood / 2);
    }

    private void ReInit()
    {
        var amountToSpawn = Random.Range(_minTreshold, _maxAmountOfFood - _numberOfFoodOnTheField);
        _spawner.SpawnAwayFrom(amountToSpawn, Vector3.zero);
        _numberOfFoodOnTheField += amountToSpawn;

        _respawnTreshold = Random.Range(_minTreshold, _maxAmountOfFood / 2);
    }

    public void PlayerAte()
    {
        _numberOfFoodOnTheField--;
        IncreaseScore();
        IncreaseBonusTime();
        Multiplier++;
        UpdateUI();
        

        if (_numberOfFoodOnTheField <= _respawnTreshold)
            ReInit();
    }

    private void UpdateUI()
    {
        UpdateScore();

    }

    private void UpdateBonusText()
    {
        _bonusText.text = "Bonus Time: " + ((int)BonusTime);
    }

    private void IncreaseBonusTime()
    {
        if (BonusTime <= 0)
            BonusTime = 10;
        else
            BonusTime++;
    }
    private void IncreaseScore()
    {
        _score+= 1 * Multiplier;
    }

    private void UpdateScore()
    {
        _scoreText.text = "Score: " + _score;
    }

    public void ShowGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
