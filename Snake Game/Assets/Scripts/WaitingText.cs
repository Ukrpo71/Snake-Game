using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitingText : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
            _event.Invoke();
    }
}
