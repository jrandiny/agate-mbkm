﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Control
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;

    // Param
    public float speed = 10.0f;
    public float yBoundary = 9.0f;

    private Rigidbody2D _rigidbody2D;
    private int _score;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var velocity = _rigidbody2D.velocity;

        if (Input.GetKey(upButton))
        {
            velocity.y = speed;
        }else if (Input.GetKey(downButton))
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
        }else if (position.y < -yBoundary)
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

    public int Score => _score;
}
