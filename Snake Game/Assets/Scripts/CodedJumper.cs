using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodedJumper : MonoBehaviour
{
    [SerializeField]
    private float _jumpHeight;
    [SerializeField]
    private float _speedOfJump;

    private bool _onGround;
    private bool _jumpTriggered;
    private bool _reachedHeight;

    void Start()
    {
        
    }

    void Update()
    {
        if (_jumpTriggered)
        {
            if (transform.position.y < _jumpHeight && _reachedHeight == false)
            {
                transform.position += new Vector3(0,_speedOfJump);
            }
            else
            {
                _reachedHeight = true;
                transform.position -= new Vector3(0, _speedOfJump);
            }
        }
    }

    public void Jump()
    {
        _jumpTriggered = true;
        _reachedHeight = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out JumpTrigger jumpTrigger) && _jumpTriggered == false)
        {
            Jump();
            Debug.Log(gameObject.name + " jumped, " + _jumpTriggered);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            _jumpTriggered = false;
        }
    }
}
