using System;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
public enum EnemyState
{
    Chasing,
    Attacking,
    Die
}

public class TorchEnemyAI : EnemyBase
{
    public EnemyState currentState;
    private float lastTimeEnemyAttack;
    private bool isAttacking;
    [SerializeField]private Transform attackpoint;

    protected override void Awake()
    {
        startingBaseEnemyHealth = 990;
        enemyDamage = 20;
        lastTimeEnemyAttack = 0f;

        base.Awake();



    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Chasing:
                ChasePlayer();
                break;
            case EnemyState.Attacking:
                AttackPlayer();
                break;
            case EnemyState.Die:
                Die();
                break;
            default:
                Debug.Log("Nie znaleziono stanu");
                break;
        }

        CheckTransitions();

        if (lastTimeEnemyAttack < cooldownAttack)
        {
            lastTimeEnemyAttack += Time.deltaTime;
        }

        Debug.Log(healthSystem.getCurrentHealth());
    }

    private void ChasePlayer()
    {
        if (player == null || isAttacking) return;
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * movementSpeed * Time.deltaTime;
        anim.SetBool("IsEnemyRunning", true);

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }


    public void StopAttackOnFrame()
    {
        isAttacking = false;
    }


    private void AttackPlayer()
    {
        Vector3 direction = Vector3.zero;

        if (lastTimeEnemyAttack >= cooldownAttack && !isAttacking)
        {
            lastTimeEnemyAttack = 0f;
            anim.SetTrigger("EnemyAttacking");
            anim.SetBool("IsEnemyRunning", false);
            isAttacking = true;
        }

    }

    private void CheckTransitions()
    {
        if (healthSystem.getCurrentHealth() <= 0)
        {
            currentState = EnemyState.Die;
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < attackRange)
        {
            currentState = EnemyState.Attacking;
        }
        else
        {
            currentState = EnemyState.Chasing;
        }

    }

    private void Die()
    {
        base.EnemyDie();
    }

    public void CheckCollidersOfPlayer()
    {
        Collider2D[] playerCollider = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, playerLayer);
        if (playerCollider.Length > 0)
        {
            playerCollider[0].GetComponent<HealthSystem>().ChangeHealth(-enemyDamage);
        }

     }

}
