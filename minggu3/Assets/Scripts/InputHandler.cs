using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerShooting playerShooting;

    private LinkedList<Command> _commands = new LinkedList<Command>();

    private MoveCommand _moveCommand;
    private ShootCommand _shootCommand;

    private bool _isUndo = false;

    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        var movement = new Vector3(h, 0f, v);
        _moveCommand = new MoveCommand(playerMovement, movement);

        if (Input.GetKey(KeyCode.Z))
        {
            _isUndo = true;
        }

        var shootCommand = InputShootHandling();
        shootCommand?.Execute();
    }


    private void FixedUpdate()
    {
        if (_isUndo)
        {
            if (_commands.Count <= 0) return;
            
            _isUndo = false;
            var undoCommand = _commands.Last.Value;
            _commands.RemoveLast();
            
            undoCommand.UnExecute();
        }
        else if (_moveCommand != null)
        {
            _commands.AddLast(_moveCommand);
            if (_commands.Count > 1000) _commands.RemoveFirst();
            _moveCommand.Execute();
        }
    }

    private Command InputShootHandling()
    {
        return Input.GetButton("Fire1") ? new ShootCommand(playerShooting) : null;
    }
}