using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    [SerializeField]
    private int _gap = 5;

    private int _speed = 5;

    public List<GameObject> _bodyParts = new List<GameObject>();
    public List<Vector3> _positionHistory = new List<Vector3>();

    private Spawner _spawner;

    void Start()
    {
        _gap = 3;
    }

    void Update()
    {
        Move();

        if (transform.position.y > 0.5)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            
        }
    }

    void Move()
    {
        // Удаление ненужных позиций
        if (_positionHistory.Count > (_bodyParts.Count + 1) * _gap)
            _positionHistory.RemoveAt(_positionHistory.Count - 1);

        //Сохранение позиции змейки
        _positionHistory.Insert(0, transform.position);



        /*int index = 1;
        foreach (var body in _bodyParts)
        {
            Vector3 point = _positionHistory[Mathf.Min(index * _gap, _positionHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * (_speed - 1) * Time.deltaTime;
            //body.transform.position = point;
            body.transform.LookAt(point);
            index++;
        }
        */


        if (_bodyParts.Count > 0)
        {
            var body = _bodyParts[0];
            Vector3 point = _positionHistory[Mathf.Min(_gap, _positionHistory.Count - 1)];
            point.y = 0.5f;
            Vector3 moveDirection = point - body.transform.position;
            moveDirection.y = 0;
            body.transform.position += moveDirection * (_speed) * Time.deltaTime;
            //body.transform.position = point;
            body.transform.LookAt(point);
        }

    }

    public void AddBody(GameObject body)
    {
        _bodyParts.Add(body);
    }

}
