using UnityEngine;

public class TankFly : FlyBase
{
    private void OnCollisionEnter(Collision collision)
    {
        FlyBase otherFly = collision.gameObject.GetComponent<FlyBase>();

        // Check if we collided with another fly that can take damage
        if (otherFly != null)
        {
            TakeDamage(otherFly.attackDamage);  // Tank takes a fixed amount or percentage of damage
            // No damage dealt to the other fly
        }
    }
}

