using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemManager : MonoBehaviour
{
    [Serializable]
    public struct ItemType
    {
        public string itemTag;
        public int spawnOdds;
    }
    // Seharusnya enemyType bisa langsung prefabnya, tapi karena di tutorial mengharuskan penggunaaan factory jadinya digunakan


    [SerializeField] private ItemFactory itemFactory;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private ItemType[] itemTypes;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnTime = 3f;

    private int _totalOdds;
    private int[] _spawnTypeLookup;

    private void Start()
    {
        _totalOdds = itemTypes.Aggregate(0, (sum, entry) => sum + entry.spawnOdds);

        _spawnTypeLookup = new int[itemTypes.Length];
        _spawnTypeLookup[0] = itemTypes[0].spawnOdds;
        for (var i = 1; i < itemTypes.Length; i++)
        {
            _spawnTypeLookup[i] = _spawnTypeLookup[i - 1] + itemTypes[i].spawnOdds;
        }

        InvokeRepeating(nameof(Spawn), spawnTime, spawnTime);
    }


    private void Spawn()
    {
        if (playerHealth.CurrentHealth <= 0f)
        {
            return;
        }

        var spawnPointIndex = Random.Range(0, spawnPoints.Length);

        var itemRandomType = Random.Range(0, _totalOdds);
        var chosenItem = 0;
        for (var i = 0; i < _spawnTypeLookup.Length; i++)
        {
            if (itemRandomType >= _spawnTypeLookup[i]) continue;

            chosenItem = i;
            break;
        }

        itemFactory.Create(
            itemTypes[chosenItem].itemTag,
            spawnPoints[spawnPointIndex].position,
            spawnPoints[spawnPointIndex].rotation
        );
    }
}