using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    private Rigidbody _rb;
    public bool _isJumping;
    public bool _isInTheAir;

    [SerializeField] private float _jumpHeight;

    [SerializeField]
    private float _jumpForce = 5;

    [SerializeField]
    private float _jumpingTime;
    private float _timer;

    public Vector3 velocity;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _timer = 0;
    }

    void FixedUpdate()
    {
        velocity = _rb.velocity;
        if (_isJumping)
        {
            _timer += Time.deltaTime;
            if (_timer >= _jumpingTime || transform.position.y > _jumpHeight)
            {
                _rb.velocity = new Vector3(0, 0, 0);
                _rb.AddForce(Vector3.down * _jumpForce, ForceMode.Impulse);
                _isInTheAir = true;
                _timer = 0;
            }
        }
        else if (_isInTheAir == false)
        {
            _rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out JumpTrigger jumpTrigger) && _isJumping == false)
        {
            Jump();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isInTheAir && collision.gameObject.CompareTag("Floor"))
        {
            _isJumping = false;
            _isInTheAir = false;
            _rb.velocity = new Vector3(0,0,0);
            if (!gameObject.CompareTag("Body"))
                GetComponent<PlayerController>().LandedJump();
        }
    }

    private void Jump()
    {
        if (gameObject.CompareTag("Body"))
            transform.GetComponentInChildren<BoxCollider>().isTrigger = false;
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _isJumping = true;
    }

}
