using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    private List<GameObject> _bodyParts = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (_bodyParts.Count != 0)
        {
            if (other.gameObject == _bodyParts[_bodyParts.Count - 1])
            {
                Destroy(gameObject);
            }
        }

        //Destroy jumpTrigger when the player jumped and there is no body
        if (!other.CompareTag("Floor") && !other.CompareTag("Body"))
            if (other.gameObject.TryGetComponent(out PlayerController player))
            {
                if (player._bodyParts.Count == 0)
                    Destroy(gameObject);
                _bodyParts = player._bodyParts;
            }

        //Destroy jumpTrigger when the last body part jumped




        /*
        if (other.gameObject.TryGetComponent(out SnakeBody body))
        {
            Debug.Log(body._bodyParts.Count);

            if (body._bodyParts.Count == 0)
                Destroy(gameObject);
        }*/

        
    }
}
