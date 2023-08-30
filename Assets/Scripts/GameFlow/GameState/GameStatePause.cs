using TMPro;
using UnityEngine;

public class GameStatePause : GameState
{
    public GameObject pauseUi;
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI fishScore;
    [SerializeField] private TextMeshProUGUI currentFish;

    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Game);
        pauseUi.SetActive(true);        
       

        highScore.text = "HighScore : " + SaveManager.instance.save.Highscore;
        currentScore.text = GameStat.Instance.score.ToString("0000000");
        fishScore.text = "Total : " + SaveManager.Instance.save.Fish;
        currentFish.text = GameStat.Instance.fishCollectedThisSession.ToString("0000000");
    }
    public void ResumePlayer()
    {
        Time.timeScale = 1;
        brain.ChangeState(GetComponent<GameStateGame>());
    }
    
    public override void Destruct()
    {
        pauseUi.SetActive(false);
    }
}
