using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private EnemyHealth _enemyHealth;
    private Transform _player;
    private NavMeshAgent _navMeshAgent;


    private void Awake ()
    {
        _player = GameObject.FindGameObjectWithTag ("Player").transform;

        _playerHealth = _player.GetComponent <PlayerHealth> ();
        _enemyHealth = GetComponent <EnemyHealth> ();
        _navMeshAgent = GetComponent <NavMeshAgent> ();
    }


    private void Update ()
    {
        if (_enemyHealth.currentHealth > 0 && _playerHealth.currentHealth > 0)
        {
            _navMeshAgent.SetDestination (_player.position);
        }
        else
        {
            _navMeshAgent.enabled = false;
        }
    }
}
