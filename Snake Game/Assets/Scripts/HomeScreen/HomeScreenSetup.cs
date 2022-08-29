using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomeScreenSetup : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private TMP_Text _chickenText;

    private void Start()
    {
        DataPersist dataPersist = FindObjectOfType<DataPersist>();

        foreach(var animal in dataPersist.PlayerData.Animals)
        {
            _spawner.SpawnAwayFrom(animal.AnimalType, animal.NumberOfCollectedAnimals);
        }

        _chickenText.text = dataPersist.PlayerData.Animals[0].NumberOfCollectedAnimals + "/5";
    }
}
