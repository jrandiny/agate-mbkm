using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class SlingShooter : MonoBehaviour
{
    public CircleCollider2D circleCollider2D;
    private Vector2 _startPosition;
    private BirdController _bird;

    [SerializeField] private float _pullRadius = 0.75f;
    [SerializeField] private float _throwSpeed = 30f;

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

        _bird.Shoot(velocity, distance, _throwSpeed);

        transform.position = _startPosition;
    }

    private void OnMouseDrag()
    {
        var mousePosition = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var diff = Vector2.ClampMagnitude(mousePosition - _startPosition, _pullRadius);

        transform.position = _startPosition + diff;
    }

    public void InitiateBird(BirdController bird)
    {
        _bird = bird;
        _bird.MoveTo(transform.position, gameObject);
        circleCollider2D.enabled = true;
    }
}