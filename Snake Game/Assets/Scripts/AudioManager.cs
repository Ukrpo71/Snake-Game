using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _collectedAnimal;
    private AudioManager _audioManager;
    

    void Start()
    {
        _collectedAnimal.Play();
    }

    public void PlayClip(AudioSource audio)
    {

    }

    void Update()
    {
        
    }
}
