using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FallAndFade : MonoBehaviour {

    public Transform pivot;
    public Renderer meshRenderer;
    public Vector3 rotation;
    public float fallTime;
    public float fadeAfter;
    public float fadeIn;
    public Ease fallEaseType;

    public void Fall()
    {
        float zRandom = Random.Range(-10f, 10f);

        pivot.DOBlendableLocalRotateBy(rotation + new Vector3(0f, 0f, zRandom), fallTime).SetEase(fallEaseType);
        StartCoroutine(WaitAndFade());
    }

    IEnumerator WaitAndFade()
    {
        yield return new WaitForSeconds(fadeAfter);
        Fade();
    }

    private void Fade()
    {
        Material mat = meshRenderer.material;
        GetComponentInChildren<Collider>().enabled = false;
        Tweener tw = DOTween.To(() => mat.GetColor("_Color"), c => mat.SetColor("_Color", c),
                        new Color(mat.color.r, mat.color.g, mat.color.b, 0f), fadeIn).OnComplete(new TweenCallback(Destroy));
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
