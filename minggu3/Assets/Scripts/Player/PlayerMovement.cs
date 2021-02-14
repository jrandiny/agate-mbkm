using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 6f;
    [SerializeField] private PowerUpUiManager powerUpUiManager;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private int _floorMask;

    private static readonly int IsWalkingAnimBool = Animator.StringToHash("isWalking");
    private const float CamRayLength = 100f;

    private bool _stopMovement = false;
    private float _speed;

    private Camera _mainCamera;

    private void Awake()
    {
        _floorMask = LayerMask.GetMask("Floor");
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

        _speed = initialSpeed;
        
        _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        Turning();
    }

    public void DisableMovement()
    {
        _stopMovement = true;
    }

    public void SpeedUp(float targetSpeed, float length)
    {
        _speed = targetSpeed;

        powerUpUiManager.ShowTimer(length);

        StopAllCoroutines();
        StartCoroutine(ResetSpeed(length));
    }

    private IEnumerator ResetSpeed(float length)
    {
        yield return new WaitForSeconds(length);
        _speed = initialSpeed;
    }

    public void Move(Vector3 diffMovement)
    {
        if (_stopMovement) return;

        var movement = diffMovement.normalized * (_speed * Time.deltaTime);
        _rigidbody.MovePosition(transform.position + movement);
    }

    private void Turning()
    {
        if (_stopMovement) return;

        var camRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(camRay, out var floorHit, CamRayLength, _floorMask)) return;

        var playerToMouseFloorRay = floorHit.point - transform.position;
        playerToMouseFloorRay.y = 0f;

        var newRotation = Quaternion.LookRotation(playerToMouseFloorRay);

        _rigidbody.MoveRotation(newRotation);
    }

    public void Animating(Vector3 diffMovement)
    {
        _animator.SetBool(IsWalkingAnimBool, diffMovement.sqrMagnitude > 0);
    }
}