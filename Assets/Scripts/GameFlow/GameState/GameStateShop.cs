using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStateShop : GameState
{

    public GameObject shopUI;
    public TextMeshProUGUI totalFish;
    public TextMeshProUGUI currentHatName;
    public HatLogic hatLogic;


    //shop item

    public GameObject hatPrefab;
    public Transform hatContainer;
    public Hat[] hats;
    public string last;
    

    private void Awake()
    {
        hats = Resources.LoadAll<Hat>("Hat/");
        PopulateShop();
        currentHatName.text = hats[SaveManager.Instance.save.CurrentHatIndex].ItemName;
        last = hats[SaveManager.Instance.save.CurrentHatIndex].ItemName;
    }
    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Shop);
        totalFish.text = SaveManager.Instance.save.Fish.ToString();
        shopUI.SetActive(true);
    }

    private void PopulateShop()
    {
        for(int i=0;i<hats.Length;i++)
        {
            int index = i;
            GameObject go=Instantiate(hatPrefab, hatContainer);
            //Button
            go.GetComponent<Button>().onClick.AddListener(() => OnHatClick(index));
            //Tgumbnail
            go.transform.GetChild(1).GetComponent<Image>().sprite = hats[index].Thumbnail;
            //Item Name
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = hats[index].ItemName;
            
            //price
            if(SaveManager.Instance.save.unlockedHatFlag[i]==0)
                 go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = hats[index].ItemPrice.ToString();
            else
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Bought";



        }
    }

    private void OnHatClick(int i)
    {
        
      if (SaveManager.Instance.save.unlockedHatFlag[i] == 1)
        {
            SaveManager.Instance.save.CurrentHatIndex = i;
            last = hats[i].ItemName;
            currentHatName.text = hats[i].ItemName;
            hatLogic.SelectHat(i);
            SaveManager.Instance.Save();
            
        
        
        }
        
        else if((int)hats[i].ItemPrice<=SaveManager.Instance.save.Fish)
        {
            
            SaveManager.Instance.save.Fish -= hats[i].ItemPrice;
            SaveManager.Instance.save.unlockedHatFlag[i] = 1;
            SaveManager.Instance.save.CurrentHatIndex = i;
            currentHatName.text = hats[i].ItemName;
            hatLogic.SelectHat(i);
            totalFish.text = SaveManager.Instance.save.Fish.ToString();
            SaveManager.Instance.Save();
            hatContainer.GetChild(i).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Bought";
            last = hats[i].ItemName;



        }
        else
        {
            Debug.Log("Fish not");
            
            StartCoroutine(SomeCoroutime());
            
        }
        
    }
    public override void Destruct()
    {
        shopUI.SetActive(false);
    }
    IEnumerator SomeCoroutime()
    {
        WaitForSeconds wait = new WaitForSeconds(2);
        
        for (int i = 1; i < 2; i++) 
        {
            Debug.Log(i);
            currentHatName.text = "Not enough Fish";
            yield return wait;
        }
        currentHatName.text = last;
    }
    
}
