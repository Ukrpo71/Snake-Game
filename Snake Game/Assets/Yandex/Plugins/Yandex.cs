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

    [DllImport("__Internal")]
    public static extern void ShowAdv();

    public static Yandex Instance;

    private int _numberOfTimesPlayed = 0;

    public int NumberOfTimesPlayed
    {
        get { return _numberOfTimesPlayed; }
        set { 
            _numberOfTimesPlayed = value;
            if (value>=4)
            {
                ShowAdv();
                _numberOfTimesPlayed = 0;
            }
                }
    }

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

    public void ShowInterstitial()
    {
        ShowAdv();
    }
    public bool IsAuth()
    {
        string mode = GetMode();
        Debug.Log("mode: " + mode);
        if (mode == "lite")
            return false;
        else
            return true;
    }
}