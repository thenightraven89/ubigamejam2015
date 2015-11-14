using UnityEngine;
using UnityEngine.UI;

public class VictoryText : MonoBehaviour
{
    public Material P1, P2, P3, P4;
    
    private void Start()
    {
        var textComp = GetComponent<Text>();

        if (PlayerPrefs.GetInt("Player1") > 0)
        {
            textComp.color = P1.color;
            textComp.text = "Player 1 wins";
        }

        if (PlayerPrefs.GetInt("Player2") > 0)
        {
            textComp.color = P2.color;
            textComp.text = "Player 2 wins";
        }

        if (PlayerPrefs.GetInt("Player3") > 0)
        {
            textComp.color = P3.color;
            textComp.text = "Player 3 wins";
        }

        if (PlayerPrefs.GetInt("Player4") > 0)
        {
            textComp.color = P4.color;
            textComp.text = "Player 4 wins";
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Application.LoadLevel("Main");
        }
    }
}