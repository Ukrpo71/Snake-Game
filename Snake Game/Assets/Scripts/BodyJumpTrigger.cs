using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyJumpTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            player.JumpOverSelf();
        }
    }
}
