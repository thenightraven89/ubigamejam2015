using UnityEngine;

public class Cleanup : MonoBehaviour
{
    private const int MAX_LIFE = 5;
    private const int MAX_PLAYERS = 2;

    void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            ResetStats();
        }
    }

    void OnApplicationQuit()
    {
        ResetStats();
    }

    void ResetStats()
    {
        PlayerPrefs.SetInt("playerCount", MAX_PLAYERS);
        PlayerPrefs.SetInt("Player1", MAX_LIFE);
        PlayerPrefs.SetInt("Player2", MAX_LIFE);
        PlayerPrefs.SetInt("Player3", MAX_LIFE);
        PlayerPrefs.SetInt("Player4", MAX_LIFE);
    }
}