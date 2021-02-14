using UnityEngine;

public class GraveDestroyer : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}