using System;
using UnityEngine;

public class GameStat : MonoBehaviour
{
    public static GameStat Instance { get { return instance; } }
    public static GameStat instance;


    //Score
    public float score;
    public float highScore;
    public float distanceModifier = 1.5f;

    //Fish
    public int totalFish;
    public int fishCollectedThisSession;
    public float pointsPerFish = 10f;

    //internal cooldown
    private float lastScoreUpdate;
    private float scoreUpdateDelta = 0.2f;

    //Action
    public Action<int> onCollectFish;
    public Action<float> onScoreChange;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        float s = GameManager.Instance.motor.transform.position.z * distanceModifier;
        s += fishCollectedThisSession * pointsPerFish;

        if(s>score)
        {
            score = s;
            if (Time.time - lastScoreUpdate > scoreUpdateDelta)
            {
                lastScoreUpdate = Time.time;
                onScoreChange?.Invoke(score);
            }
        }


    }
    public void CollectFish()
    {
        fishCollectedThisSession++;
        onCollectFish?.Invoke(fishCollectedThisSession);
    }
    public void ResetSession()
    {
        score = 0;
        fishCollectedThisSession = 0;
        onScoreChange?.Invoke(score);
        onCollectFish?.Invoke(fishCollectedThisSession);
    }
}
