using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FXBase : MonoBehaviour
{
    public bool looped = false;
    public float destroyAfter = 0f;
    public float fadeTime = 0f;

    public Transform Target { get; private set; } 

    protected ParticleSystem particles;
    protected AudioSource audioSource;
    protected TweenEffectBase tweenEffect;

    void Awake()
    {
        Play();
    }

    public virtual void Play(Transform target = null)
    {
        Target = target;
        particles = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        tweenEffect = GetComponent<TweenEffectBase>();
        if (destroyAfter == 0f)
        {
            float audioDuration = audioSource == null ? 0f : audioSource.clip.length;
            float particlesDuration = particles == null ? 0f : particles.duration;
            float ssEffectDuration = tweenEffect == null ? 0f : tweenEffect.duration;
            destroyAfter = Mathf.Max(audioDuration, particlesDuration, ssEffectDuration);
        }
        if (tweenEffect!=null)
        {
            tweenEffect.Play(Target);
        }
        if (!looped)
        {
            StartCoroutine(WaitBeforeFade(destroyAfter));
        }
    }

    protected virtual IEnumerator WaitBeforeFade(float time)
    {
        yield return new WaitForSeconds(time);
        Stop(fadeTime);
    }

    protected virtual IEnumerator WaitBeforeDestruction(float time)
    {
        yield return new WaitForSeconds(time);
        AutoDestruct();
    }

    public virtual void Stop(float time = 0f)
    {
        if (time == 0f)
        {
            AutoDestruct();
        }
        else
        {
            FadeOut(time);
        }
    }

    protected virtual void FadeOut(float time)
    {
        if (audioSource != null)
        {
            DOTween.To(() => audioSource.volume, x => audioSource.volume = x, 0f, time);
        }
        if (particles != null)
        {
            DOTween.To(() => particles.startColor, x => particles.startColor = x, 
                new Color(particles.startColor.r, particles.startColor.g, particles.startColor.b, 0f), time);
        }
        if (tweenEffect != null)
        {
            tweenEffect.Stop(time);
        }
        StartCoroutine(WaitBeforeDestruction(time));
    }

    protected virtual void AutoDestruct()
    {
        Destroy(gameObject);
    }
}
