using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelSelectionSetup : MonoBehaviour
{
    [SerializeField] private Transform _levelsParent;

    private void Start()
    {
        Load();
    }

    public void Save()
    {
        List<Level> levels = new List<Level>();
        foreach (LevelButton levelButton in _levelsParent.GetComponentsInChildren<LevelButton>())
            levels.Add(levelButton.Level);

        var dataPersist = FindObjectOfType<DataPersist>();
        dataPersist.PlayerData.Levels = levels;
        dataPersist.Save();
    }

    public void Load()
    {
        var dataPersist = FindObjectOfType<DataPersist>();
        foreach (LevelButton levelButton in _levelsParent.GetComponentsInChildren<LevelButton>())
        {
            var associatedLevel = dataPersist.PlayerData.Levels.FirstOrDefault(t => t.Number.ToString() == levelButton.name);
            levelButton.Setup(associatedLevel);
        }
    }
}
