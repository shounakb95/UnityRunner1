using UnityEngine;
using System;

[System.Serializable]
public class SaveState
{
    [NonSerialized]private const int HAT_COUNT = 16;
    public int Highscore { set; get; }
    public int Fish { set; get; }
    public DateTime LastSaveTime { set; get; }

    public int CurrentHatIndex { set; get; }

    public byte[] unlockedHatFlag { set; get; }

    public SaveState()
    {
        Fish = 0;
        Highscore = 0;
        LastSaveTime = DateTime.Now;
        CurrentHatIndex = 0;
        unlockedHatFlag = new byte[HAT_COUNT];
        unlockedHatFlag[0] = 1;
        unlockedHatFlag[1] = 1;
    }
}

