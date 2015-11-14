using UnityEngine;
using System.Collections;
using DG.Tweening;

public class RotationTween : MonoBehaviour
{
    public Vector3 amount;
    public Ease easeType = Ease.Linear;
    public LoopType loopType = LoopType.Restart;
    public float rotationTime = 1f;
	// Use this for initialization
	void Start ()
    {
        transform.DOBlendableLocalRotateBy(amount, rotationTime).SetLoops(-1, loopType).SetEase(easeType);
	}
}
