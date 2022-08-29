using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void LoadLevel(int index)
    {
        string level = "Level" + index;
        SceneManager.LoadScene(level);
    }
}
