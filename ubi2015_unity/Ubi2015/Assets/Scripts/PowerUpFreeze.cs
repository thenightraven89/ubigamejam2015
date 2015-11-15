using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpFreeze : MonoBehaviour
{
    public float duration;
    private List<PlayerController> affectedMovers;
    public GameObject freezeEffect;

    void Start()
    {
        affectedMovers = new List<PlayerController>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerController goodOne = col.transform.parent.GetComponent<PlayerController>();
            PlayerController[] all = FindObjectsOfType<PlayerController>();
            Debug.Log(all.Length);
            foreach (var mv in all)
            {
                if (mv.GetInstanceID() != goodOne.GetInstanceID())
                {
                    mv.speed = 1f;
                    Debug.Log("Disabled " + mv.name);
                    affectedMovers.Add(mv);

                    var fx = Instantiate(freezeEffect) as GameObject;
                    fx.name = "fx";
                    fx.transform.parent = mv.transform;
                    fx.transform.localPosition = Vector3.zero;
                    fx.transform.localRotation = Quaternion.identity;
                    
                }
            }

            Destroy(transform.GetChild(0).gameObject);
            StartCoroutine(DelayDestroy());
        }
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(duration);
        foreach (var mv in affectedMovers)
        {
            mv.speed = 8f;
            Destroy(mv.transform.FindChild("fx").gameObject);
        }
        Destroy(gameObject);
    }
}
