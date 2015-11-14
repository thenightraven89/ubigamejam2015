using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private PlayerScore[] scores;

    void Awake()
    {
        instance = this;

        scores = GetComponentsInChildren<PlayerScore>();
    }

    public void UpdateScore(string player)
    {
        foreach (var score in scores)
        {
            if (score.playerId == player)
            {
                score.Load();
            }
        }
    }
}