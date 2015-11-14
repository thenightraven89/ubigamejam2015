using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public string playerId;

    private void Start()
    {
        Load();
    }

    public void Load()
    {
        GetComponent<Text>().text = PlayerPrefs.GetInt(playerId).ToString();
    }
}