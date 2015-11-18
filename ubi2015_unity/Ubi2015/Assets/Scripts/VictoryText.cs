using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class VictoryText : MonoBehaviour
{
    public Material P1, P2, P3, P4;
    private KeyCode startGameKey;

    private void Start()
    {
        var textComp = GetComponent<Text>();

        Dictionary<string, Material> mats = new Dictionary<string, Material>()
        {
            {"Player1", P1 },
            {"Player2", P2 },
            {"Player3", P3 },
            {"Player4", P4 }
        };

        var winner = PlayerPrefs.GetString("winner");

        textComp.text = winner + " wins!";
        textComp.color = mats[winner].color;
    }

    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            startGameKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + (i + 1).ToString() + "Button7");
            
            if (Input.GetKeyDown(startGameKey) ||
                Input.GetKeyDown(KeyCode.Space))
            {
                Application.LoadLevel("Menu");
            }
        }


    }
}