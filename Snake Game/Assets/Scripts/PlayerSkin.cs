using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    private int selectedIndex = 0;
    void Start()
    {
        if(GameObject.Find("DataPersist").TryGetComponent(out DataPersist dataPersist))
        {
            selectedIndex = dataPersist.PlayerData.SelectedSkin;
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        transform.GetChild(selectedIndex).gameObject.SetActive(true);
    }

    
}
