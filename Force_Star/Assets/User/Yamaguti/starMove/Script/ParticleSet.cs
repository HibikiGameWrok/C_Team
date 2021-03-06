﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSet : MonoBehaviour
{
    ParticleSystem.MainModule mEmObj;

    int maxStar;

    // Start is called before the first frame update
    void Start()
    {
        maxStar = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetMaxStar(int max)
    {
        ParticleSystem ParticleObj = transform.Find("StarParticle").GetComponent<ParticleSystem>();
        ParticleMove ParticleMove = transform.Find("StarParticle").GetComponent<ParticleMove>();
        //↓最終目的である rate にアクセスするために必要な emission を取得し格納
        mEmObj = ParticleObj.main;
        maxStar = max;
        mEmObj.maxParticles = maxStar;
      //  ParticleMove.SetMaxParticle();
    }

    public void ResetSpeed()
    {
        ParticleSystem[] psArray = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < psArray.Length; i++)
        {
            ParticleSystem.MainModule psm = psArray[i].main;
            psm.simulationSpeed = 0.001f;
        }
    }

    public void Kill()
    {
        ParticleSystem[] psArray = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < psArray.Length; i++)
        {
            ParticleSystem.MainModule psm = psArray[i].main;

        }
    }
}
