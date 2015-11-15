using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class TweenAlpha : TweenEffectBase
{
    public int loops;
    List<Tweener> tweener;
    public override void Play(Transform target)
    {
        base.Play(target);
        tweener = new List<Tweener>();
        
        if (target != null)
        {
            Renderer[] rend = target.GetComponentsInChildren<Renderer>();
            foreach (var x in rend)
            {
                if (!x.gameObject.name.Equals("ghost") &&
                    !x.gameObject.name.Equals("smoke"))
                {
                    Material mat = x.material;
                    x.material = mat;
                    mat.SetFloat("_Mode", 1f);
                    Tweener tw = DOTween.To(() => mat.GetColor("_Color"), c => mat.SetColor("_Color", c),
                                    new Color(mat.color.r, mat.color.g, mat.color.b, 0f), duration);
                    tweener.Add(tw);
                    if (loops != -1)
                        loops *= 2;
                    tw.SetLoops(loops, LoopType.Yoyo);
                }
            }

            tweener[0].OnComplete(new TweenCallback(StopCallback));
        }
    }

    private void StopCallback()
    {
        Stop(0f);
        Destroy(gameObject);
    }

    public override void Stop(float fadeOutTime)
    {
        if (Target == null)
            return;
        foreach (var tw in tweener)
        {
            tw.Kill();
        }
        Renderer[] rnds = Target.GetComponentsInChildren<Renderer>();

        foreach (var rnd in rnds)
        {
            if (!rnd.gameObject.name.Equals("ghost") &&
                !rnd.gameObject.name.Equals("smoke"))
            {
                Material mat = rnd.material;
                mat.SetFloat("_Mode", 0f);
                mat.SetColor("_Color", new Color(mat.color.r, mat.color.g, mat.color.b, 1f));
            }
        }
    }
}
