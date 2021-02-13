using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CapsuleCollider))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private float sinkSpeed = 2.5f;
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private AudioClip deathClip;

    [HideInInspector] public int currentHealth;

    private Animator _animator;
    private AudioSource _audioSource;
    private ParticleSystem _hitParticles;
    private CapsuleCollider _capsuleCollider;
    private bool _isDead;
    private bool _isSinking;
    private static readonly int DeadAnimTrigger = Animator.StringToHash("Dead");


    private void Awake ()
    {
        _animator = GetComponent <Animator> ();
        _audioSource = GetComponent <AudioSource> ();
        _hitParticles = GetComponentInChildren <ParticleSystem> ();
        _capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }


    private void Update ()
    {
        if (_isSinking)
        {
            transform.Translate (-Vector3.up * (sinkSpeed * Time.deltaTime));
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if (_isDead)
            return;

        _audioSource.pitch = Random.Range(0.75f, 1.25f);
        _audioSource.PlayOneShot(_audioSource.clip);

        currentHealth -= amount;

        _hitParticles.transform.position = hitPoint;
        _hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death ();
        }
    }


    private void Death ()
    {
        _isDead = true;

        _capsuleCollider.isTrigger = true;

        _animator.SetTrigger (DeadAnimTrigger);

        _audioSource.clip = deathClip;
        _audioSource.Play ();
    }


    public void StartSinking ()
    {
        GetComponent<NavMeshAgent> ().enabled = false;
        GetComponent<Rigidbody> ().isKinematic = true;
        _isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
