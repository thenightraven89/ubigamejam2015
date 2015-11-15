using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TweenScale : MonoBehaviour {

    public float time = 2;
    public float delay = 0.5f;

    void OnEnable()
    {
        StartCoroutine(ApplyDelay());
    }

    IEnumerator ApplyDelay()
    {
        yield return new WaitForSeconds(delay);

        transform.DOScale(1, time).SetEase(Ease.OutBack);
    }
}
