using TMPro;
using UnityEngine;

public class GameStateGame : GameState
{
    public GameObject gameUI;
    [SerializeField] private TextMeshProUGUI fishCount;
    [SerializeField] private TextMeshProUGUI scoreCount;

    public override void Construct()
    {
        GameManager.Instance.motor.ResumePlayer();
    
        GameManager.Instance.ChangeCamera(GameCamera.Game);

        GameStat.Instance.onCollectFish += onCollectFish;
        GameStat.Instance.onScoreChange += onScoreChange;

        gameUI.SetActive(true);
       

    
    }

    private void onCollectFish(int amnCollected)
    {
        fishCount.text =amnCollected.ToString("000");

    }
    private void onScoreChange(float score)
    {
        scoreCount.text = score.ToString("000000");
    }
    public override void Destruct()
    {
        gameUI.SetActive(false);
        GameStat.Instance.onCollectFish -= onCollectFish;
        GameStat.Instance.onScoreChange -= onScoreChange;
    }

    public override void UpdateState()
    {
        GameManager.Instance.worldGeneration.ScanPosition();
        GameManager.Instance.sceneChunkGeneration.ScanPosition();
    }

}
