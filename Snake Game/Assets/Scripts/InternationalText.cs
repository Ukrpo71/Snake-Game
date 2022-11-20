using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InternationalText : MonoBehaviour
{
    [SerializeField] string _ru;
    [SerializeField] string _en;

    private void Start()
    {
        if (Yandex.Instance.Language == "en")
        {
            if (TryGetComponent(out TextMeshProUGUI text))
                text.text = _en;
            else if (TryGetComponent(out Text normalText))
                normalText.text = _en;
        }
        else if (Yandex.Instance.Language == "ru")
        {
            if (TryGetComponent(out TextMeshProUGUI text))
                text.text = _ru;
            else if (TryGetComponent(out Text normalText))
                normalText.text = _ru;
        }
        else
        {
            if (TryGetComponent(out TextMeshProUGUI text))
                text.text = _en;
            else if (TryGetComponent(out Text normalText))
                normalText.text = _en;
        }
    }
}
