﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FXManager : MonoBehaviour
{
    public static FXManager Instance { get; private set; }

    public List<GameObject> effects;

    private Dictionary<string, GameObject> effectsDictionary;

    void Awake ()
    {
        Instance = this;

        effectsDictionary = new Dictionary<string, GameObject>();
        effects.ForEach(x =>
       {
           if (!effectsDictionary.ContainsKey(x.name))
           {
               effectsDictionary.Add(x.name, x);
           }
       });
        //StartCoroutine(TestCoroutine());
	}

    IEnumerator TestCoroutine()
    {
        FXBase f = PlayEffect("TweenAlphaInvulnerability", GameObject.Find("Cube").transform);
        yield return new WaitForSeconds(5f);
        //f.Stop(3);
    }

    public FXBase PlayEffect(string effectName, Transform target)
    {
        if (!effectsDictionary.ContainsKey(effectName))
        {
            return null;
        }
        FXBase fx = (Instantiate(effectsDictionary[effectName], target.position, target.rotation) as GameObject).GetComponent<FXBase>();
        //Debug.Log(target);
        fx.Play(target);
        return fx;
    }

    public FXBase PlayEffect(string effectName, Vector3 target)
    {
        if (!effectsDictionary.ContainsKey(effectName))
        {
            return null;
        }
        FXBase fx = (Instantiate(effectsDictionary[effectName], target, Quaternion.identity) as GameObject).GetComponent<FXBase>();
        fx.Play();
        return fx;
    }

    public FXBase PlayEffect(string effectName)
    {
        if (!effectsDictionary.ContainsKey(effectName))
        {
            return null;
        }
        FXBase fx = (Instantiate(effectsDictionary[effectName], transform.position, transform.rotation) as GameObject).GetComponent<FXBase>();
        fx.Play();
        return fx;
    }
}
