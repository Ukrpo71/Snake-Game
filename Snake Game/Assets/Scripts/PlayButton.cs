using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void Play()
    {
        DataPersist dataPersist = FindObjectOfType<DataPersist>();
        dataPersist.Save();
        SceneManager.LoadScene("HomeScreen");
    }
}
