using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    private DataPersist _dataPersist;

    void Start()
    {
        _dataPersist = FindObjectOfType<DataPersist>();

        if (_dataPersist.PlayerData.HomeScreenExplained == true)
        {
            gameObject.SetActive(false);
        }
    }

    public void FinishExplanation()
    {
        //PlayerPrefs.SetInt("HomeScreenExplained", 1);
        _dataPersist.PlayerData.HomeScreenExplained = true;
    }

    public void FinishGameTutorial()
    {
        _dataPersist.PlayerData.TutorialFinished = true;
        _dataPersist.Save();
    }

    
}
