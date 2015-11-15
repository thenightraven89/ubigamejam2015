using UnityEngine;

public class PowerUpLife : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.transform.parent.GetComponent<PlayerController>().life++;
            FXManager.Instance.PlayEffect("powerupsound", transform);
            GameObject.Destroy(gameObject);
        }
    }
}