using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerControl player1Left;
    public PlayerControl player2Right;

    public PowerUpManager powerUpManager;
    public float powerUpSpawnMinTimer = 3.0f;
    public float powerUpSpawnMaxTimer = 10.0f;

    public BallControl ballControl;
    private Rigidbody2D _ballRigidBody2D;
    private CircleCollider2D _ballCollider2D;

    public FireBallController fireBallPrefab;

    public Trajectory trajectory;

    public int maxScore;

    private bool _showDebugWindow = false;
    private bool _gameOver = false;

    private bool _player1Win = false;
    private bool _player2Win = false;

    void Start()
    {
        _ballRigidBody2D = ballControl.GetComponent<Rigidbody2D>();
        _ballCollider2D = ballControl.GetComponent<CircleCollider2D>();
        trajectory.enabled = false;

        player1Left.dieFireball.AddListener(delegate { _player2Win = true; });
        player2Right.dieFireball.AddListener(delegate { _player1Win = true; });

        StartGame();
    }

    private void StartGame()
    {
        _player1Win = false;
        _player2Win = false;

        player1Left.ResetScore();
        player2Right.ResetScore();
        _gameOver = false;

        powerUpManager.Reset();

        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(SpawnFireballRoutine());
    }

    private IEnumerator SpawnPowerUpRoutine()
    {
        while (!_gameOver)
        {
            yield return new WaitForSeconds(Random.Range(powerUpSpawnMinTimer, powerUpSpawnMaxTimer));
            if (!_gameOver)
            {
                powerUpManager.SpawnPowerUp();
            }
        }

        powerUpManager.DestroyPowerUp();
    }

    private IEnumerator SpawnFireballRoutine()
    {
        while (!_gameOver)
        {
            yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
            if (!_gameOver)
            {
                Instantiate(fireBallPrefab);
            }
        }
    }


    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), player1Left.Score.ToString());
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), player2Right.Score.ToString());

        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            StartGame();

            ballControl.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
        }

        if (player1Left.Score == maxScore || _player1Win)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");
            ballControl.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
            _gameOver = true;
        }
        else if (player2Right.Score == maxScore || _player2Win)
        {
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");
            ballControl.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
            _gameOver = true;
        }

        if (_showDebugWindow)
        {
            Color oldColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;

            var ballMass = _ballRigidBody2D.mass;
            var ballVelocity = _ballRigidBody2D.velocity;
            var ballSpeed = ballVelocity.magnitude;
            var ballMomentum = ballMass * ballVelocity;
            var ballFriction = _ballCollider2D.friction;

            var impulsePlayer1X = player1Left.LastContactPoint.normalImpulse;
            var impulsePlayer1Y = player1Left.LastContactPoint.tangentImpulse;
            var impulsePlayer2X = player2Right.LastContactPoint.normalImpulse;
            var impulsePlayer2Y = player2Right.LastContactPoint.tangentImpulse;

            var debugText =
                "Ball mass = " + ballMass + "\n" +
                "Ball velocity = " + ballVelocity + "\n" +
                "Ball speed = " + ballSpeed + "\n" +
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction = " + ballFriction + "\n" +
                "Last impulse from player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
                "Last impulse from player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n";

            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea) {alignment = TextAnchor.UpperCenter};
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);

            GUI.backgroundColor = oldColor;
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO"))
        {
            _showDebugWindow = !_showDebugWindow;
            trajectory.enabled = !trajectory.enabled;
        }
    }
}