using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedPlayerData : MonoBehaviour
{
    [SerializeField] private Transform _characters;
    public int SelectedIndex { get; private set; } = 0;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SelectLevel(int selectedIndex)
    {
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
