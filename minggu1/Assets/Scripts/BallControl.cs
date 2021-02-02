using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    public float xInitialForceDirection;
    public float yInitialForceDirection;
    public float forceInitialMagnitude;

    private Vector2 _trajectoryOrigin;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _trajectoryOrigin = transform.position;

        RestartGame();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _trajectoryOrigin = transform.position;
    }

    public Vector2 TrajectoryOrigin => _trajectoryOrigin;

    void ResetBall()
    {
        transform.position = Vector2.zero;
        _rigidbody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        float yRandomInitialForce = Random.Range(-yInitialForceDirection, yInitialForceDirection);
        float randomDirection = Random.Range(0, 2);

        var force = new Vector2(
            randomDirection < 1.0f ? -xInitialForceDirection : xInitialForceDirection,
            yRandomInitialForce);

        _rigidbody2D.AddForce(
            force.normalized * forceInitialMagnitude
        );
    }

    void RestartGame()
    {
        ResetBall();

        Invoke(nameof(PushBall), 2);
    }
}