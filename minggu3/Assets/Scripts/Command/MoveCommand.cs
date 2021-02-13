using UnityEngine;

public class MoveCommand : Command
{
    private PlayerMovement _playerMovement;
    private Vector3 _movement;
    private float _v;


    public MoveCommand(PlayerMovement playerMovement, Vector3 movement)
    {
        _playerMovement = playerMovement;
        _movement = movement;
    }

    public override void Execute()
    {
        _playerMovement.Move(_movement);
        _playerMovement.Animating(_movement);
    }

    public override void UnExecute()
    {
        _playerMovement.Move(-_movement);
        _playerMovement.Animating(_movement);
    }
}
