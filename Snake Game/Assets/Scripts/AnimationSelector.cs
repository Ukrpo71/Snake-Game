using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSelector : MonoBehaviour
{
    [SerializeField] private AnimationToChose _animation;
    private void OnEnable()
    {
        var anim = GetComponent<Animator>();
        anim.Play(_animation.ToString());
    }

    public enum AnimationToChose
    {
        Running,
        Idle,
        SillyDancing,
        RumbaDancing,
        HipHop
    }
}
