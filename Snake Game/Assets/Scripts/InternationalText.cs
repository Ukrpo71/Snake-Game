using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternationalText : MonoBehaviour
{
    [SerializeField] string _ru;
    [SerializeField] string _en;

    private void Start()
    {
        if (Yandex.Instance.Language == "en")
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = _en;
        }
        else if (Yandex.Instance.Language == "ru")
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = _ru;
        }
        else
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = _en;
        }
    }
}
