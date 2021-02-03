using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class SlingShooter : MonoBehaviour
{
    public CircleCollider2D circleCollider2D;
    public LineRenderer trajectory;
    private Vector2 _startPosition;
    private BirdController _bird;

    [SerializeField] private float pullRadius = 0.75f;
    [SerializeField] private float throwSpeed = 30f;

    void Start()
    {
        _startPosition = transform.position;
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
    }

    private void OnMouseDrag()
    {
        var mousePosition = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var diff = Vector2.ClampMagnitude(mousePosition - _startPosition, pullRadius);

        transform.position = _startPosition + diff;

        if (!trajectory.enabled)
        {
            trajectory.enabled = true;
        }
        float distance = Vector2.Distance(_startPosition, transform.position);
        DisplayTrajectory(diff);
    }
    void DisplayTrajectory(Vector2 diff)
    {
        if(_bird == null)
        {
            return;
        }

        const int segmentCount = 5;

        var segments = new Vector2[segmentCount];

        segments[0] = transform.position;

        var shootVelocity = (-diff) * throwSpeed * diff.magnitude;

        for (var i = 1; i < segmentCount; i++)
        {
            var elapsedTime = i * Time.fixedDeltaTime * 5;
            segments[i] = segments[0] + shootVelocity * elapsedTime + 0.5f * Physics2D.gravity * Mathf.Pow(elapsedTime, 2);
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