using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField] protected int startingBaseEnemyHealth = 100;
    [SerializeField] protected float movementSpeed = 4;
    [SerializeField] protected float attackRange = 1.5f;
    [SerializeField] protected int enemyDamage = 10;
    [SerializeField] protected float cooldownAttack = 2f;
    [SerializeField] protected LayerMask playerLayer;

    [Header("Inspector Attributes")]
    [SerializeField]protected Transform player;
    protected HealthSystem healthSystem;
    protected Animator anim;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.Init(startingBaseEnemyHealth);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void EnemyDie()
    {
        Debug.Log("Enemy Died");
    }

 
}
