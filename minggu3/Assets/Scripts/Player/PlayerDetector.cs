using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private GameOverManager gameOverManager;

    private List<Collider> _enemyInRange = new List<Collider>();

    private void Awake()
    {
        InvokeRepeating(nameof(ShowWarning), 0.5f, 0.5f);
    }


    private void ShowWarning()
    {
        if (_enemyInRange.Count <= 0) return;
        
        var myPosition = transform.position;
        var nearestDistance = _enemyInRange
            .Select(enemy => Vector3.Distance(myPosition, enemy.transform.position))
            .Min();

        gameOverManager.ShowWarning(nearestDistance);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy") || other.isTrigger) return;
        
        if (!_enemyInRange.Contains(other))
        {
            _enemyInRange.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _enemyInRange.Remove(other);
    }
}