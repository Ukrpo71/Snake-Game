using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedPlayerData : MonoBehaviour
{
    private static SelectedPlayerData instance;
    [SerializeField] private Transform _characters;
    public int SelectedIndex { get; private set; } = 0;
    
    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
        DataPersist dataPersit = FindObjectOfType<DataPersist>();
        SelectedIndex = dataPersit.PlayerData.SelectedSkin;
    }

    public void SelectLevel(int selectedIndex)
    {
        var _dataPersist = FindObjectOfType<DataPersist>();
        _dataPersist.PlayerData.SelectedSkin = selectedIndex;
        SelectedIndex = selectedIndex;
    }

    public void ChangeCharacter()
    {
        foreach (Transform child in _characters)
        {
            child.gameObject.SetActive(false);
        }

        _characters.GetChild(SelectedIndex).gameObject.SetActive(true);
    }
}
