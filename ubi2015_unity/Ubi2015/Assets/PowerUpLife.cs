using UnityEngine;

public class PowerUpLife : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.transform.parent.GetComponent<PlayerController>().life++;
            GameObject.Destroy(gameObject);
        }
    }
}