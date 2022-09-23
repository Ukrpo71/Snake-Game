using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private AudioClip _UISound;
    public void Play()
    {
        DataPersist dataPersist = FindObjectOfType<DataPersist>();
        dataPersist.Save();
        SoundManager.Instance.PlayAudio(_UISound);
        SceneManager.LoadScene("HomeScreen");
    }
}
