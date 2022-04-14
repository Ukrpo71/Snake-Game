using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _gameWonPanel;

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _jumpsOverObstaclesText;
    [SerializeField] private Text _jumpsOverYourselfText;
    [SerializeField] private TextMeshProUGUI _timeToBeatText;
    [SerializeField] private TextMeshProUGUI _bonusText;

    [SerializeField] private int _maxAmountOfFood;
    [SerializeField] private int _minTreshold;

    private LevelGoals _levelGoals;

    private int _numberOfFoodOnTheField;
    private int _respawnTreshold;

    private int _score;
    private float _remainingTime;
    private int _timesJumpedOverObstacles = 0;
    private int _timesJumpedOverSelf = 0;

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
    private float _bonusTime = 0;
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

    public GameState CurrentGameState { get; private set; }

    private bool _gameOver;
    public bool GameOver
    {
        get { return _gameOver; }
        set 
        { 
            _gameOver = value;
            if (_gameOver)
                ChangeState(GameState.GameLost);
        }
    }

    private void ChangeState(GameState gameState)
    {
        CurrentGameState = gameState;

        switch(CurrentGameState)
        {
            case (GameState.WaitingInput):
                break;
            case (GameState.SettingUp):
                Init();
                break;
            case (GameState.Playing):
                break;
            case (GameState.GameWon):
                ShowGameWonPanel();
                break;
            case (GameState.GameLost):
                ShowGameOverPanel();
                break;
        }
    }

    public void ShowGameWonPanel()
    {
        _gameWonPanel.SetActive(true);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Start()
    {
        _levelGoals = GetComponent<LevelGoals>();
        _remainingTime = _levelGoals.TimeToBeat;

        ChangeState(GameState.WaitingInput);
        UpdateScore();
    }

    private void Update()
    {
        if (CurrentGameState == GameState.Playing)
        {
            UpdateBonusTime();
            UpdateRemainingTime();
        }

        if (CurrentGameState == GameState.WaitingInput && Input.GetMouseButtonDown(0))
            ChangeState(GameState.SettingUp);
    }

    private void Init()
    {
        var amountToSpawn = Random.Range(_minTreshold, _maxAmountOfFood);
        _spawner.SpawnAwayFrom(amountToSpawn, new Vector3(0,0,0));
        _numberOfFoodOnTheField = amountToSpawn;

        _respawnTreshold = Random.Range(_minTreshold, _maxAmountOfFood / 2);

        ChangeState(GameState.Playing);
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
        UpdateScore();

        if (_numberOfFoodOnTheField <= _respawnTreshold)
            ReInit();
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

        if (IsGoalReached())
            ChangeState(GameState.GameWon);

    }

    public void PlayerJumpedOver(bool jumpedOverObstacle)
    {
        if (_levelGoals.JumpTreshold <= _score)
        {
            if (jumpedOverObstacle)
                _timesJumpedOverObstacles++;
            else
                _timesJumpedOverSelf++;

            UpdateScore();

            if (IsGoalReached())
                ChangeState(GameState.GameWon);
        }
    }

    private bool IsGoalReached()
    {
        return (_score >= _levelGoals.Score && 
            _timesJumpedOverObstacles >= _levelGoals.JumpsOverObstacles 
            && _timesJumpedOverSelf >= _levelGoals.JumpsOverYourself);
    }

    private void UpdateBonusTime()
    {
        if (BonusTime > 0)
            BonusTime -= (Time.deltaTime);
        if (BonusTime <= 0)
        {
            BonusTime = 0;
            Multiplier = 1;
        }
    }

    private void UpdateRemainingTime()
    {
        if (_levelGoals.TimeToBeat > 0 && CurrentGameState == GameState.Playing)
        {
            _remainingTime -= Time.deltaTime;
            _timeToBeatText.text = ((int)_remainingTime).ToString();

            if (_remainingTime <= 0)
            {
                GameOver = true;
            }
        }
    }

    private void UpdateScore()
    {
        _scoreText.text = "Score: " + _score + "/" + _levelGoals.Score;
        
        //if (_levelGoals.JumpTreshold > 0)
        //    _scoreText.text += "\n" + "Treshold: " + _levelGoals.JumpTreshold;
        
        if (_levelGoals.JumpsOverObstacles > 0)
            _jumpsOverObstaclesText.text = _timesJumpedOverObstacles + "/" + _levelGoals.JumpsOverObstacles;
        if (_levelGoals.JumpsOverYourself > 0)
            _jumpsOverYourselfText.text = _timesJumpedOverSelf + "/" + _levelGoals.JumpsOverYourself;

    }

}

public enum GameState
{
    WaitingInput,
    SettingUp,
    Playing,
    GameWon,
    GameLost
}
