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

        Debug.Log("randomVector: " + randomVector);
        Debug.Log("transform:position: " + transform.position);
        Debug.Log("movementDirection: " + movementDirection);

        Debug.DrawRay(transform.position, movementDirection, Color.yellow);


        if (Physics.Raycast(transform.position, movementDirection, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.CompareTag("Wall"))
            {
                Debug.Log("Can't move here");
                FindNextTarget();
            }
            else
            {
                Debug.Log("Can Move Here");
                return movementDirection;
            }

        }

        Debug.Log("Can Move Here");
        return movementDirection;
    }
}
