using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class SlingShooter : MonoBehaviour
{
    public CircleCollider2D circleCollider2D;
    public LineRenderer trajectory;
    public LineRenderer backRope;
    public LineRenderer frontRope;

    [SerializeField] private float birdRadius;
    private Vector2 _startPosition;
    private BirdController _bird;
    private Vector3 _backRopeInitialPosition;
    private Vector3 _frontRopeInitialPosition;

    [SerializeField] private float pullRadius = 0.75f;
    [SerializeField] private float throwSpeed = 30f;

    void Start()
    {
        _startPosition = transform.position;
        _backRopeInitialPosition = backRope.GetPosition(1);
        _frontRopeInitialPosition = frontRope.GetPosition(1);
    }

    private void OnMouseUp()
    {
        circleCollider2D.enabled = false;
        var currentPosition = (Vector2) transform.position;
        var velocity = _startPosition - currentPosition;
        var distance = Vector2.Distance(_startPosition, currentPosition);

        _bird.Shoot(velocity, distance, throwSpeed);

        transform.position = _startPosition;

        trajectory.enabled = false;

        backRope.SetPosition(1, _backRopeInitialPosition);
        frontRope.SetPosition(1, _frontRopeInitialPosition);
    }

    private void OnMouseDrag()
    {
        var mousePosition = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var diff = Vector2.ClampMagnitude(mousePosition - _startPosition, pullRadius);
        var positionTarget = (_startPosition + diff);
        transform.position = positionTarget;

        if (!trajectory.enabled)
        {
            trajectory.enabled = true;
        }

        DisplayTrajectory(diff);

        var ropePositionTarget = _startPosition + (diff * ((diff.magnitude + birdRadius) / diff.magnitude));

        frontRope.SetPosition(1, ropePositionTarget);
        backRope.SetPosition(1, ropePositionTarget);
    }

    void DisplayTrajectory(Vector2 diff)
    {
        if (_bird == null)
        {
            return;
        }

        const int segmentCount = 5;

        var segments = new Vector2[segmentCount];

        segments[0] = transform.position;

        var shootVelocity = (-diff) * throwSpeed * diff.magnitude;

        for (var i = 1; i < segmentCount; i++)
        {
            var elapsedTime = i * Time.fixedDeltaTime * 50 / shootVelocity.magnitude;
            segments[i] = segments[0] + shootVelocity * elapsedTime +
                          0.5f * Physics2D.gravity * Mathf.Pow(elapsedTime, 2);
        }

        trajectory.positionCount = segmentCount;
        for (var i = 0; i < segmentCount; i++)
        {
            trajectory.SetPosition(i, segments[i]);
        }
    }

    public void InitiateBird(BirdController bird)
    {
        _bird = bird;
        _bird.MoveTo(transform.position, gameObject);
        circleCollider2D.enabled = true;
    }
}