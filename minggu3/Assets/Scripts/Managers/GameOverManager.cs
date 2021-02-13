using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private float restartDelay = 5f;
    [SerializeField] private Text warningText;


    private Animator _animator;
    private float _restartTimer;
    private static readonly int GameOverAnimTrigger = Animator.StringToHash("GameOver");
    private static readonly int WarningAnimTrigger = Animator.StringToHash("Warning");
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (playerHealth.currentHealth > 0) return;
        
        _animator.SetTrigger(GameOverAnimTrigger);

        _restartTimer += Time.deltaTime;

        if (_restartTimer >= restartDelay)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ShowWarning(float enemyDistance)
    {
        warningText.text = $"{Mathf.RoundToInt(enemyDistance)}m!";
        _animator.SetTrigger(WarningAnimTrigger);
    }
}