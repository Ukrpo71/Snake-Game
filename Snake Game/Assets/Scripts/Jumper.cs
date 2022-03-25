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


    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _timer = 0;
    }

    void FixedUpdate()
    {
        if (_isJumping && _isInTheAir)
        {
            _timer += Time.deltaTime;
            if (_timer >= _jumpingTime || transform.position.y > _jumpHeight)
            {
                //Debug.Log(gameObject.name + " is in the air");
                _rb.velocity = new Vector3(0, 0, 0);
                _rb.AddForce(Vector3.down * _jumpForce, ForceMode.Impulse);
                _isInTheAir = false;
                _timer = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out JumpTrigger jumpTrigger) && _isJumping == false)
        {
            Jump();
            //Debug.Log(gameObject.name + " jumped, " + _isJumping);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isJumping && collision.gameObject.name == "Floor")
        {
            _isJumping = false;
            _rb.velocity = Vector3.zero;
        }
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _isJumping = true;
        _isInTheAir = true;
    }

}
