using System.Collections;
using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    public string enemyTag;  // The tag of the enemy objects
    private bool damageApplied = false;  // Ensure damage is applied once

    // This will be called when the enemy enters the circle trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object has the enemy tag and hasn't been damaged yet
        if (!damageApplied && other.CompareTag(enemyTag))
        {
            // Apply damage to the enemy's HealthManager component
            HealthManager healthManager = other.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(1f);  // Apply 1 damage (adjust as needed)
                StartCoroutine(ShakeAndTurnRed(other.gameObject));
            }
        }
    }

    // This can be called once the circle is fully grown
    public void EnableDamage()
    {
        damageApplied = false;  // Reset flag so damage can be applied when the circle grows
    }
    
    private IEnumerator ShakeAndTurnRed(GameObject enemyObj)
    {
        // Get the original position and sprite color
        Transform enemyTransform = enemyObj.transform;
        Vector3 originalPosition = enemyTransform.position;

        SpriteRenderer spriteRenderer = enemyObj.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;

        // Turn the enemy red
        spriteRenderer.color = Color.yellow;

        // Shake and color change duration (0.5 seconds total)
        float shakeDuration = 0.5f;
        float elapsedShakeTime = 0f;
        float shakeMagnitude = 0.2f;

        // Perform shaking 3-4 times (you can adjust this based on duration)
        while (elapsedShakeTime < shakeDuration)
        {
            elapsedShakeTime += Time.deltaTime;

            // Create a horizontal oscillation (left and right)
            float offsetX = Mathf.Sin(elapsedShakeTime * 20f) * shakeMagnitude;
            enemyTransform.position = new Vector3(originalPosition.x + offsetX, originalPosition.y, originalPosition.z);

            // Wait for the next frame
            yield return null;
        }

        enemyTransform.position = originalPosition;
        spriteRenderer.color = originalColor;

        yield break;
    }
}