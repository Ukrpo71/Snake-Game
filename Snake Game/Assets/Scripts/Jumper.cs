using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _isJumping;
    private bool _isInTheAir;

    [SerializeField]
    private float _jumpForce = 5;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _jumpForce = 3;
    }

    void Update()
    {
        if (_isJumping && _isInTheAir && transform.position.y > 2f)
        {
            Debug.Log(gameObject.name + " is in the air");
            _rb.velocity = new Vector3(0, 0, 0);
            _rb.AddForce(Vector3.down * _jumpForce, ForceMode.Impulse);
            _isInTheAir = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out JumpTrigger jumpTrigger) && _isJumping == false)
        {
            Jump();
            Debug.Log(gameObject.name + " jumped, " + _isJumping);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isJumping && collision.gameObject.name == "Floor")
        {
            _isJumping = false;
        }
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _isJumping = true;
        _isInTheAir = true;
    }

}
