using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public string playerId;
    private int cachedScore = 0;
    
    public void Set(int score)
    {
        if (score != cachedScore)
        {
            cachedScore = score;
            GetComponent<Text>().text = score.ToString();
        }
    }
}