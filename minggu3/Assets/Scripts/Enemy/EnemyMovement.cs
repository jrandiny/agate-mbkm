using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float riseUpAnimTime;
    [SerializeField] private GameObject spawnGravePrefab;

    private PlayerHealth _playerHealth;
    private EnemyHealth _enemyHealth;
    private Transform _player;
    private NavMeshAgent _navMeshAgent;

    private bool _ready;


    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _playerHealth = _player.GetComponent<PlayerHealth>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _ready = false;
        StartCoroutine(RiseUp());
    }

    private IEnumerator RiseUp()
    {
        var undergroundPosition = transform.position;

        var startPosition = new Vector3(undergroundPosition.x, 0f, undergroundPosition.z);
        var grave = Instantiate(spawnGravePrefab, startPosition, Quaternion.identity);
        Destroy(grave, riseUpAnimTime * 0.01f);

        for (var i = 0; i < riseUpAnimTime; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.position = Vector3.Lerp(
                undergroundPosition,
                startPosition,
                Mathf.SmoothStep(0, 1f, i / riseUpAnimTime)
            );
        }

        _ready = true;
    }


    private void Update()
    {
        if (!_ready) return;

        if (_enemyHealth.currentHealth > 0 && _playerHealth.CurrentHealth > 0)
        {
            _navMeshAgent.SetDestination(_player.position);
        }
        else
        {
            _navMeshAgent.enabled = false;
        }
    }
}