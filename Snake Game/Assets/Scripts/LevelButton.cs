using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class LevelButton : MonoBehaviour
{
    [SerializeField] private Level _level;

    public Level Level { get { return _level; } }

    [SerializeField] private GameObject _lock;
    [SerializeField] private GameObject _star;

    private int _levelIndex;

    public void Setup(Level level)
    {
        _level = level;
        if (_level.IsUnlocked)
            _lock.SetActive(false);
        if (_level.IsFinished)
            _star.SetActive(true);

        _level.Number = int.Parse(gameObject.name);
        _levelIndex = int.Parse(gameObject.name);

        GetComponentInChildren<TextMeshProUGUI>().text = _levelIndex.ToString();
        if (_level.IsUnlocked)
        {
            GetComponent<Button>().onClick.AddListener(LoadLevel);
            GetComponent<Button>().onClick.AddListener(SoundManager.Instance.PlayUIAudio);
        }
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(_levelIndex.ToString());
    }
}
