using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image damageImage;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private float flashSpeed = 5f;
    [SerializeField] private Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    private Animator _animator;
    private AudioSource _playerAudio;

    private PlayerMovement _playerMovement;
    private PlayerShooting _playerShooting;

    private bool _isDead;
    private bool _damaged;
    private int _currentHealth;

    private static readonly int DieAnimTrigger = Animator.StringToHash("Die");

    public int CurrentHealth
    {
        get => _currentHealth;
        private set
        {
            _currentHealth = value;
            healthSlider.value = value;
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerShooting = GetComponentInChildren<PlayerShooting>();

        CurrentHealth = startingHealth;
    }


    private void Update()
    {
        damageImage.color =
            _damaged ? flashColour : Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        _damaged = false;
    }


    public void TakeDamage(int amount)
    {
        _damaged = true;

        CurrentHealth -= amount;

        _playerAudio.Play();

        if (CurrentHealth <= 0 && !_isDead)
        {
            Death();
        }
    }

    public void IncreaseHealth(int delta)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + delta, 0, startingHealth);
    }


    private void Death()
    {
        _isDead = true;

        _playerShooting.DisableEffects();

        _animator.SetTrigger(DieAnimTrigger);

        _playerAudio.clip = deathClip;
        _playerAudio.Play();

        _playerMovement.DisableMovement();
        _playerShooting.DisableShooting();
    }
}