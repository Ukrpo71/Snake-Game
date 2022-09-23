using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _pickUpSound;
    [SerializeField] private AudioClip _backSound;
    [SerializeField] private AudioClip _UISound;
    [SerializeField] private AudioClip _startLevelSound;

    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayAudio(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }

    public void PlayUIAudio()
    {
        _audioSource.PlayOneShot(_UISound);
    }

    public void PlayBackAudio()
    {
        _audioSource.PlayOneShot(_backSound);
    }

    public void PlayStartLevelSound()
    {
        _audioSource.PlayOneShot(_startLevelSound);
    }
}
