using System.Collections.Generic;
using UnityEngine;

public class HatLogic : MonoBehaviour
{
    [SerializeField] private Transform hatContainer;
    private List<GameObject> hatModels=new List<GameObject>();
    public Hat[] hats;
    

    private void Start()
    {
        hats = Resources.LoadAll<Hat>("Hat");
        SpawnHats();
        SelectHat(SaveManager.Instance.save.CurrentHatIndex);

    }

    private void SpawnHats()
    {
        for (int i = 0; i < hats.Length; i++)
        {
            
            hatModels.Add(Instantiate(hats[i].Model, hatContainer));
        }
    }

    public void DisableAllHats()
    {
        Debug.Log("here");
        for(int i=0;i<hatModels.Count;i++)
        {
            hatModels[i].SetActive(false);
        }
    }

    public void SelectHat(int i)
    {
        DisableAllHats();
        hatModels[i].SetActive(true);

    }
}
