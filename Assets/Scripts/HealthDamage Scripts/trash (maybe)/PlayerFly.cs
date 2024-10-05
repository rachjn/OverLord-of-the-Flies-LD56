using UnityEngine;

public class PlayerFly : HealthManager
{
    private void OnCollisionEnter(Collision collision)
    {
        HealthManager otherFly = collision.gameObject.GetComponent<HealthManager>();

        // Check if we collided with another fly that takes damage
        if (otherFly != null)
        {
            otherFly.TakeDamage(attackDamage);  // Deal damage to the other fly
        }
    }
}
