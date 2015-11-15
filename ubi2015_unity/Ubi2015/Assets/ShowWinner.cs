using UnityEngine;
using System.Collections;

public class ShowWinner : MonoBehaviour {

    public GameObject[] trains;

    void OnEnable() 
    {
        string winnerName = PlayerPrefs.GetString("winner");
        int nr = System.Convert.ToInt32(winnerName[winnerName.Length - 1].ToString());

        for (int i = 0; i < trains.Length; i++)
        {
            if (nr - 1 == i)
            {
                trains[i].SetActive(true);
            }
            else
            {
                trains[i].SetActive(false);
            }
        }
    }
}
