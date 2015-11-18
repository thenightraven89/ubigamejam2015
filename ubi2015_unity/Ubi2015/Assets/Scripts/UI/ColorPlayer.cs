using UnityEngine;
using UnityEngine.UI;

public class ColorPlayer : MonoBehaviour
{

    public Color[] colors;
    public RawImage[] playerImages;

    private int activePlayers = 0;

    public GameObject playerSelection;
    public GameObject difficultySelection;

    public GameObject pressStart;

    private bool canContinue = false;

    private KeyCode difficultyKey;
    private KeyCode enterGameKey;
    private KeyCode startGameKey;

    public AudioSource buttonPush;

    private bool player1ready = false;
    private bool player2ready = false;
    private bool player3ready = false;
    private bool player4ready = false;

    private void StartGame()
    {
        PlayerPrefs.SetInt("player1", (player1ready == true) ? 1 : 0);
        PlayerPrefs.SetInt("player2", (player2ready == true) ? 1 : 0);
        PlayerPrefs.SetInt("player3", (player3ready == true) ? 1 : 0);
        PlayerPrefs.SetInt("player4", (player4ready == true) ? 1 : 0);

        Application.LoadLevel("Main");
    }

    private int GetActivePlayers()
    {
        var result = 0;
        if (player1ready) result++;
        if (player2ready) result++;
        if (player3ready) result++;
        if (player4ready) result++;

        return result;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player1ready = true;
            player2ready = true;
            player3ready = false;
            player4ready = false;
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            player1ready = true;
            playerImages[0].color = colors[0];
        }

        if (Input.GetKeyDown(KeyCode.Joystick2Button0))
        {
            player2ready = true;
            playerImages[1].color = colors[1];
        }

        if (Input.GetKeyDown(KeyCode.Joystick3Button0))
        {
            player3ready = true;
            playerImages[2].color = colors[2];
        }

        if (Input.GetKeyDown(KeyCode.Joystick4Button0))
        {
            player4ready = true;
            playerImages[3].color = colors[3];
        }

        if (GetActivePlayers() > 1)
        {
            pressStart.SetActive(true);
            if (Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                buttonPush.Play();
                StartGame();
            }
        }
    }
}