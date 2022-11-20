using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuySkinButton : MonoBehaviour
{
    private string _skinName;

    public void SetSkinName(string skinName)
    {
        _skinName = skinName;
    }

    public void UnlockSkin()
    {
        Yandex.Instance.BuySkins(_skinName);
    }
}
