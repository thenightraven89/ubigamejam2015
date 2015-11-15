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
            cachedScore = Mathf.Clamp(score, 0, 11);
            var donuts = GetComponentsInChildren<Donut>(true);

            for (int i = 0; i < donuts.Length; i++)
            {
                donuts[i].gameObject.SetActive(i < cachedScore);
            }
        }
    }
}