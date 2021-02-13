using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6f;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private int _floorMask;

    private static readonly int IsWalkingAnimBool = Animator.StringToHash("isWalking");
    private const float CamRayLength = 100f;

    private bool _stopMovement = false;

    private void Awake()
    {
        _floorMask = LayerMask.GetMask("Floor");
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Turning();
    }

    public void DisableMovement()
    {
        _stopMovement = true;
    }

    public void Move(Vector3 diffMovement)
    {
        if (_stopMovement) return;
        
        var movement = diffMovement.normalized * (speed * Time.deltaTime);
        _rigidbody.MovePosition(transform.position + movement);
    }

    private void Turning()
    {
        if (_stopMovement) return;
        
        var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;
        if (!Physics.Raycast(camRay, out floorHit, CamRayLength, _floorMask)) return;
        
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