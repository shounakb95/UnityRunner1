using TMPro;
using UnityEngine;

public class GameStateInit : GameState
{

    public GameObject menuUI;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI FishCountText;

    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Init);

        highScoreText.text = "HighScore :"+SaveManager.Instance.save.Highscore.ToString();
        FishCountText.text = "Fish :"+SaveManager.Instance.save.Fish.ToString();

        menuUI.SetActive(true);
    }
    

    public void OnPlayClick()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
        GameStat.Instance.ResetSession();

        
    }

    public override void Destruct()
    {
        menuUI.SetActive(false);
    }

    public void OnShopClick()
    {
        brain.ChangeState(GetComponent<GameStateShop>());
        Debug.Log("Shop button");
    }
   
}
