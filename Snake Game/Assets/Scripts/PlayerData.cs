using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int selectedSkin;
    public int unlockedLevels;

    public override string ToString()
    {
        return "The player has selected skin #" + selectedSkin + " and has unlocked " + unlockedLevels + " levels.";
    }
}
