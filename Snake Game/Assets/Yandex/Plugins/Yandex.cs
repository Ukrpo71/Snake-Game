using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);

    [DllImport("__Internal")]
    private static extern void BuySkin(string skinName);

    [DllImport("__Internal")]
    public static extern void LoadExtern();

    [DllImport("__Internal")]
    private static extern void BuyAllSkins();

    [DllImport("__Internal")]
    public static extern string GetMode();

    [DllImport("__Internal")]
    public static extern void ShowAdv();

    [DllImport("__Internal")]
    public static extern string GetLang();

    public static Yandex Instance;

    private int _numberOfTimesPlayed = 0;

    public string Language;

    public int NumberOfTimesPlayed
    {
        get { return _numberOfTimesPlayed; }
        set
        {
            _numberOfTimesPlayed = value;
            if (value >= 4)
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

    private void Start()
    {
        Language = GetLang();
    }

    public void BuySkins(string skinName)
    {
        BuySkin(skinName);
    }

    public void BuyAll()
    {
        BuyAllSkins();
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
