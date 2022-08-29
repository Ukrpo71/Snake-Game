using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _gameWonPanel;

    [SerializeField] private GameObject _jumpsOverObstaclesPanel;
    [SerializeField] private GameObject _jumpsOverYourselfPanel;


    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _jumpsOverObstaclesText;
    [SerializeField] private TextMeshProUGUI _jumpsOverYourselfText;
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

    void Start()
    {
        _levelGoals = GetComponent<LevelGoals>();
        _remainingTime = _levelGoals.TimeToBeat;
        InitScorePanel();
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

    private void InitScorePanel()
    {
        if (_levelGoals.JumpsOverObstacles <= 0)
            _jumpsOverObstaclesPanel.SetActive(false);
        if (_levelGoals.JumpsOverYourself <= 0)
            _jumpsOverYourselfPanel.SetActive(false);
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

    public void NextLevel()
    {
        int levelNumber = int.Parse(SceneManager.GetActiveScene().name);
        var dataPersist = FindObjectOfType<DataPersist>();

        var level = dataPersist.PlayerData.Levels.FirstOrDefault(l => l.Number == levelNumber);
        level.IsFinished = true;

        var nextLevel = dataPersist.PlayerData.Levels.FirstOrDefault(l => l.IsUnlocked == false);
        if (nextLevel != null)
        {
            nextLevel.IsUnlocked = true;

            dataPersist.Save();

            SceneManager.LoadScene("LevelSelection");
        }
        else
        {
            SceneManager.LoadScene("HomeScreen");
        }
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
        _bonusText.text = "Bonus Time: " + ((int)BonusTime).ToString();
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

        if (_levelGoals.JumpTreshold > 0 && _score >= _levelGoals.JumpTreshold)
        {
            _scoreText.color = Color.yellow;
            _scoreText.fontStyle = FontStyles.Bold;

            if (_jumpsOverObstaclesPanel.activeInHierarchy)
            {
                _jumpsOverObstaclesPanel.GetComponent<CanvasGroup>().alpha = 1;
                _jumpsOverObstaclesText.fontStyle = FontStyles.Bold;

            }
            if (_jumpsOverYourselfPanel.activeInHierarchy)
            {
                _jumpsOverYourselfPanel.GetComponent<CanvasGroup>().alpha = 1;
                _jumpsOverYourselfText.fontStyle = FontStyles.Bold;
            }
        }

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
        {
            _bonusText.gameObject.SetActive(true);
            BonusTime -= (Time.deltaTime);
        }
        if (BonusTime <= 0)
        {
            BonusTime = 0;
            _bonusText.gameObject.SetActive(false);
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
        _scoreText.text = _score + "/" + _levelGoals.Score;
        
        if (_levelGoals.JumpsOverObstacles > 0 && _timesJumpedOverObstacles <= _levelGoals.JumpsOverObstacles)
            _jumpsOverObstaclesText.text = _timesJumpedOverObstacles + "/" + _levelGoals.JumpsOverObstacles;

        if (_levelGoals.JumpsOverYourself > 0 && _timesJumpedOverSelf <= _levelGoals.JumpsOverYourself)
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
