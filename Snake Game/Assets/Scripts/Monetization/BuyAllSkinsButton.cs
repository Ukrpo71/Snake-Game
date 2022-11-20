using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyAllSkinsButton : MonoBehaviour
{
    private void Start()
    {
        var dataPersist = FindObjectOfType<DataPersist>();
        if (dataPersist.PlayerData.Skins.FindAll(s => s.IsUnlocked == false).Count <= 1)
            gameObject.SetActive(false);
    }

    public void BuyAllSkins()
    {
        Yandex.Instance.BuyAll();
    }
}
