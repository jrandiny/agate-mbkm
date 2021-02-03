using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BirdController : MonoBehaviour
{
    public enum BirdState
    {
        Idle,
        Thrown
    }

    public GameObject parent;
    private Rigidbody2D _rigidBody;
    private CircleCollider2D _collider;

    public UnityAction OnBirdDestroyed = delegate { };

    private BirdState _state;
    private float _minVelocity = 0.05f;
    private bool _flagDestroy = false;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();

        _rigidBody.bodyType = RigidbodyType2D.Kinematic;
        _collider.enabled = false;
        _state = BirdState.Idle;
    }

    private void FixedUpdate()
    {
        var sqrMagnitude = _rigidBody.velocity.sqrMagnitude;

        if (_state == BirdState.Idle && sqrMagnitude >= _minVelocity)
        {
            _state = BirdState.Thrown;
        }

        if (_state == BirdState.Thrown && sqrMagnitude < _minVelocity && !_flagDestroy)
        {
            _flagDestroy = true;
            Destroy(gameObject, 2);
        }
    }

    public void MoveTo(Vector2 target, GameObject newParent)
    {
        var transformObj = gameObject.transform;
        transformObj.SetParent(newParent.transform);
        transformObj.position = target;
    }

    public void Shoot(Vector2 velocity, float distance, float speed)
    {
        _collider.enabled = true;
        _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        _rigidBody.velocity = velocity * speed * distance;
    }

    private void OnDestroy()
    {
        OnBirdDestroyed();
    }
}