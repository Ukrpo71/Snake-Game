using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField]
    private float _movingSpeed = 10f;
    [SerializeField]
    private float _rotationSpeed = 180f;
    [SerializeField]
    private int _gap = 3;

    [SerializeField]
    private GameObject _bodyPrefab;

    private List<GameObject> _bodyParts = new List<GameObject>();
    private List<Vector3> _positionHistory = new List<Vector3>();
    void Start()
    {
        /*GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        */
    }

    void Update()
    {
        transform.position += transform.forward * _movingSpeed * Time.deltaTime;


        

        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDirection * _rotationSpeed * Time.deltaTime);

        //Store positions
        _positionHistory.Insert(0, transform.position);

        int index = 0;
        foreach (var body in _bodyParts)
        {
            Vector3 point = _positionHistory[Mathf.Min(index * _gap, _positionHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * _movingSpeed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
        }
    }

    private void GrowSnake()
    {
        var body = Instantiate(_bodyPrefab);
        _bodyParts.Add(body);
    }
}
