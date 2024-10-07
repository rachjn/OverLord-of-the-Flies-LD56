using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Flick : PowerUps
{
    public float force;
    [SerializeField] private GameObject flickPrefab;
    public float moveDuration = 1f;  // Time it takes for the flick to reach the enemy

    void Start()
    {
        powerUpType = PowerUpType.Flick;
    }

    public override IEnumerator activatePowerUp(string self, string enemy)
    {
        Debug.Log("activate flick powerup");

        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag(enemy);

        // Get player's transform
        GameObject player = GameObject.FindGameObjectWithTag(self);
        Transform playerT = player.transform;
        Vector2 enemyPosition = enemyT.transform.position;
        // Determine where to instantiate the flickPrefab (top or bottom)
        Vector2 flickStartPosition;
        if (enemyPosition.y > 0)
        {
            flickStartPosition = new Vector2(enemyPosition.x, enemyPosition.y + 5f);  // Start from top
        }
        else
        {
            flickStartPosition = new Vector2(enemyPosition.x, enemyPosition.y - 5f);  // Start from bottom
        }

        // Instantiate the flickPrefab
        GameObject flickObject = Instantiate(flickPrefab, flickStartPosition, Quaternion.identity);

        // Step 2: Check if the enemy is to the right of the player and flip the flickPrefab
        if (enemyPosition.x > playerT.position.x)
        {
            // Enemy is to the right of the player, flip the X scale of the flickPrefab
            flickObject.transform.localScale = new Vector3(-flickObject.transform.localScale.x, flickObject.transform.localScale.y, flickObject.transform.localScale.z);
        }
        // Otherwise, no need to flip, it flicks to the right by default

        // Step 3: Move the flick object toward the enemy's position
        yield return StartCoroutine(MoveFlick(flickObject, flickStartPosition, enemyPosition));

        // Step 4: Play flick animation (assumed to be handled via Animator on flickPrefab)
        Animator flickAnimator = flickObject.GetComponentInChildren<Animator>();
        if (flickAnimator != null)
        {
            flickAnimator.enabled = true;  // Assumes you have an animation trigger named "PlayFlick"
        }

        // Wait for the animation to finish (assuming 1 second animation time)
        yield return new WaitForSeconds(0.3f);

        // Step 5: Destroy the flick object after the animation
        Destroy(flickObject);
        // Loop through all found GameObjects and access their Transform component
        foreach (GameObject enemyObj in enemyObjects)
        {
            Rigidbody2D enemyRb = enemyObj.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 dir = (enemyObj.transform.position - playerT.position).normalized;
                enemyRb.AddForce(dir * force, ForceMode2D.Impulse);
            }
        }

        yield break;
    }

    // Coroutine to move the flick object from start position to target position
    private IEnumerator MoveFlick(GameObject flickObject, Vector2 startPosition, Vector2 targetPosition)
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / moveDuration;

            // Lerp the flick object towards the target position
            flickObject.transform.position = Vector2.Lerp(startPosition, targetPosition, progress);

            yield return null;  // Wait for the next frame
        }

        // Ensure final position is set exactly to the target
        flickObject.transform.position = targetPosition;
    }
}
