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
        Thrown,
        HitSomething
    }

    public GameObject parent;
    private Rigidbody2D _rigidBody;
    private CircleCollider2D _collider;

    public UnityAction OnBirdDestroyed = delegate { };
    public UnityAction<BirdController> OnBirdShoot = delegate { };

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

        if ((_state == BirdState.Thrown || _state == BirdState.HitSomething) &&
            sqrMagnitude < _minVelocity &&
            !_flagDestroy)
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
        OnBirdShoot(this);
    }

    private void OnDestroy()
    {
        if (_state == BirdState.Thrown || _state == BirdState.HitSomething)
        {
            OnBirdDestroyed();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _state = BirdState.HitSomething;
    }

    public BirdState State => _state;
    protected Rigidbody2D RigidBody => _rigidBody;

    public virtual void OnTap()
    {

    }
}