using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using CloudOnce;

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

        //Application.targetFrameRate = 60;

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
        Debug.Log("json: " + json);
        Debug.Log("Encoded json: " + Base64Encode(json));
        //CloudVariables.savedGameData = Base64Encode(json);
        //Cloud.Storage.Save();
    }

    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }


    public void Load()
    {
        if (File.Exists(_path))
        {
            string json = File.ReadAllText(_path);
            PlayerData = JsonUtility.FromJson<PlayerData>(json);
        }
        /*else if (CloudVariables.savedGameData != "")
        {

            string json = CloudVariables.savedGameData;
            Debug.Log("savedGameData: " + Base64Decode(json));
            PlayerData = JsonUtility.FromJson<PlayerData>(Base64Decode(json));
        }
        */
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
