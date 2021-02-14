using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothing = 5f;

    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        var targetPose = target.position + _offset;

        transform.position = Vector3.Lerp(transform.position, targetPose, smoothing * Time.deltaTime);
    }
}