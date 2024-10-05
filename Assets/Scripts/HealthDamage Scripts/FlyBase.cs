using UnityEngine;

public class FlyBase : MonoBehaviour
{
    public float maxHealth;          // Starting health, customizable for each fly
    public float attackDamage;       // Attack Damage, customizable for each fly

    protected float currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth;  // Set initial health when the fly spawns
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);  // Remove the fly from the game
    }

    // Attacking other flies when colliding
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        FlyBase otherFly = collision.gameObject.GetComponent<FlyBase>();

        Debug.Log("Collision detected with: " + collision.gameObject.name); // Debug log for collision detection

        // Check if we collided with another fly that takes damage
        if (otherFly != null)
        {
            otherFly.TakeDamage(attackDamage);  // Deal damage to the other fly
            Debug.Log(collision.gameObject.name + " took " + attackDamage + " damage");
        }
    }
}
