using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TweenAlpha : TweenEffectBase
{
    public int loops;
    Tweener tweener;

    public override void Play(Transform target)
    {
        base.Play(target);
        tweener = target.GetComponent<Renderer>().material.DOFade(0f, duration);
        if (loops != -1)
            loops *= 2;
        tweener.SetLoops(loops, LoopType.Yoyo);
        tweener.OnComplete(new TweenCallback(StopCallback));
    }

    private void StopCallback()
    {
        Stop(0f);
        Destroy(gameObject);
    }

    public override void Stop(float fadeOutTime)
    {
        tweener.Kill();
        Material mat = Target.GetComponent<Renderer>().material;
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1);
    }
}
