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
    public bool Ready {get { return attackTimer == 0; }}

    private float attackTimer = 0;
    
    public void Update()
    {
        attackTimer = Math.Max(0, attackTimer - Time.fixedDeltaTime);
    }

    public bool TryAttack(GameObject enemyFly)
    {
        Debug.Log("Attacking target fly" + enemyFly.name);
        if (attackTimer > 0) return false;
        HealthManager target = null;
        if (enemyFly.TryGetComponent<HealthManager>(out target))
        {
            attackTimer = AttackTime;
            target.TakeDamage(attackDamage);
            return true;
        }
        return false;
    }
}
