using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveManager : MonoBehaviour
{
   public static SaveManager Instance { get { return instance; } }
    public static SaveManager instance;


    //Fields
    public SaveState save;
    private const string saveFileName = "data.ss";
    private BinaryFormatter formatter;

    public Action<SaveState> OnLoad;
    public Action<SaveState> OnSave;


    private void Awake()
    {
        instance = this;
        formatter = new BinaryFormatter();
        //try and load previous save state
        Load();
    }
    public void Load()
    {
        try
        {
            FileStream file = new FileStream(Application.persistentDataPath+saveFileName, FileMode.Open, FileAccess.Read);
            save = formatter.Deserialize(file) as SaveState;//deserialze
            file.Close();
            OnLoad?.Invoke(save);
        }
        catch
        {
            Debug.Log("savefile not found!!!");
            Save();
        }
    }
    public void Save()
    {
        //if there is no previous state create new one
        if (save == null)
            save = new SaveState();

        // set the time of saving
        save.LastSaveTime = DateTime.Now;

        //open a file
        FileStream file = new FileStream(Application.persistentDataPath + saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
        formatter.Serialize(file, save);
        file.Close();

            OnSave?.Invoke(save);

    }
}
