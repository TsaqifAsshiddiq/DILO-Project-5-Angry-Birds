﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird
{
    [SerializeField]
    public float _boostForce = 100;
    public bool _hasBoost = false;
    public void Boost()
    {
        if(state == BirdState.Thrown && !_hasBoost)
        {
            rigidbody.AddForce(rigidbody.velocity * _boostForce);
            _hasBoost = true;
        }
    }

    public override void OnTap()
    {
        Boost();
    }
}
