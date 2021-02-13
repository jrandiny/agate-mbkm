using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour, IFactory
{
    // Dibuat Item Factory karena kalau dipisah enemy dan powerup factory, jadi lebih redundan lagi. Seharusnya tidak butuh factory ini juga bisa
    
    [Serializable]
    public struct ItemPrefab
    {
        public string tag;
        public GameObject prefab;
    }

    [SerializeField] private ItemPrefab[] itemPrefabs;

    private Dictionary<string, GameObject> _itemLookup;

    private void Awake()
    {
        _itemLookup = new Dictionary<string, GameObject>();

        foreach (var enemy in itemPrefabs)
        {
            _itemLookup.Add(enemy.tag, enemy.prefab);
        }
    }

    public GameObject Create(string spawnTag)
    {
        return Instantiate(_itemLookup[spawnTag]);
    }

    public GameObject Create(string spawnTag, Vector3 position, Quaternion rotation)
    {
        return Instantiate(_itemLookup[spawnTag], position, rotation);
    }
}