using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SlingShooter slingShot;
    public List<BirdController> birds;
    void Start()
    {
        foreach(BirdController bird in birds)
        {
            bird.OnBirdDestroyed += ChangeBird;
        }
        slingShot.InitiateBird(birds[0]);
    }

    public void ChangeBird()
    {
        birds.RemoveAt(0);

        if (birds.Count > 0)
        {
            slingShot.InitiateBird(birds[0]);
        }
    }
}
