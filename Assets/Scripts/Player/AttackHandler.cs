using System.Security.Cryptography;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] private Transform attackpoint;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] private int damage = 30;

    public void PlayerAttacking()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayer);
        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<HealthSystem>().ChangeHealth(-damage);
        }

    }


}
