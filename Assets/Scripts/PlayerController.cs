using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    public float sprintMultiplier = 2f;
    public float maxStamina = 3f; 
    public float staminaRegen = 1f;
    public LayerMask itemLayer;
    public float staminaRegenDelay = 2f;
    public int playerControlScheme;

    private float staminaRegenTimer = 0f;
    private float stamina = 0f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private SwarmManager swarm;

    private bool sprinting = false;
    private bool sprintHeld = false;

    void Start()
    {
        StartCoroutine(OpenEggsCoroutine());
        swarm = GetComponentInChildren<SwarmManager>();
        rb = GetComponent<Rigidbody2D>();

        var color = (tag == "Player1") ? GameManager.Instance.Player1Color : GameManager.Instance.Player2Color;
        GetComponent<SpriteRenderer>().color = color;
    }

    void Update()
    {
        // Handle input based on the assigned control scheme
        if (playerControlScheme == 1)
        {
            movement.x = Input.GetAxisRaw("Horizontal1");
            movement.y = Input.GetAxisRaw("Vertical1");

            sprintHeld = Input.GetAxisRaw("Sprint1") > 0;
            swarm.Attacking = Input.GetAxisRaw("Attack1") > 0;
            swarm.Retreating = Input.GetAxisRaw("Retreat1") > 0;
        }
        else if (playerControlScheme == 2)
        {
            movement.x = Input.GetAxisRaw("Horizontal2");
            movement.y = Input.GetAxisRaw("Vertical2");

            sprintHeld = Input.GetAxisRaw("Sprint2") > 0;
            swarm.Attacking = Input.GetAxisRaw("Attack2") > 0;
            swarm.Retreating = Input.GetAxisRaw("Retreat2") > 0;
        }
    }

    void FixedUpdate()
    {
        // rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        bool sprinting = false;
        if (sprintHeld && stamina > 0)
        {
            stamina -= Time.fixedDeltaTime;
            staminaRegenTimer = staminaRegenDelay;
            sprinting = true;
        }
        else 
        {
            staminaRegenTimer = Math.Max(0, staminaRegenTimer - Time.fixedDeltaTime);
            if (staminaRegenTimer <= 0)
            {
                stamina = Math.Clamp(stamina + staminaRegen * Time.deltaTime, 0, maxStamina);
            }
        }
        
        Vector2 force = movement * speed * (sprinting ? sprintMultiplier : 1f);
        rb.AddForce(force);
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

    private IEnumerator OpenEggsCoroutine()
    {
        while (true)
        {
            var items = Physics2D.OverlapCircleAll(transform.position, 1.5f, itemLayer);
            EggManager egg = null;
            // Debug.Log(items.Length);
            foreach (Collider2D item in items)
            {
                egg = null;
                if (item.gameObject.TryGetComponent(out egg))
                {
                    egg.OpenEgg(tag);
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
}