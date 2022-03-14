using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleTrigger : MonoBehaviour
{
    [SerializeField]
    private float _invisibleTimer = 1;

    private float _timer = 0;
    void Start()
    {
        transform.GetComponentInChildren<BoxCollider>().isTrigger = false;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _invisibleTimer)
        {
            transform.GetComponentInChildren<BoxCollider>().isTrigger = true;
            Destroy(this);
        }

    }
}