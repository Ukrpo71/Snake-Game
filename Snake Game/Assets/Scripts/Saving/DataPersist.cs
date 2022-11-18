using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using CloudOnce;
using System.Runtime.InteropServices;

public class DataPersist : MonoBehaviour
{
    private static DataPersist dataPersist;

    public PlayerData PlayerData;

    public int NumberOfFinishedLevels => PlayerData.Levels.Count(t => t.IsFinished);

    private string _path;

    private string _json;
    private bool _loaded;

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

#if UNITY_WEBGL
        //SetAuthorization(IsAuthorized());
        if (Yandex.Instance.IsAuth())
        {
            Debug.Log("Saving to yandex cloud");
            Yandex.Instance.Save(json);
        }
        else
            Debug.Log("Not authorized to save on yandex cloud");
#endif
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
        //SetAuthorization(IsAuthorized());
        if (Yandex.Instance.IsAuth())
        {
            Debug.Log("Person authorized");
            Yandex.Instance.Load();
/*            Debug.Log("Json from yandex load: " + _json);
            if (_json != null)
            {
                Debug.Log("savedGameData: " + (_json));
                PlayerData = JsonUtility.FromJson<PlayerData>(Base64Decode(_json));
            }
*/        }
        else
        {
            LoadFromFile();
        }
    }

    public void StartLoadingYandexData(string data)
    {
        Debug.Log("save file from load yandex data" + data);
        _json = data;
    }

    IEnumerator WaitUntilYandexLoads(string data)
    {
        Debug.Log("save file from load yandex data" + data);
        _json = data;
        yield return new WaitForSeconds(1.5f);
    }

    public void LoadYandexData(string data)
    {
        Debug.Log("save file from load yandex data" + data);
        _json = data;

        if(_json != null)
        {
            Debug.Log("savedGameData: " + (_json));
            PlayerData = JsonUtility.FromJson<PlayerData>(_json);
        }
        else
        {
            LoadFromFile();
        }
    }
    
    public void LoadFromFile()
    {
        if (_json == null && File.Exists(_path))
        {
            Debug.Log("Loading from local save file");
            string json = File.ReadAllText(_path);
            PlayerData = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            Debug.Log("No save on file or cloud, making new save");
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
