using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownBirdController : BirdController
{
    public GameObject explosionPrefab;
    protected override void OnCollision()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
