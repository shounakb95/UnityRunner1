using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStateDeath : GameState

{
    public GameObject deathUI;
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI fishScore;
    [SerializeField] private TextMeshProUGUI currentFish;

    //circle completion field 

    [SerializeField] private Image completionCircle;

    public float timeToDecision = 2.5f;
    private float deathTime;

    public override void Construct()
    {
        GameManager.Instance.motor.PausePlayer();

        deathTime = Time.time;
        deathUI.SetActive(true);
        completionCircle.gameObject.SetActive(true);

        if (SaveManager.Instance.save.Highscore < (int)GameStat.Instance.score)
            SaveManager.Instance.save.Highscore = (int)GameStat.Instance.score;

        SaveManager.Instance.save.Fish += GameStat.Instance.fishCollectedThisSession;
        SaveManager.Instance.Save();

        highScore.text = "HighScore : "+SaveManager.instance.save.Highscore;
        currentScore.text = GameStat.Instance.score.ToString("0000000");
        fishScore.text = "Total : "+SaveManager.Instance.save.Fish;
        currentFish.text = GameStat.Instance.fishCollectedThisSession.ToString("0000000");
    }

    public override void Destruct()
    {
        deathUI.SetActive(false);
    }
    public override void UpdateState()
    {
        float ratio = (Time.time - deathTime)/timeToDecision;
        completionCircle.color = Color.Lerp(Color.green, Color.red,ratio);
        completionCircle.fillAmount = 1 - ratio;

        if(ratio>1)
        {
            completionCircle.gameObject.SetActive(false);
        }

    }
    public void ToMenu()
    {
        

        brain.ChangeState(GetComponent<GameStateInit>());
        GameManager.Instance.motor.ResetPlayer();
        GameManager.Instance.worldGeneration.ResetWorld();
        GameManager.Instance.sceneChunkGeneration.ResetWorld();

        // prior to saving set the scores

        

    }
    public void ResumeGame()
    {
        
        brain.ChangeState(GetComponent<GameStateGame>());
        GameManager.Instance.motor.RespawnPlayer();


    }
}
