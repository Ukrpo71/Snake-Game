using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy jumpTrigger when the last body part jumped
        if (other.gameObject.transform.parent.TryGetComponent(out SnakeBody body))
        {
            Debug.Log(body._bodyParts.Count);

            if (body._bodyParts.Count == 0)
                Destroy(gameObject);
        }

        //Destroy jumpTrigger when the player jumped and there is no body
        if (!other.CompareTag("Floor") && !other.CompareTag("Body"))
            if (other.gameObject.transform.parent.TryGetComponent(out PlayerController player))
            {
                if (player._bodyParts.Count == 0)
                    Destroy(gameObject);
            }
    }
}
