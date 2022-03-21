using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HoldDownButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PlayerController _player;
    public void OnPointerDown(PointerEventData eventData)
    {
        _player.ToggleWalk();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _player.ToggleWalk();
    }
}
