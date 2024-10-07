
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class Spray : PowerUps
{
    // Start is called before the first frame update
    public float force;
    [SerializeField] private GameObject sprayHandPrefab;
    void Start()
    {
        powerUpType = PowerUpType.Spray;
    }

    public override IEnumerator activatePowerUp(string self, string enemy)
    {
        Debug.Log("activate spray powerup");

        // Find enemy objects
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag(enemy);

        // Find the position of the first enemy to determine if the spray hand should come from the top or bottom
        Vector2 enemyPosition = enemyT.transform.position;
        bool fromTop = enemyPosition.y > 0;
        Vector2 sprayStartPosition = fromTop ? new Vector2(enemyPosition.x, enemyPosition.y + 5f) : new Vector2(enemyPosition.x, enemyPosition.y - 5f);

        // Find the spray hand in the scene (assuming it's part of the player or an already assigned GameObject)
        GameObject sprayHand = Instantiate(sprayHandPrefab, sprayStartPosition, Quaternion.identity);  // Adjust this based on your hierarchy

        // Disable player and enemy movement
        enemyT.GetComponent<PlayerController>().DisablePlayerMovement(2.5f);
        playerT.GetComponent<PlayerController>().DisablePlayerMovement(2.5f);

        // Call the spray animation coroutine and wait for it to finish
        yield return StartCoroutine(MoveAndShake(sprayHand, sprayStartPosition, enemyPosition));

        Debug.Log("Animation finished, now spraying.");

        // Keep the spray hand in place and spray while enemies get pushed
        float sprayDuration = 1f; // Duration the spray will last
        yield return StartCoroutine(StayAndSpray(sprayHand, sprayDuration, enemyObjects));

        // Enable player and enemy movement again

        // Destroy the spray hand after the spray completes
        Destroy(sprayHand);

        yield break;
    }

    // Coroutine to handle moving the spray hand to the enemy and shaking it
    private IEnumerator MoveAndShake(GameObject sprayHand, Vector2 sprayStartPosition, Vector2 targetPosition)
    {
        float elapsedTime = 0f;
        float moveDuration = 1f;  // Time it takes to move to the target
        float shakeDuration = 0.5f;  // Time the hand shakes once it reaches the target
        float shakeMagnitudeX = 0.5f;  // The intensity of the shake along the X-axis (S-shape)

        float shakeMagnitudeY = 0.3f;
        float shakeSpeed = 5f;  // Frequency of the shake (controls the speed of the oscillation)

        // Step 1: Move the spray hand towards the target
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / moveDuration;
            sprayHand.transform.position = Vector2.Lerp(sprayStartPosition, targetPosition, progress);
            yield return null;
        }

        // Step 2: S-shaped oscillating shake at the target position
        Vector2 originalPosition = sprayHand.transform.position;
        elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;

            float shakeX = Random.Range(-shakeMagnitudeX, shakeMagnitudeX);
            float shakeY = Random.Range(-shakeMagnitudeY, shakeMagnitudeY);

            // Apply the shake movement to the spray hand
            sprayHand.transform.position = new Vector3(originalPosition.x + shakeX, originalPosition.y + shakeY, 0);

            yield return null;
        }

        // Reset to the original position after shaking
        sprayHand.transform.position = originalPosition;
    }



    // Coroutine to handle spraying while enemies are pushed back
    private IEnumerator StayAndSpray(GameObject sprayHand, float sprayDuration, GameObject[] enemyObjects)
    {
        float elapsedTime = 0f;

        // Get the particle system from the child object "sprayParticles"
        ParticleSystem particleSystem = sprayHand.GetComponentInChildren<ParticleSystem>();

        // Play the particle system at the start of the spray
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
        else
        {
            Debug.LogWarning("Particle system not found on sprayParticles child.");
        }

        // While the hand is spraying, push enemies away
        while (elapsedTime < sprayDuration)
        {
            elapsedTime += Time.deltaTime;

            // Push enemies away while the spray hand stays in place
            foreach (GameObject enemyObj in enemyObjects)
            {
                if (enemyObj == null || enemyObj.GetComponent<PlayerController>())
                {
                    continue; // Ignore enemy player, only scatter their flies
                }

                Vector2 displacement = (enemyObj.transform.position - sprayHand.transform.position).normalized;
                FlyManager fly;
                if (enemyObj.TryGetComponent(out fly))
                {
                    fly.ReceiveKnockback(displacement/75f);
                }
            }

            yield return null;  // Wait for the next frame
        }

        // Stop the particle system after spraying is done (optional)
        if (particleSystem != null)
        {
            particleSystem.Stop();
        }
    }

}
