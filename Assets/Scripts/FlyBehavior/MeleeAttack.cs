using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MeleeAttack : MonoBehaviour, IAttack
{
    [field: SerializeField]
    public float AttackRange {get; set;} = 0.5f;
    // anim time
    [field: SerializeField]
    public float AttackAnimTime {get; set;} = 0.5f;
    // attack rate
    [field: SerializeField]
    public float AttackTime {get; set;} = 0.5f;
    [SerializeField]
    private int attackDamage = 1;

    [SerializeField]
    private float knockback = 0.5f;
    [SerializeField]
    private float selfKnockback = 0f;
    public bool Ready {get { return attackTimer == 0; }}

    private float attackTimer = 0;
    private FlyManager fly;
    private SpriteRenderer sprite;
    public void Start()
    {
        if (!TryGetComponent(out fly))
        {
            Debug.LogWarning("No FlyManager");
        }
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    
    public void FixedUpdate()
    {
        attackTimer = Math.Max(0, attackTimer - Time.fixedDeltaTime);
    }

    public IEnumerator DoAttack(GameObject enemyFly)
    {
        // 1) lerp to enemy (disconnects renderer from collider)
        // 2) do attack
        // 3) get knocked back, lerp renderer back to collider 
        float attackAnimTimer = 0f;

        HealthManager enemyHealth = null;
        FlyManager enemyManager = null;

        if (selfKnockback != 0)
        {
            fly.ReceiveKnockback(-(enemyFly.transform.position - transform.position ) * selfKnockback);
        }
        if (knockback != 0 && enemyFly.TryGetComponent(out enemyManager))
        {
            enemyManager.ReceiveKnockback((enemyFly.transform.position - transform.position ) * knockback);
        }

        if (enemyFly.TryGetComponent(out enemyHealth))
        {
            enemyHealth.TakeDamage(attackDamage);
        }

        Vector3 enemyPosition = enemyFly.transform.position;
        while (attackAnimTimer < AttackAnimTime)
        {   
            if (enemyFly != null)
            {
                enemyPosition = enemyFly.transform.position;
            }
            sprite.transform.position = Vector3.Lerp(transform.position, enemyPosition, (float) Math.Sin(Math.PI * (attackAnimTimer / AttackAnimTime)));
            attackAnimTimer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        sprite.transform.localPosition = Vector3.zero;
        yield return null;
    }

    public bool TryAttack(GameObject enemyFly)
    {
        Debug.Log(name + " is attacking target fly" + enemyFly.name);
        if (Ready && Vector3.Distance(transform.position, enemyFly.transform.position) < AttackRange)
        {
            attackTimer = AttackTime;
            StartCoroutine(DoAttack(enemyFly));
            return true;
        }
        return false;
    }
}