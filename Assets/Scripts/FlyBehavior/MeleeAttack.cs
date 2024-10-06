using System;
using UnityEngine;

public class MeleeAttack : MonoBehaviour, IAttack
{
    [field: SerializeField]
    public float AttackRange {get; set;} = 0.5f;
    // anim time
    [field: SerializeField]
    public float AttackAnimTime {get; set;} = 0.25f;
    // attack rate
    [field: SerializeField]
    public float AttackTime {get; set;} = 0.5f;
    [SerializeField]
    private int attackDamage = 1;

    [SerializeField]
    private float knockback = 0f;
    [SerializeField]
    private float selfKnockback = 0f;
    public bool Ready {get { return attackTimer == 0; }}

    private float attackTimer = 0;
    private Rigidbody2D rb;
    public void Start()
    {
        if (!TryGetComponent(out rb))
        {
            Debug.LogWarning("No rigidbody attached to fly");
        }
    }
    
    public void FixedUpdate()
    {
        attackTimer = Math.Max(0, attackTimer - Time.fixedDeltaTime);
    }

    public bool TryAttack(GameObject enemyFly)
    {
        Debug.Log(name + " is attacking target fly" + enemyFly.name);
        if (Ready && Vector3.Distance(transform.position, enemyFly.transform.position) < AttackRange)
        {
            HealthManager target = null;
            Rigidbody2D targetRb = null;
            if (enemyFly.TryGetComponent(out targetRb))
            {
                targetRb.AddForce((transform.position - targetRb.transform.position) * knockback * 10);
            }
            rb.AddForce((enemyFly.transform.position - transform.position ) * selfKnockback * 10);
            if (enemyFly.TryGetComponent(out target))
            {
                attackTimer = AttackTime;
                target.TakeDamage(attackDamage);
                return true;
            }
        }
        return false;
    }
}
