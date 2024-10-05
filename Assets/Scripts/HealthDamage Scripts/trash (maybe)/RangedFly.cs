using UnityEngine;

public class RangedFly : HealthManager
{
    private void OnCollisionEnter(Collision collision)
    {
        HealthManager otherFly = collision.gameObject.GetComponent<HealthManager>();

        // Check if we collided with another fly that can take damage
        if (otherFly != null)
        {
            TakeDamage(otherFly.attackDamage);  // RangedFly only takes damage
        }
    }

    // Function to handle projectile-based damage in the future
    public void TakeProjectileDamage(float damage)
    {
        TakeDamage(damage);  // Handle damage from projectiles
    }

    
}
