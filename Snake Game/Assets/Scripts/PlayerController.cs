using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private VariableJoystick _joystick;

    [SerializeField] private GameObject _bodyPrefab;
    [SerializeField] private GameObject _jumpTriggerPrefab;
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private float _walkSpeed = 5;
    [SerializeField] private float _runSpeed = 10;
    private float _moveSpeed;

    [SerializeField] private int _gap = 3;

    [SerializeField] private GameObject _popUpText;

    private Vector3 _input;

    public List<GameObject> _bodyParts = new List<GameObject>();
    public List<Vector3> _positionHistory = new List<Vector3>();

    private Spawner _spawner;

    private Rigidbody _rb;


    private bool _isRotating;
    private float _timer;
    [SerializeField] private float _timeToTurn;

    private bool _isRunning;

    [SerializeField] private UnityEvent _playerLost;

    void Start()
    {
        _spawner = FindObjectOfType<Spawner>();
        _rb = GetComponent<Rigidbody>();

        _moveSpeed = _walkSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnJumpTrigger();
        }
    }
    private void FixedUpdate()
    {
        CheckBounds();
        if (_gameManager.CurrentGameState == GameState.Playing)
        {
            GatherInput();
            Look();
            Move();
        }
        else if (_gameManager.CurrentGameState == GameState.GameLost)
        {
            RotateAround();
        }
    }

    private void RotateAround()
    {
        transform.Rotate(new Vector3(0, 5, 0));
        if (_bodyParts.Count > 0)
        {
            foreach (var body in _bodyParts)
            {
                body.transform.Rotate(new Vector3(0, 5, 0));
            }
        }
    }

    private void GatherInput()
    {
        _input = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized;

        if (_input == Vector3.zero)
            _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if (_isRunning && _gameManager.GameOver == false)
            _moveSpeed = _runSpeed;
        else if (_isRunning == false && _gameManager.GameOver == false)
            _moveSpeed = _walkSpeed;

            
    }

    public void SpawnJumpTrigger()
    {
        Instantiate(_jumpTriggerPrefab, transform.position, Quaternion.identity);
    }

    public void ToggleWalk() => _isRunning = !_isRunning;

    public void StopAllMovement()
    {
        _moveSpeed = 0;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;
        foreach (var body in _bodyParts)
        {
            body.GetComponent<Rigidbody>().velocity = Vector3.zero;
            body.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void Move()
    {
        transform.position += transform.forward * _moveSpeed * Time.deltaTime;

        // ”даление ненужных позиций
        if (_positionHistory.Count > (_bodyParts.Count + 1) * _gap)
            _positionHistory.RemoveAt(_positionHistory.Count - 1);

        //—охранение позиции змейки
        _positionHistory.Insert(0, transform.position);

        int index = 1;
        foreach (var body in _bodyParts)
        {
            Vector3 point = _positionHistory[Mathf.Min(index * _gap, _positionHistory.Count - 1)];
            point.y = body.transform.position.y;
            Vector3 moveDirection = point - body.transform.position;
            body.transform.LookAt(point);
            body.transform.position = point;
            index++;
        }
    }

    void Look()
    {
        if (_input != Vector3.zero)
        {
            // _isRotating ограничивает поворот на 45 градусов раз в промежуток времени _timeToTurn
            if (_isRotating == false)
            {
                // "ѕоворачиваем" переменную _input на 45 градусов, чтобы изометрическое движение было интуиивно
                // пон€тно. Ќажима€ на "верх" игрок движетс€ "вверх".
                var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
                var skewedInput = matrix.MultiplyPoint3x4(_input);

                // ѕововрачиваем игрока в сторону, которую мы указали
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

    public void CheckBounds()
    {
        if (transform.position.x < -9.8f || transform.position.x > 9.8f || transform.position.z > 9.8f || transform.position.z < -9.8f)
        {
            Debug.Log("Game Over!");
            _gameManager.GameOver = true;
            _moveSpeed = 0;
            StopAllMovement();
        }
        
    }

    public void Grow()
    {
        PopUpMultiplierText();

        _gameManager.PlayerAte();

        for (int i = 1; i < _gameManager.Multiplier; i++)
        {
            var body = Instantiate(_bodyPrefab);
            var spawnPosition = _positionHistory[_positionHistory.Count - i];
            spawnPosition.y = 0.25f;
            body.transform.position = spawnPosition;

            _bodyParts.Add(body);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.transform.parent != null && collision.collider.transform.parent.CompareTag("Body"))
        if (collision.gameObject.CompareTag("Body") || collision.gameObject.CompareTag("Wall"))
        {
            _gameManager.GameOver = true;

            _playerLost.Invoke();
        }
    }

    private void PopUpMultiplierText()
    {
        var body = Instantiate(_popUpText, transform.position, Quaternion.identity);
        body.GetComponent<TextMeshPro>().text = "x" + _gameManager.Multiplier;
    }



}
