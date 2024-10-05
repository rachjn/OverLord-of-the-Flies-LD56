using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public int playerControlScheme;

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
    }

    void FixedUpdate()
    {
        // rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        
        rb.AddForce(movement * speed);
    }
    public void DisablePlayerMovement(float duration)
    {
        StartCoroutine(DisableMovementCoroutine(duration));
    }

    private IEnumerator DisableMovementCoroutine(float duration)
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is null in DisableMovementCoroutine.");
            yield break;  // Exit the coroutine if Rigidbody is not assigned
        }
        float oldSpeed = speed;
        speed = 0f;
        rb.velocity = Vector2.zero;  // Reset velocity immediately

        yield return new WaitForSeconds(duration);
        speed = oldSpeed;
    }
}