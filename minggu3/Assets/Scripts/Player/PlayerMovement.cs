using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6f;

    private Vector3 _movement;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private int _floorMask;

    private static readonly int IsWalkingAnimBool = Animator.StringToHash("isWalking");
    private const float CamRayLength = 100f;

    private void Awake()
    {
        _floorMask = LayerMask.GetMask("Floor");
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        
        _movement.Set(h, 0f, v);
        
    }

    private void FixedUpdate()
    {
        Turning();
    }

    public void Move(Vector3 diffMovement)
    {
        var movement = diffMovement.normalized * (speed * Time.deltaTime);
        _rigidbody.MovePosition(transform.position + movement);
    }

    private void Turning()
    {
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