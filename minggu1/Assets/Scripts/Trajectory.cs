using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public BallControl ball;
    private CircleCollider2D _ballCircleCollider2D;
    private Rigidbody2D _ballRigidbody2D;

    public GameObject ballAtCollision;

    void Start()
    {
        _ballRigidbody2D = ball.GetComponent<Rigidbody2D>();
        _ballCircleCollider2D = ball.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        RaycastHit2D circleCast = Physics2D.CircleCast(
            _ballRigidbody2D.position,
            _ballCircleCollider2D.radius,
            _ballRigidbody2D.velocity.normalized
        );

        if (circleCast.distance != 0)
        {
            var hitPoint = circleCast.point;
            var hitNormal = circleCast.normal;

            var offsetHitPoint = hitPoint + hitNormal * _ballCircleCollider2D.radius;

            DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitPoint);

            var inVector = (offsetHitPoint - ball.TrajectoryOrigin).normalized;
            var outVector = Vector2.Reflect(inVector, hitNormal);

            var outDot = Vector2.Dot(outVector, hitNormal);
            if (outDot > -1.0f && outDot < 1.0)
            {
                DottedLine.DottedLine.Instance.DrawDottedLine(
                    offsetHitPoint,
                    offsetHitPoint + outVector * 10.0f);

                ballAtCollision.transform.position = offsetHitPoint;
                ballAtCollision.SetActive(true);
            }
        }
        else
        {
            Vector2 position = ball.transform.position;
            DottedLine.DottedLine.Instance.DrawDottedLine(
                position,
                position + (_ballRigidbody2D.velocity.normalized * 30.0f)
            );

            ballAtCollision.SetActive(false);

        }
    }

    private void OnDisable()
    {
        ballAtCollision.SetActive(false);
    }
}