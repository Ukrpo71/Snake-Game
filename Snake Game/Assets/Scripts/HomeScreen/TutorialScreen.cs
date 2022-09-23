using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("HomeScreenExplained") == 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void FinishExplanation()
    {
        PlayerPrefs.SetInt("HomeScreenExplained", 1);
    }

    
}
