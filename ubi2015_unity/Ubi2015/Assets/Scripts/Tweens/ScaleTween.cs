using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ScaleTween : MonoBehaviour {
    public Vector3 scaleTarget;
    public Ease easeType = Ease.Linear;
    public LoopType loopType = LoopType.Restart;
    public float tweenTime = 1f;
    public int nrLoops = -1;
    // Use this for initialization
    void Start()
    {
        transform.DOScale(scaleTarget, tweenTime).SetLoops(nrLoops, loopType).SetEase(easeType);
    }
}
