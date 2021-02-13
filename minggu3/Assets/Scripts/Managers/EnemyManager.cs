using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [Serializable]
    public struct EnemyType
    {
        public string enemyTag;
        public int spawnOdds;
    }
    // Seharusnya enemyType bisa langsung prefabnya, tapi karena di tutorial mengharuskan penggunaaan factory jadinya digunakan


    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private EnemyType[] enemyTypes;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnTime = 3f;

    private int _totalOdds;
    private int[] _spawnTypeLookup;

    private void Start()
    {
        _totalOdds = enemyTypes.Aggregate(0, (sum, entry) => sum + entry.spawnOdds);

        _spawnTypeLookup = new int[enemyTypes.Length];
        _spawnTypeLookup[0] = enemyTypes[0].spawnOdds;
        for (var i = 1; i < enemyTypes.Length; i++)
        {
            _spawnTypeLookup[i] = _spawnTypeLookup[i - 1] + enemyTypes[i].spawnOdds;
        }

        InvokeRepeating(nameof(Spawn), spawnTime, spawnTime);
    }


    private void Spawn()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        var spawnPointIndex = Random.Range(0, spawnPoints.Length);

        var enemyRandomType = Random.Range(0, _totalOdds);
        var chosenEnemy = 0;
        for (var i = 0; i < _spawnTypeLookup.Length; i++)
        {
            if (enemyRandomType >= _spawnTypeLookup[i]) continue;

            chosenEnemy = i;
            break;
        }

        enemyFactory.Create(
            enemyTypes[chosenEnemy].enemyTag,
            spawnPoints[spawnPointIndex].position,
            spawnPoints[spawnPointIndex].rotation
        );
    }
}