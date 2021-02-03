using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SlingShooter slingShot;
    public TrailController trailController;
    public List<BirdController> birds;
    public List<EnemyController> enemies;

    public BoxCollider2D tapCollider;

    private bool _isGameEnded = false;
    private BirdController _shotBird;
    void Start()
    {
        foreach(var bird in birds)
        {
            bird.OnBirdDestroyed += ChangeBird;
            bird.OnBirdShoot += AssignTrail;
        }

        foreach (var enemy in enemies)
        {
            enemy.OnEnemyDestroyed += CheckGameEnd;
        }

        tapCollider.enabled = false;
        slingShot.InitiateBird(birds[0]);
        _shotBird = birds[0];
    }

    public void AssignTrail(BirdController bird)
    {
        trailController.SetBird(bird);
        StartCoroutine(trailController.SpawnTrail());
        tapCollider.enabled = true;
    }

    public void ChangeBird()
    {
        tapCollider.enabled = false;

        if (_isGameEnded)
        {
            return;
        }

        birds.RemoveAt(0);

        if (birds.Count > 0)
        {
            slingShot.InitiateBird(birds[0]);
            _shotBird = birds[0];
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i].gameObject == destroyedEnemy)
            {
                enemies.RemoveAt(i);
                break;
            }
        }

        if(enemies.Count == 0)
        {
            _isGameEnded = true;
        }
    }

    private void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }
}
