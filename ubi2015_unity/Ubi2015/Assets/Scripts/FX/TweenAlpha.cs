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
        var rend = target.GetComponentInChildren<Renderer>();
        Debug.Log(rend);
        Material mat = rend.material;
        mat.SetFloat("_Mode", 2f);
        tweener = mat.DOFade(0f, duration);
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
        Material mat = Target.GetComponentInChildren<Renderer>().material;
        mat.SetFloat("_Mode", 0f);
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1);
    }
}
