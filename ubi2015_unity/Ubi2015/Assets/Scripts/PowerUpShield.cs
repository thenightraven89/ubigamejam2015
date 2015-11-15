using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpShield : MonoBehaviour
{
    public float duration;
    public PlayerController affectedPlayer;
    public GameObject shieldPrefab;
    private GameObject shieldInstance;
   
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            affectedPlayer = col.transform.parent.GetComponent<PlayerController>();
            if (affectedPlayer == null)
                return;
            affectedPlayer.hasShiled = true;
            shieldInstance = Instantiate(shieldPrefab, col.transform.position, col.transform.rotation) as GameObject;
            Debug.Log(shieldInstance);
            FXManager.Instance.PlayEffect("powerupsound", col.transform);
            shieldInstance.transform.parent = affectedPlayer.transform;
            Destroy(transform.GetChild(0).gameObject);
            StartCoroutine(DelayDestroy());
        }
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(duration);
        if(affectedPlayer != null) affectedPlayer.hasShiled = false;
        Destroy(shieldInstance);
        Destroy(gameObject);
    }
}
