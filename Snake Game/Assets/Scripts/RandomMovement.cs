using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    [SerializeField]
    private float _range;

    private NavMeshAgent _agent;


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
        var randomX = Random.Range(-_range, _range);
        var randomZ = Random.Range(-_range, _range);
        var randomVector = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        var movementDirection = randomVector - transform.position;

        if (Physics.Raycast(transform.position, movementDirection, out RaycastHit hitInfo, _range))
        {
            if (hitInfo.collider.CompareTag("Wall") || hitInfo.collider.CompareTag("Obstacle"))
            {
                Debug.Log("hit a wall");
                return FindNextTarget();
            }
            else
            {
                Debug.Log("Hit something else");
                return randomVector;
            }

        }
        Debug.Log("Didn't hit anything");
        return randomVector;
    }
}
