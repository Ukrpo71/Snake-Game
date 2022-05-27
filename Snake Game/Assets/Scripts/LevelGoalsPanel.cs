using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelGoalsPanel : MonoBehaviour
{
    [SerializeField] private LevelGoals _levelGoals;

    [SerializeField] private GameObject _jumpsOverObstaclesGoal;
    [SerializeField] private GameObject _jumpsOverYourselfGoal;
    [SerializeField] private GameObject _jumpTresholdGoal;

    [SerializeField] private TextMeshProUGUI _collectAnimalsGoal;
    [SerializeField] private TextMeshProUGUI _jumpsOverObstaclesText;
    [SerializeField] private TextMeshProUGUI _jumpsOverYourselfText;
    [SerializeField] private TextMeshProUGUI _jumpTresholdText;


    private void Start()
    {
        _collectAnimalsGoal.text = _levelGoals.Score.ToString();

        if (_levelGoals.JumpsOverObstacles > 0)
        {
            _jumpsOverObstaclesGoal.SetActive(true);
            _jumpsOverObstaclesText.text = _levelGoals.JumpsOverObstacles.ToString();

        }
        if (_levelGoals.JumpsOverYourself > 0)
        {
            _jumpsOverYourselfGoal.SetActive(true);
            _jumpsOverYourselfText.text = _levelGoals.JumpsOverYourself.ToString();
        }

        if (_levelGoals.JumpTreshold > 0)
        {
            _jumpTresholdGoal.SetActive(true);
            _jumpTresholdText.SetText($"Jumps will count only after collecting " + $"<b>{_levelGoals.JumpTreshold.ToString()}</b>"
                + $" animals");
        }
    }

    

}
