using UnityEngine;
using Random = UnityEngine.Random;

public class FireBallController : MonoBehaviour
{
    public float xInitialForceDirection;
    public float yInitialForceDirection;
    public float forceInitialMagnitude;

    void Start()
    {
        var fireBallRigidbody2D = GetComponent<Rigidbody2D>();

        var yRandomInitialForce = Random.Range(-yInitialForceDirection, yInitialForceDirection);
        var randomDirection = Random.Range(0, 2);

        var force = new Vector2(
            randomDirection < 1.0f ? -xInitialForceDirection : xInitialForceDirection,
            yRandomInitialForce);

        fireBallRigidbody2D.AddForce(
            force.normalized * forceInitialMagnitude
        );
    }
}
