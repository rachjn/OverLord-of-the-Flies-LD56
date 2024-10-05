using UnityEngine;

public class MeleeFly : FlyBase
{
    private void OnCollisionEnter(Collision collision)
    {
        FlyBase otherFly = collision.gameObject.GetComponent<FlyBase>();

        // Check if we collided with another fly that takes damage
        if (otherFly != null)
        {
            otherFly.TakeDamage(attackDamage);  // Deal damage to the other fly
        }
    }
}
