using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class LevelButton : MonoBehaviour
{
    private int _levelIndex;

    private void Start()
    {
        _levelIndex = int.Parse(gameObject.name);

        GetComponentInChildren<TextMeshProUGUI>().text = _levelIndex.ToString();
        GetComponent<Button>().onClick.AddListener(LoadLevel);
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(_levelIndex.ToString());
    }
}
