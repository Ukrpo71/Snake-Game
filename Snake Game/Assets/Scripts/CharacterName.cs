using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterName : MonoBehaviour
{
    [SerializeField] private Text _nameText;
    public void ChangeText(string text)
    {
        _nameText.text = text;
    }
}
