using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataPersist : MonoBehaviour
{
    private static DataPersist dataPersist;

    public PlayerData PlayerData;

    public int NumberOfFinishedLevels => PlayerData.Levels.Count(t => t.IsFinished);

    private string _path;

    private void Awake()
    {
        if (dataPersist == null)
            dataPersist = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
        _path = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "save.json";
        Load();
    }


    public void Save()
    {
        UpdateAnimals();
        UpdateSkins();
        string json = JsonUtility.ToJson(PlayerData);
        File.WriteAllText(_path, json);
    }

    public void Load()
    {
        if (File.Exists(_path))
        {
            string json = File.ReadAllText(_path);
            PlayerData = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            PlayerData = FindObjectOfType<InitialDataPersist>().PlayerData;
        }
    }

    private void UpdateAnimals()
    {
        var animal = PlayerData.Animals.FirstOrDefault(a => a.AnimalType == AnimalType.Chicken);
        animal.NumberOfCollectedAnimals = 1 + NumberOfFinishedLevels / 4;
    }

    private void UpdateSkins()
    {
        foreach (var skin in PlayerData.Skins)
            if (skin.UnlockingRequirement != 0 && skin.UnlockingRequirement <= NumberOfFinishedLevels)
            {
                skin.IsUnlocked = true;
            }
    }
}
