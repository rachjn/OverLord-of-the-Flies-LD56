using System;
using UnityEngine;

public class RangedAttack : MonoBehaviour, IAttack
{
    [field: SerializeField]
    public float AttackRange {get; set;} = 3f;
    // anim time
    [field: SerializeField]
    public float AttackAnimTime {get; set;} = 0.25f;
    // attack rate
    [field: SerializeField]
    public float AttackTime {get; set;} = 0.75f;
    [SerializeField]
    private int attackDamage = 1;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float selfKnockback = 0.01f;
    public bool Ready {get { return attackTimer == 0; }}

    private FlyManager fly;

    private float attackTimer = 0;

    public void Start()
    {
        if (!TryGetComponent(out fly))
        {
            Debug.LogWarning("No FlyManager");
        }
    }
    
    public void FixedUpdate()
    {
        attackTimer = Math.Max(0, attackTimer - Time.fixedDeltaTime);
    }

    public bool TryAttack(GameObject enemyFly)
    {
        Debug.Log(name + " is attacking target fly" + enemyFly.name);
        if (attackTimer > 0) return false;
        HealthManager target = null;
        if (enemyFly.TryGetComponent<HealthManager>(out target))
        {
            if (selfKnockback != 0)
            {
                fly.ReceiveKnockback(-(enemyFly.transform.position - transform.position ) * selfKnockback);
            }
            attackTimer = AttackTime;
            var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().Target = target;
            return true;
        }
        return false;
    }
}
