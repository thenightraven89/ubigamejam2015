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

    private Vector3 camPos;
    public override void Play(Transform target)
    {
        base.Play(target);
        camPos = mainCam.transform.position;
        tweener = DOTween.Shake(() => mainCam.transform.position, x => mainCam.transform.position = x,
           duration, strength, vibrato, randomness).OnComplete(new TweenCallback(ResetCamera));
        if (loop)
        {
            tweener.SetLoops(-1);
        }
    }

    private void ResetCamera()
    {
        mainCam.transform.DOMove(new Vector3(0f, 32.79f, -21.41f), 0.1f);
    }

    public override void Stop(float fadeOutTime)
    {
        tweener.Kill();
    }
    

}
