using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    public GameObject trailPrefab;
    public BirdController targetBird;

    private List<GameObject> _trails;
    void Start()
    {
        _trails = new List<GameObject>();
    }

    public void SetBird(BirdController bird)
    {
        targetBird = bird;

        foreach (var trail in _trails)
        {
            Destroy(trail);
        }

        _trails.Clear();
    }

    public IEnumerator SpawnTrail()
    {
        _trails.Add(Instantiate(trailPrefab, targetBird.transform.position, Quaternion.identity));

        yield return new WaitForSeconds(0.1f);

        if (targetBird != null && targetBird.State != BirdController.BirdState.HitSomething)
        {
            StartCoroutine(SpawnTrail());
        }
    }
}
