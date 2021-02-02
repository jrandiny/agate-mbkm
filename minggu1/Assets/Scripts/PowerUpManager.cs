using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PowerUpManager : MonoBehaviour
{
    public PowerUpController powerUp;
    private CapsuleCollider2D _powerUpCollider;

    public Vector2 minSpawn;
    public Vector2 maxSpawn;

    private bool _powerUpTaken = true;

    public PlayerControl player1Left;
    public PlayerControl player2Right;
    public Rigidbody2D ballRigidBody2D;

    public float powerUpDuration = 10.0f;

    void Start()
    {
        powerUp.activated.AddListener(delegate
        {
            _powerUpTaken = true;
            powerUp.gameObject.SetActive(false);

            StartCoroutine(ballRigidBody2D.velocity.x > 0 ? SmallTimer(player1Left) : SmallTimer(player2Right));
        });

        Reset();
    }

    public void Reset()
    {
        player1Left.UseLargeCollider = false;
        player2Right.UseLargeCollider = false;

        powerUp.gameObject.SetActive(false);
    }

    private IEnumerator SmallTimer(PlayerControl player)
    {
        player.UseLargeCollider = true;
        yield return new WaitForSeconds(powerUpDuration);
        player.UseLargeCollider = false;
    }

    public void SpawnPowerUp()
    {
        var position = new Vector2(
            Random.Range(minSpawn.x, maxSpawn.x),
            Random.Range(minSpawn.y, maxSpawn.y)
        );

        powerUp.transform.position = position;

        if (_powerUpTaken)
        {
            powerUp.gameObject.SetActive(true);
        }
    }

    public void DestroyPowerUp()
    {
        powerUp.gameObject.SetActive(false);
    }
}