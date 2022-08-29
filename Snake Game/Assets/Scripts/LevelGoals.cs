using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGoals : MonoBehaviour
{
    [SerializeField] private int _levelScoreGoal;
    [SerializeField] private float _timeToBeat = 0;
    [SerializeField] private int _jumpTreshold = 0;
    [SerializeField] private int _jumpOverObstaclesGoal = 0;
    [SerializeField] private int _jumpOverSelfGoal = 0;

    public int Score { get { return _levelScoreGoal; } }
    public float TimeToBeat { get { return _timeToBeat; } }
    public int JumpTreshold { get { return _jumpTreshold; } }
    public int JumpsOverObstacles { get { return _jumpOverObstaclesGoal; } }
    public int JumpsOverYourself { get { return _jumpOverSelfGoal; } }

}
