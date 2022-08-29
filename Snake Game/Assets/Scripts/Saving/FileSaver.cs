using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileSaver : MonoBehaviour
{
    public PlayerData playerData;
    public PlayerData newPlayerData;

    public string path;

    void Start()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "save.json";
        //playerData = new PlayerData(3, 2);
    }

    public void SaveData()
    {
        Debug.Log("Saving at " + path);

        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(path, json);
    }

    public void LoadData()
    {
        string json = File.ReadAllText(path);
        newPlayerData = JsonUtility.FromJson<PlayerData>(json);
    }
}
