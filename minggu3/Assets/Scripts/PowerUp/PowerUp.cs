using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] private float despawnTimer = 3f;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(DespawnTimer());
    }

    private IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(despawnTimer);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || other.isTrigger) return;

        executePower(other.gameObject);

        gameObject.transform.position = new Vector3(0, 50, 0);

        _audioSource.Play();

        Destroy(gameObject, _audioSource.clip.length);
    }

    protected abstract void executePower(GameObject player);
}