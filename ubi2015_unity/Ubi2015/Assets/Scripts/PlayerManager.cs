using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    private Dictionary<string, PlayerScore> lifeLabels;
    private Dictionary<string, PlayerController> players;
    public bool getPlayersFromInspector = false;
    public GameObject[] playerGOs;
    public GameObject[] lifeCounters;

    private bool[] isPlaying = new bool[4] { false, false, false, false };

    private void Awake()
    {
        if (!getPlayersFromInspector)
        {
            for (int i = 0; i < 4; i++)
            {
                var pstring = "player" + (i + 1).ToString();
                isPlaying[i] = (PlayerPrefs.GetInt(pstring) == 1) ? true : false;
            }
        }

        for (int i = 0; i < playerGOs.Length; i++)
        {
            if (isPlaying[i])
            {
                playerGOs[i].SetActive(true);
                lifeCounters[i].SetActive(true);
            }
            else
            {
                playerGOs[i].SetActive(false);
                lifeCounters[i].SetActive(false);
            }
        }

        var allPlayers = GameObject.FindObjectsOfType<PlayerController>();
        players = new Dictionary<string, PlayerController>();
        for (int i = 0; i < allPlayers.Length; i++)
        {
            players.Add(allPlayers[i].gameObject.name, allPlayers[i]);
        }

        var allLifeLabels = GameObject.FindObjectsOfType<PlayerScore>();
        lifeLabels = new Dictionary<string, PlayerScore>();
        for (int i = 0; i < allLifeLabels.Length; i++)
        {
            lifeLabels.Add(allLifeLabels[i].playerId, allLifeLabels[i]);
        }

        foreach (var player in players)
        {
            lifeLabels[player.Key].Set(player.Value.life);
        }
    }

    private void Update()
    {
        var playerCount = 0;
        var lastLivingPlayer = "";

        foreach (var player in players)
        {
            var life = player.Value.life;
            lifeLabels[player.Key].Set(life);
            if (life > 0)
            {
                playerCount++;
                lastLivingPlayer = player.Key;
            }
        }

        if (playerCount == 1)
        {
            PlayerPrefs.SetString("winner", lastLivingPlayer);
            StartCoroutine(DelayEnd());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator DelayEnd()
    {
        yield return new WaitForSeconds(2);
        Application.LoadLevel("EndScene");
    }


}