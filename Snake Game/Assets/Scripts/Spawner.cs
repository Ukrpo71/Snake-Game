using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnPrefab;

    [SerializeField]
    private BoxCollider _floor;

    public void Spawn(int amount)
    {
        Debug.Log("Spawn");
        for (int i = 0; i < amount; i++)
        {
            Instantiate(_spawnPrefab, GenerateRandomSpawnPosition(), Quaternion.identity);
        }
        
    }

    public void SpawnAwayFrom(int amount, Vector3 awayFrom)
    {
        Debug.Log("Spawn Away");
        for (int i = 0; i < amount; i++)
        {
            Instantiate(_spawnPrefab, GenerateRandomSpawnPositionAwayFrom(awayFrom), Quaternion.identity);
        }
    }

    private Vector3 GenerateRandomSpawnPosition()
    {
        var xBounds = _floor.bounds.size.x;
        var zBounds = _floor.bounds.size.z;

        var yBounds = _floor.bounds.size.y;

        Vector3 randomPosition = new Vector3(Random.Range(-xBounds / 2, xBounds / 2), yBounds * 0.61f,Random.Range(-yBounds / 2, yBounds));
        return randomPosition;

    }

    private Vector3 GenerateRandomSpawnPositionAwayFrom(Vector3 awayFrom)
    {
        var point = Random.insideUnitSphere.normalized * Random.Range(_floor.bounds.size.x / 3, _floor.bounds.size.x/2 - 1);
        point.y = 0.25f;
        return point;
    }

    
}
