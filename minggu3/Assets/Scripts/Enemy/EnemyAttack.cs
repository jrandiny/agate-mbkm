﻿using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyHealth))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float timeBetweenAttacks = 0.5f;
    [SerializeField] private int attackDamage = 10;


    private Animator _animator;
    private GameObject _player;
    private PlayerHealth _playerHealth;
    private EnemyHealth _enemyHealth;
    private bool _playerInRange;
    private float _timer;
    private static readonly int PlayerDeadAnimTrigger = Animator.StringToHash("PlayerDead");


    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<PlayerHealth>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.isTrigger == false)
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }


    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= timeBetweenAttacks && _playerInRange && _enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        if (_playerHealth.CurrentHealth <= 0)
        {
            _animator.SetTrigger(PlayerDeadAnimTrigger);
        }
    }


    private void Attack()
    {
        _timer = 0f;

        if (_playerHealth.CurrentHealth > 0)
        {
            _playerHealth.TakeDamage(attackDamage);
        }
    }
}