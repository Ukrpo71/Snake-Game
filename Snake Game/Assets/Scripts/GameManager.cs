using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Spawner _spawner;

    [SerializeField] private int _maxAmountOfFood;
    [SerializeField] private int _minTreshold;

    private int _numberOfFoodOnTheField;
    private int _respawnTreshold;

    private int _score;


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

    public int Score { 
        get => _score; 
        set
        { 
            _score = value;
            UpdateScore();
        } 
    }

    void Start()
    {
        Init();
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

    public void DecreaseNumberOfFood()
    {
        _numberOfFoodOnTheField--;
        IncreaseScore();

        if (_numberOfFoodOnTheField <= _respawnTreshold)
            ReInit();
    }

    private void IncreaseScore()
    {
        Score++;
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
