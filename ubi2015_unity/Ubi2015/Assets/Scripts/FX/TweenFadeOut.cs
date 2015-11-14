using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TweenFadeOut : TweenEffectBase
{
    public float delay;
    public override void Play(Transform target)
    {
        Material mat = GetComponentInChildren<Renderer>().material;
        DOTween.To(() => mat.GetColor("_TintColor"), c => mat.SetColor("_TintColor", c),
                        new Color(mat.GetColor("_TintColor").r, mat.GetColor("_TintColor").g, mat.GetColor("_TintColor").b, 0f), duration).SetDelay(delay).OnComplete(new TweenCallback(Destroy));
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
