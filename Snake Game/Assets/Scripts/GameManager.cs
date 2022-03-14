using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _maxAmountOfFood;

    private int _numberOfFoodOnTheField;
    private int _respawnTreshold;
    [SerializeField]
    private int _minTreshold;

    [SerializeField]
    private Spawner _spawner;


    void Start()
    {
        Init();
    }


    private void Init()
    {
        var amountToSpawn = Random.Range(0, _maxAmountOfFood);
        _spawner.SpawnAwayFrom(amountToSpawn, new Vector3(0,0,0));
        _numberOfFoodOnTheField = amountToSpawn;

        _respawnTreshold = Random.Range(_minTreshold, _maxAmountOfFood / 2);
    }

    private void ReInit()
    {
        var amountToSpawn = Random.Range(1, _maxAmountOfFood - _numberOfFoodOnTheField);
        _spawner.Spawn(amountToSpawn);
        _numberOfFoodOnTheField += amountToSpawn;

        _respawnTreshold = Random.Range(_minTreshold, _maxAmountOfFood / 2);
    }

    public void DecreaseNumberOfFood()
    {
        _numberOfFoodOnTheField--;

        if (_numberOfFoodOnTheField <= _respawnTreshold)
            ReInit();
    }
}
