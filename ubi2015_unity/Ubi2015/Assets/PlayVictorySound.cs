using UnityEngine;
using System.Collections;

public class PlayVictorySound : MonoBehaviour {

    public AudioSource source;

    void OnEnable()
    {
        StartCoroutine(DelaySound());
    }

    IEnumerator DelaySound()
    {
        yield return new WaitForSeconds(0.5f);

        source.Play();
    }
}

