using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var colliderTag = other.gameObject.tag;
        if (colliderTag == "Bird" || colliderTag == "Enemy" || colliderTag == "Obstacle")
        {
            Destroy(other.gameObject);
        }
    }
}