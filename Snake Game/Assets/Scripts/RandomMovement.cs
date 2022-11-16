using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    [SerializeField]
    private float _range;

    private NavMeshAgent _agent;

    private float _randomX;
    private float _randomZ;

    private Vector3 _randomVector;
    private Vector3 _movementDirection;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(FindNextTarget());
    }

    void Update()
    {
        if ( _agent.remainingDistance < _agent.stoppingDistance)
        {
            _agent.SetDestination(FindNextTarget());
        }

    }

    private Vector3 FindNextTarget()
    {
        _randomX = Random.Range(-_range, _range);
        _randomZ = Random.Range(-_range, _range);
        _randomVector = new Vector3(transform.position.x + _randomX, transform.position.y, transform.position.z + _randomZ);

        _movementDirection = _randomVector - transform.position;

        if (Physics.Raycast(transform.position, _movementDirection, out RaycastHit hitInfo, _range))
        {
            if (hitInfo.collider.CompareTag("Wall") || hitInfo.collider.CompareTag("Obstacle"))
            {
                return FindNextTarget();
            }
            else
            {
                return _randomVector;
            }

        }
        return _randomVector;
    }
}
