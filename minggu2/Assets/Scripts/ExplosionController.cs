using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float blastRadius;
    public float explosionForce;

    void Start()
    {
        Destroy(gameObject, 1);

        var blastPosition = (Vector2) transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(blastPosition, blastRadius);

        foreach (var otherCollider in colliders)
        {
            if (otherCollider.CompareTag("Enemy") || otherCollider.CompareTag("Obstacle"))
            {
                var diff = (Vector2) otherCollider.gameObject.transform.position - blastPosition;
                var forceMultiplier = 1 - (diff.magnitude / blastRadius);
                otherCollider.attachedRigidbody.AddForce(diff.normalized * explosionForce * forceMultiplier);
            }
        }
    }
}