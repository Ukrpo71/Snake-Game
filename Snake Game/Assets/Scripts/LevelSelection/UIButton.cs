using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SoundManager.Instance.PlayUIAudio);
    }
}
