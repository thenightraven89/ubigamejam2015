using UnityEngine;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Plugins.Options;

public class CameraShakeEffect : ImageEffectTween
{
    public float strength = 3f;
    public int vibrato = 10;
    public float randomness = 90f;
    public bool loop;
    DG.Tweening.Core.TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> tweener;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Play(Transform target)
    {
        base.Play(target);
        tweener = DOTween.Shake(() => mainCam.transform.position, x => mainCam.transform.position = x,
           duration, strength, vibrato, randomness);
        if (loop)
        {
            tweener.SetLoops(-1);
        }
    }

    public override void Stop(float fadeOutTime)
    {
        tweener.Kill();
    }
    

}
