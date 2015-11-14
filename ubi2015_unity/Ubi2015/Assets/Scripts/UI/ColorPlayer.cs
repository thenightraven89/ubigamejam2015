using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorPlayer : MonoBehaviour {

    public Color[] colors;
    public RawImage[] playerImages;

    private int activePlayers = 0;

    public GameObject playerSelection;
    public GameObject difficultySelection;

    public enum MainMenuStates { selectingCharacter, selectingDifficulty };
    public MainMenuStates currentState = MainMenuStates.selectingCharacter;

    private KeyCode difficultyKey;
    private KeyCode enterGameKey;
    private KeyCode startGameKey;

    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            enterGameKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + (i + 1).ToString() + "Button0");

            startGameKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + (i + 1).ToString() + "Button7");

            if (Input.GetKeyDown(startGameKey))
            {
                MoveToDifficulty();
            }

            if (currentState == MainMenuStates.selectingCharacter)
            {
                if (Input.GetKeyDown(enterGameKey))
                {
                    activePlayers++;
                    playerImages[i].color = colors[i];
                }  
            }
            else if (currentState == MainMenuStates.selectingDifficulty)
            {
                for (int j = 0; j < 4; j++)
                {
                    difficultyKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick" + (i + 1).ToString() + "Button" + j.ToString());

                    if (Input.GetKeyDown(difficultyKey))
                    {
                        SetDifficulty(j);
                    }  
                }
            } 
        }           
    }

    void MoveToDifficulty()
    {
        playerSelection.SetActive(false);
        difficultySelection.SetActive(true);
        currentState = MainMenuStates.selectingDifficulty;
        PlayerPrefs.SetInt("numberOfPlayers", activePlayers);
    }

    void SetDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt("difficulty", difficulty);
        Application.LoadLevel("Main");
    }
}
