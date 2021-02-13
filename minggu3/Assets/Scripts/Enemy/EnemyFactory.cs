using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IFactory
{
    [Serializable]
    public struct EnemyPrefab
    {
        public string tag;
        public GameObject prefab;
    }

    [SerializeField] private EnemyPrefab[] enemyPrefabs;

    private Dictionary<string, GameObject> _enemyLookup;

    private void Awake()
    {
        _enemyLookup = new Dictionary<string, GameObject>();

        foreach (var enemy in enemyPrefabs)
        {
            _enemyLookup.Add(enemy.tag, enemy.prefab);
        }
    }

    public GameObject Create(string spawnTag)
    {
        return Instantiate(_enemyLookup[spawnTag]);
    }

    public GameObject Create(string spawnTag, Vector3 position, Quaternion rotation)
    {
        return Instantiate(_enemyLookup[spawnTag], position, rotation);
    }
}