using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnPrefab;

    [SerializeField]
    private GameObject _floor;

    public void Spawn(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(_spawnPrefab, GenerateRandomSpawnPosition(), Quaternion.identity);
        }
        
    }

    public void SpawnAwayFrom(int amount, Vector3 awayFrom)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(_spawnPrefab, GenerateRandomSpawnPositionAwayFrom(awayFrom), Quaternion.identity);
        }
    }

    private Vector3 GenerateRandomSpawnPosition()
    {
        var xBounds = _floor.GetComponent<BoxCollider>().bounds.size.x;
        var zBounds = _floor.GetComponent<BoxCollider>().bounds.size.z;

        var yBounds = _floor.GetComponent<BoxCollider>().bounds.size.y;

        Vector3 randomPosition = new Vector3(Random.Range(-xBounds / 2, xBounds / 2), yBounds * 0.61f,Random.Range(-yBounds / 2, yBounds));
        return randomPosition;

    }

    private Vector3 GenerateRandomSpawnPositionAwayFrom(Vector3 awayFrom)
    {
        var point = Random.insideUnitCircle.normalized * Random.Range(3, 9);
        point.y = 0.5f;
        return point;
    }

    
}
