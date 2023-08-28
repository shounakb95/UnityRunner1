using UnityEngine;
using System;

[System.Serializable]
public class SaveState
{
    public int Highscore { set; get; }
    public int Fish { set; get; }
    public DateTime LastSaveTime { set; get; }

    public SaveState()
    {
        Fish = 0;
        Highscore = 0;
        LastSaveTime = DateTime.Now;
    }
}
