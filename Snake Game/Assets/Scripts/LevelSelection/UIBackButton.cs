using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBackButton : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(SoundManager.Instance.PlayBackAudio);
    }
}
