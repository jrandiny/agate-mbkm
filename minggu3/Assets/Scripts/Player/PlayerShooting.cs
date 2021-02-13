using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Light))]
public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private int damagePerShot = 20;                  
    [SerializeField] private float timeBetweenBullets = 0.15f;        
    [SerializeField] private float range = 100f;     

    private float _timer;                                    
    private Ray _shootRay;                                   
    private RaycastHit _shootHit;                            
    private int _shootableMask;                             
    private ParticleSystem _gunParticles;                    
    private LineRenderer _gunLine;                           
    private AudioSource _gunAudio;                           
    private Light _gunLight;                                 
    private float effectsDisplayTime = 0.2f;

    private void Awake()
    {
        _shootableMask = LayerMask.GetMask("Shootable");
        _gunParticles = GetComponent<ParticleSystem>();
        _gunLine = GetComponent<LineRenderer>();
        _gunAudio = GetComponent<AudioSource>();
        _gunLight = GetComponent<Light>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        _gunLine.enabled = false;
        _gunLight.enabled = false;
    }

    public void Shoot()
    {
        if (_timer <= timeBetweenBullets) return;
        
        _timer = 0f;

        _gunAudio.Play();

        _gunLight.enabled = true;

        _gunParticles.Stop();
        _gunParticles.Play();

        var position = transform.position;
        
        _gunLine.enabled = true;
        _gunLine.SetPosition(0, position);

        _shootRay.origin = position;
        _shootRay.direction = transform.forward;

        if (Physics.Raycast(_shootRay, out _shootHit, range, _shootableMask))
        {
            var enemyHealth = _shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, _shootHit.point);
            }

            _gunLine.SetPosition(1, _shootHit.point);
        }
        else
        {
            _gunLine.SetPosition(1, _shootRay.origin + _shootRay.direction * range);
        }
    }
}