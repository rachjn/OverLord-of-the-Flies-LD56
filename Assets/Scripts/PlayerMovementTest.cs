using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    public float speed = 5f;

    public float
        driftFactor = 0.9f; // How quickly the player slows down (1 = no slowdown, closer to 0 = quicker slowdown)

    public int playerControlScheme;
    public bool isStunned = false;
    private Vector2 movement;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Handle input based on the assigned control scheme
        if (playerControlScheme == 1)
        {
            movement.x = Input.GetAxisRaw("Horizontal1");
            movement.y = Input.GetAxisRaw("Vertical1");
        }
        else if (playerControlScheme == 2)
        {
            movement.x = Input.GetAxisRaw("Horizontal2");
            movement.y = Input.GetAxisRaw("Vertical2");
        }

        // Normalize the movement vector so diagonal movement isn't faster
        if (!isStunned)
        {
            movement = movement.normalized;
        }
    }

    void FixedUpdate()
    {
        if (!isStunned)
        {
            if (movement.magnitude > 0)
            {
                // Set the velocity directly for instant movement when there is input
                rb.velocity = movement * speed;
            }
            else
            {
                // Gradually reduce velocity when no input is provided, simulating drift
                rb.velocity = rb.velocity * driftFactor;
            }
        }
    }

    public void DisablePlayerMovement(float duration)
    {
        StartCoroutine(DisableMovementCoroutine(duration));
    }

    private IEnumerator DisableMovementCoroutine(float duration)
    {
        float oldSpeed = speed;
        speed = 0f;
        rb.velocity = rb.velocity * 0;
        yield return new WaitForSeconds(duration);
        speed = oldSpeed;
    }

}