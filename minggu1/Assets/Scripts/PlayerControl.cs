using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    // Control
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;

    // Param
    public float speed = 10.0f;
    public float yBoundary = 9.0f;

    // For resize
    public float largeHeightMultiplier;

    private Rigidbody2D _rigidbody2D;
    private int _score;

    private ContactPoint2D _lastContactPoint;
    private bool _useLargeCollider;

    [HideInInspector] public UnityEvent dieFireball = new UnityEvent();

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _useLargeCollider = false;
    }

    void Update()
    {
        var velocity = _rigidbody2D.velocity;

        if (Input.GetKey(upButton))
        {
            velocity.y = speed;
        }
        else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }
        else
        {
            velocity.y = 0f;
        }

        _rigidbody2D.velocity = velocity;

        var position = transform.position;

        if (position.y > yBoundary)
        {
            position.y = yBoundary;
        }
        else if (position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }

        transform.position = position;
    }

    public void IncrementScore()
    {
        _score++;
    }

    public void ResetScore()
    {
        _score = 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Ball")
        {
            _lastContactPoint = other.GetContact(0);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            dieFireball.Invoke();
        }
    }

    public int Score => _score;
    public ContactPoint2D LastContactPoint => _lastContactPoint;

    public bool UseLargeCollider
    {
        get => _useLargeCollider;
        set
        {
            transform.localScale = new Vector3(1, value ? largeHeightMultiplier : 1);
            _useLargeCollider = value;
        }
    }
}