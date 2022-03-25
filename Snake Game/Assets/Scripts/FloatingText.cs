using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy = 1f;
    [SerializeField] private Vector3 _offset = new Vector3(0.3f,0.2f,0);
    void Start()
    {
        transform.position += _offset;
        transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        Destroy(gameObject, _timeToDestroy);
    }

    void Update()
    {
        
    }
}
