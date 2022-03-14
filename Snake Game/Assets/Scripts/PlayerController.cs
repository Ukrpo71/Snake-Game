using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _bodyPrefab;
    [SerializeField]
    private GameObject _jumpTriggerPrefab;

    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private float _turnSpeed = 360;

    [SerializeField]
    private float _jumpForce = 5;

    [SerializeField]
    private int _gap = 3;

    [SerializeField]
    private GameManager _gameManager;

    private Vector3 _input;

    public List<GameObject> _bodyParts = new List<GameObject>();
    public List<Vector3> _positionHistory = new List<Vector3>();

    private Spawner _spawner;

    private Rigidbody _rb;


    void Start()
    {
        _spawner = FindObjectOfType<Spawner>();
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GatherInput();
        Look();
        Move();



    }

    void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
            _speed *= 2;
        else if (Input.GetKeyUp(KeyCode.Space))
            _speed = 5;

        if (Input.GetKeyDown(KeyCode.C))
            Instantiate(_jumpTriggerPrefab, transform.position, Quaternion.identity);
    }

    void Move()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
        // Удаление ненужных позиций
        if (_positionHistory.Count > (_bodyParts.Count + 1) * _gap)
            _positionHistory.RemoveAt(_positionHistory.Count - 1);

        //Сохранение позиции змейки
        _positionHistory.Insert(0, transform.position);



        //int index = 1;
        /*foreach (var body in _bodyParts)
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

    void Look()
    {
        if (_input != Vector3.zero)
        {
            var relative = (transform.position + _input.ToIso()) - transform.position;
            var rotation = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation,rotation, _turnSpeed * Time.deltaTime);
        }
    }

    public void Grow()
    {
        _gameManager.DecreaseNumberOfFood();

        var body = Instantiate(_bodyPrefab);
        body.transform.position = _positionHistory[_positionHistory.Count - 1];

        if (_bodyParts.Count > 0)
            _bodyParts[_bodyParts.Count - 1].GetComponent<SnakeBody>().AddBody(body);
        
        _bodyParts.Add(body);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            if (other.gameObject.transform.parent.gameObject.TryGetComponent(out SnakeBody body))
                Debug.Log("Game Over!");
        }
    }



}
