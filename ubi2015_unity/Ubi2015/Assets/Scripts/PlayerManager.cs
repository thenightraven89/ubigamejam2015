using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Dictionary<string, PlayerScore> lifeLabels;
    private Dictionary<string, PlayerController> players;

    private void Start()
    {
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
            Application.LoadLevel("EndScene");
        }
    }
}