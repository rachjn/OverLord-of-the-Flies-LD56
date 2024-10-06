using Unity.VisualScripting;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float maxHealth;          // Starting health, customizable for each fly
    public float graceTimer = 0.25f; // small delay before death
    private bool dying = false;

    protected float currentHealth;

    void FixedUpdate()
    {
        if (dying)
        {
            graceTimer -= Time.fixedDeltaTime;
            if (graceTimer < 0)
            {
                Die();
            }
        }

    }
    
    protected virtual void Start()
    {
        currentHealth = maxHealth;  // Set initial health when the fly spawns
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            dying = true;
        }
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);  // Remove the fly from the game
    }
}
