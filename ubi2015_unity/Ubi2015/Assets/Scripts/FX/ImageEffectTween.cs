using UnityEngine;
using System.Collections;

public class ImageEffectTween : TweenEffectBase
{
    protected Camera mainCam;

    protected virtual void Awake()
    {
        mainCam = Camera.main;
    }
}
