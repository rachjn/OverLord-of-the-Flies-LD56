using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Stomp : PowerUps
{
    // Start is called before the first frame update
    public float growDuration;  // Total duration of the growth in seconds
    public Vector3 finalScale = new Vector3(10f, 10f, 1f);
    [SerializeField] GameObject dropPrefab;
    private SpriteRenderer spriteRenderer;
    private float initialAlpha;
    float finalAlpha = 0.8f;

    void Start()
    {
        powerUpType = PowerUpType.Stomp;
        spriteRenderer = dropPrefab.GetComponent<SpriteRenderer>();
        initialAlpha = 0.36f;
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public override IEnumerator activatePowerUp(string self, string enemy)
    {
        Debug.Log("activate stomp powerup");

        // Find the enemy's position to spawn the indicator
        Vector2 enemyPosition = enemyT.transform.position;

        // Start the indicator on the map and drop the object down
        yield return StartCoroutine(SpawnIndicator(enemyPosition));

    }

    public IEnumerator SpawnIndicator(Vector2 position)
    {
        // Instantiate the circle at the given position
        GameObject newCircle = Instantiate(dropPrefab, position, Quaternion.identity);
        CircleCollider2D circleCollider = newCircle.AddComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;

        // Disable the collider initially, it will be enabled once the circle finishes growing
        circleCollider.enabled = false;

        
        yield return StartCoroutine(GrowCircle(newCircle)); // Wait for the circle to finish growing

        DamageOnTrigger damageTrigger = newCircle.AddComponent<DamageOnTrigger>();
        damageTrigger.enemyTag = "Player2"; // Set the enemy tag to detect enemies inside the circle

        circleCollider.enabled = true;

        damageTrigger.EnableDamage();

        yield return new WaitForSeconds(0.5f);
        Destroy(newCircle);
    }

    // Coroutine to gradually increase the size of the circle
    private IEnumerator GrowCircle(GameObject circle)
    {
        // Get the initial scale (make sure it's Vector3 since localScale uses Vector3)
        Vector3 initialScale = circle.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < growDuration)
        {
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate progress (using a quadratic easing for slow to fast growth)
            float progress = Mathf.Clamp01(elapsedTime / growDuration);
            float scaledProgress = progress * progress;

            // Lerp the scale from initial to final size
            circle.transform.localScale = Vector3.Lerp(initialScale, finalScale, scaledProgress);
            
            float currentAlpha = Mathf.Lerp(initialAlpha, finalAlpha, scaledProgress);
            circle.GetComponent<SpriteRenderer>().color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, currentAlpha);
            yield return null;  // Wait for the next frame
        }
    
        // Ensure final scale is applied after the growth duration
        circle.transform.localScale = finalScale;
    }
    

}