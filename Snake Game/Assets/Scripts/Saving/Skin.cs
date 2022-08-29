using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skin : MonoBehaviour
{
    [SerializeField] private SkinData _skin;

    public bool IsUnlocked { get { return _skin.IsUnlocked; } }
    public float Cost { get { return _skin.Cost; } }
    public int UnlockingRequiremnt { get { return _skin.UnlockingRequirement; } }
    public int UnlockingRequirement { get { return _skin.UnlockingRequirement; } }
    public string Name { get { return _skin.NameForPlaceholder; } }
    public SkinData SkinData { get { return _skin; } }

    private void Awake()
    {
        if (_skin.Name == null)
            _skin.Name = gameObject.name;
    }

}
