using UnityEngine;

public class BoostPowerUp : PowerUp
{
    [SerializeField] private float targetSpeed = 10f;
    [SerializeField] private float powerupLength = 5f;

    protected override void ExecutePower(GameObject player)
    {
        var playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.SpeedUp(targetSpeed, powerupLength);
    }
}