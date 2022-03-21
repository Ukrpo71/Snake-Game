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


    private bool _isRotating;
    private float _timer;
    [SerializeField]
    private float _timeToTurn;

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
        //var dir = transform.position + transform.forward * _speed * 10 * Time.deltaTime;
        //Debug.DrawRay(transform.position, dir - transform.position, Color.yellow);
        
        transform.position += transform.forward * _speed * Time.deltaTime;

        //velocityForTesting = _rb.velocity;

        //Debug.Log(velocityForTesting);

        // �������� �������� �������
        //if (_positionHistory.Count > (_bodyParts.Count + 1) * _gap)
        //    _positionHistory.RemoveAt(_positionHistory.Count - 1);

        //���������� ������� ������
        _positionHistory.Insert(0, transform.position);



        
        int index = 1;
        foreach (var body in _bodyParts)
        {
            Vector3 point = _positionHistory[Mathf.Min(index * _gap, _positionHistory.Count - 1)];
            point.y = body.transform.position.y;
            Vector3 moveDirection = point - body.transform.position;
            //body.transform.position += moveDirection * _speed * Time.deltaTime;
            body.transform.LookAt(point);
            body.transform.position = point;
            index++;
        }
        

        /*if (_bodyParts.Count > 0)
        {
            var body = _bodyParts[0];
            Vector3 point = _positionHistory[Mathf.Min(_gap, _positionHistory.Count - 1)];
            //point.y = 0.5f;
            Vector3 moveDirection = point - body.transform.position;
            moveDirection.y = 0;
            //body.transform.position += moveDirection * (_speed) * Time.deltaTime;
            body.transform.LookAt(point);
            body.transform.position = point;
            
        }*/
    }

    void Look()
    {
        if (_input != Vector3.zero)
        {
            // _isRotating ������������ ������� �� 45 �������� ��� � ���������� ������� _timeToTurn
            if (_isRotating == false)
            {

                // "������������" ���������� _input �� 45 ��������, ����� �������������� �������� ���� ���������
                // �������. ������� �� "����" ����� �������� "�����".
                var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
                var skewedInput = matrix.MultiplyPoint3x4(_input);

                // ������������� ������ � �������, ������� �� �������
                var relative = (transform.position + skewedInput) - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relative, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 45);

                _isRotating = true;
            }
            else
            {
                _timer += Time.deltaTime;

                if (_timer > _timeToTurn)
                {
                    _timer = 0;
                    _isRotating = false;
                }
            }
        }
    }

    public void Grow()
    {
        _gameManager.DecreaseNumberOfFood();

        var body = Instantiate(_bodyPrefab);
        body.transform.position = _positionHistory[_positionHistory.Count - 1];

        //if (_bodyParts.Count > 0)
        //    _bodyParts[_bodyParts.Count - 1].GetComponent<SnakeBody>().AddBody(body);

        _bodyParts.Add(body);

    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.CompareTag("Body"))
        {
            Debug.Log("Game Over!");
            _speed = 0;
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Game Over!");
            _speed = 0;
        }

        Debug.Log(collision.gameObject.name);

        //if (collision.collider.transform.parent != null && collision.collider.transform.parent.CompareTag("Body"))
        if (collision.gameObject.CompareTag("Body"))
        {
            Debug.Log("Game Over!");
            _speed = 0;
        }
    }



}
