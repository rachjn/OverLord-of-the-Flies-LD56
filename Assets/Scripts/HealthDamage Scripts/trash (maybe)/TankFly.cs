using UnityEngine;

public class TankFly : HealthManager
{
    private void OnCollisionEnter(Collision collision)
    {
        HealthManager otherFly = collision.gameObject.GetComponent<HealthManager>();

        // Check if we collided with another fly that can take damage
        if (otherFly != null)
        {
            TakeDamage(otherFly.attackDamage);  // Tank takes a fixed amount or percentage of damage
            // No damage dealt to the other fly
        }
    }
}

