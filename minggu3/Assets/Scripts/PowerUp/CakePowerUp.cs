using UnityEngine;

public class CakePowerUp : PowerUp
{
    [SerializeField] private int healthIncrease = 10;
    protected override void executePower(GameObject player)
    {
        var health = player.GetComponent<PlayerHealth>();
        health.IncreaseHealth(healthIncrease);
    }
}