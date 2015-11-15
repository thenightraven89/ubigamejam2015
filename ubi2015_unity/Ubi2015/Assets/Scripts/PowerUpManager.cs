using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{
    public List<GameObject> powerUpList;
    public List<float> timeToSpawn;

    void Awake()
    {
        for (int i = 0; i < timeToSpawn.Count; i++)
        {
            StartCoroutine(WaitToSpawn(i));
        }
    }

    IEnumerator WaitToSpawn(int index)
    {
        float timeToWait = timeToSpawn[index] + Random.Range(-timeToSpawn[index] / 4, timeToSpawn[index] / 4);
        yield return new WaitForSeconds(timeToWait);
        Spawn(index);
    }

    void Spawn(int index)
    {
        Transform powerUpTransform = (Instantiate(powerUpList[index]) as GameObject).transform;

        bool areaOk = false;
        int randX = 0;
        int randZ = 0;
        RaycastHit hit;

        while (!areaOk)
        {
            randX = UnityEngine.Random.Range(-15, 16);
            randZ = UnityEngine.Random.Range(-15, 16);
            var cast = Physics.Raycast(new Vector3(randX, 100, randZ), Vector3.down, out hit);
            if (cast)
            {
                if (!hit.transform.CompareTag("Player") &&
                    !hit.transform.CompareTag("PowerupTime") && !hit.transform.CompareTag("Powerup"))
                {
                    areaOk = true;
                }
            }
            else
            {
                areaOk = true;
            }
        }

        powerUpTransform.position = new Vector3(randX, 0, randZ);
        
        StartCoroutine(WaitToSpawn(index));
    }
}
