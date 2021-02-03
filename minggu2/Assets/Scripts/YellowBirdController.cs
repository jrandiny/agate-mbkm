using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBirdController : BirdController
{
    public float boostForce = 100;
    private bool _hasBoost = false;

    private void Boost()
    {
        if (_state == BirdState.Thrown && !_hasBoost)
        {
            _rigidBody.AddForce(_rigidBody.velocity * boostForce);
            _hasBoost = true;
        }
    }

    public override void OnTap()
    {
        Boost();
    }
}
