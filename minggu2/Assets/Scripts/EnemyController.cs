using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public float health = 50f;
    public UnityAction<GameObject> OnEnemyDestroyed = delegate { };

    private void OnDestroy()
    {
        OnEnemyDestroyed(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bird"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            var damage = other.rigidbody.velocity.magnitude * 10;
            health -= damage;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}