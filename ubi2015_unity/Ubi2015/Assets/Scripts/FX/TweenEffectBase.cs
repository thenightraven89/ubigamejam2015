using UnityEngine;
using System.Collections;

public class TweenEffectBase : MonoBehaviour
{
    public float duration;
    public Transform Target { get; private set; }

    public virtual void Play(Transform target)
    {
        Target = target;
    }

    public virtual void Stop(float fadeOutTime)
    {
        throw new System.NotImplementedException();
    }

    void OnDestroy()
    {
        Stop(0);
    }
}
