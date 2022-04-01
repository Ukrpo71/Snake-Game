using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectHolder : MonoBehaviour
{
    [SerializeField] private GameObject _levelSelectors;
    public void ChangeBackGround()
    {
        foreach (Transform level in _levelSelectors.transform)
        {
            level.Find("BackgroundActivated").gameObject.SetActive(false);
            level.Find("Background").gameObject.SetActive(true);
        }

        gameObject.transform.Find("BackgroundActivated").gameObject.SetActive(true);
    }


}
