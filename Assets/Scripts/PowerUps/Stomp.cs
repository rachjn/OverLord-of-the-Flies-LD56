using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Stomp : PowerUps
{
    // Start is called before the first frame update
    public float growDuration;  // Total duration of the growth in seconds
    public Vector3 finalScale = new Vector3(2.5f, 2.5f, 1f);
    [SerializeField] GameObject dropPrefab;
    [SerializeField] private GameObject stompHandPrefab;
    [SerializeField] private float fallDuration;
    private SpriteRenderer spriteRenderer;
    private float initialAlpha;
    float finalAlpha = 0.8f;

    void Start()
    {
        powerUpType = PowerUpType.Stomp;
        spriteRenderer = dropPrefab.GetComponent<SpriteRenderer>();
        initialAlpha = 0.36f;
    }
    
    
      public override IEnumerator activatePowerUp(string self, string enemy)
    {
        Debug.Log("activate stomp powerup");

        // Find the enemy's position to spawn the indicator
        Vector2 enemyPosition = enemyT.transform.position;

        // Start the indicator on the map
        yield return StartCoroutine(SpawnIndicator(enemyPosition, enemyT));
    }

      public IEnumerator SpawnIndicator(Vector2 position, GameObject enemy)
      {
          // Instantiate the circle at the given position
          GameObject newCircle = Instantiate(dropPrefab, position, Quaternion.identity);
          

          // Start growing the indicator and simultaneously drop the stomp hand in the last second
          yield return StartCoroutine(GrowCircle(newCircle, position, enemy.tag));
      }

      // Coroutine to gradually increase the size of the circle, and drop the stomp hand in the last second
    private IEnumerator GrowCircle(GameObject circle, Vector2 position, string enemy)
    {
        // Get the initial scale (make sure it's Vector3 since localScale uses Vector3)
        Vector3 initialScale = circle.transform.localScale;
        float elapsedTime = 0f;
        float dropStartTime = growDuration - 0.2f;  // When to start dropping the hand (last second)
        bool stompHandDropped = false;  // Flag to prevent multiple starts

        while (elapsedTime < dropStartTime)
        {
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate progress (using a quadratic easing for slow to fast growth)
            float progress = Mathf.Clamp01(elapsedTime / dropStartTime);
            float scaledProgress = progress * progress;

            // Lerp the scale from initial to final size
            circle.transform.localScale = Vector3.Lerp(initialScale, finalScale, scaledProgress);
            
            // Lerp the alpha for the circle to become more visible over time
            SpriteRenderer spriteRenderer = circle.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                float currentAlpha = Mathf.Lerp(initialAlpha, finalAlpha, scaledProgress);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, currentAlpha);
            }

            yield return null;  // Wait for the next frame
        }
        yield return StartCoroutine(DropStompHand(position, circle, enemy));  // Start dropping the stomp hand
        
        
    }

    // Remove (hide) the sprite from the circle but keep the collider
    private void RemoveCircleSprite(GameObject circle)
    {
        circle.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Drop the stomp hand and shake it on impact, also make the indicator disappear right before hitting
    public IEnumerator DropStompHand(Vector2 position, GameObject indicator, string enemy)
    {
        // Set the position of the stomp hand above the enemy
        Vector2 spawnPosition = new Vector2(position.x, position.y + 10f);  // Adjust height as needed

        // Instantiate the stomp hand object above the enemy
        GameObject stompHand = Instantiate(stompHandPrefab, spawnPosition, Quaternion.identity);

        // You can add a simple falling effect (by moving it downwards over time)
        float elapsedTime = 0f;

        Vector2 startPosition = spawnPosition;
        Vector2 endPosition = position;  // The final position is the enemy's position

        while (elapsedTime < fallDuration)
        {
            // Move the stomp hand down over time
            stompHand.transform.position = Vector2.Lerp(startPosition, endPosition, elapsedTime / fallDuration);
            elapsedTime += Time.deltaTime;

            // Make the indicator disappear right before the fist hits (around 90% of fall time)
            if (elapsedTime >= fallDuration * 0.9f)
            {
                RemoveCircleSprite(indicator);  // Hide the indicator's sprite but keep its collider
            }

            yield return null;
        }

        // Ensure the stomp hand reaches the enemy
        stompHand.transform.position = endPosition;

        // Shake effect on impact
        yield return StartCoroutine(ShakeStompHand(stompHand, indicator, enemy));
        Destroy(stompHand); 
    }

    
    // Coroutine to shake the stomp hand slightly on impact
    [SerializeField] private float shakeDuration = 0.2f;  // Duration of the shake (now serialized)

    private IEnumerator ShakeStompHand(GameObject stompHand, GameObject circle, String enemy)
    {
        Vector3 originalPosition = stompHand.transform.position;
        float shakeMagnitude = 0.1f;  // How much the hand shakes

        float elapsedTime = 0f;
    
        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Create a random small offset for the shake effect
            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);

            // Apply the shake effect to the stomp hand's position
            stompHand.transform.position = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);
            yield return null;  // Wait for the next frame
        }
        
        // After shaking, reset the position to the original
        stompHand.transform.position = originalPosition;
        // Ensure final scale is applied after the growth duration
        CircleCollider2D circleCollider = circle.AddComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;
        circle.GetComponent<CircleCollider2D>().enabled = true;
        DamageOnTrigger damageTrigger = circle.AddComponent<DamageOnTrigger>();
        Debug.Log(enemy);
        damageTrigger.enemyTag = enemy;  // Set the enemy tag to detect enemies inside the circle
        // Wait for a short duration to allow the damage to be applied
        yield return new WaitForSeconds(0.05f);
        Destroy(circle);

        
    }



}