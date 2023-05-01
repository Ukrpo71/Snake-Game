using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int SelectedSkin;
    public List<SkinData> Skins;
    public List<Animal> Animals;
    public List<Level> Levels;
    public bool TutorialFinished = false;
    public bool HomeScreenExplained = false;
    public bool NoAds = false;

    public PlayerData() { }
    public PlayerData(List<SkinData> unlockedLevels, int selectedSkin)
    {
        this.Skins = unlockedLevels;
        this.SelectedSkin = selectedSkin;
    }

}

[Serializable]
public class Level
{
    public int Number;
    public bool IsUnlocked;
    public bool IsFinished;
}