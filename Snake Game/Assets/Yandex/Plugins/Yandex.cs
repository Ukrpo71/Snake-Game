using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);

    [DllImport("__Internal")]
    public static extern void LoadExtern();

    [DllImport("__Internal")]
    public static extern string GetMode();

    public static Yandex Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void Save(string data)
    {
        SaveExtern(data);
    }

    public void Load()
    {
        LoadExtern();
    }

    public bool IsAuth()
    {
        string mode = GetMode();
        if (mode == "lite")
            return false;
        else
            return true;
    }
}
