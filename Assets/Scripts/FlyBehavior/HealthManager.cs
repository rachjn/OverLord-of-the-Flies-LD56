using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float maxHealth;            // Starting health
    public float graceTimer = 0.25f;   // Delay before death
    private bool dying = false;
    private float currentHealth;

     public float CurrentHealth    // Public property to access current health
    {
        get { return currentHealth; }
    }

    // public HealthBar healthBar; // Reference to the HealthBar script for Player 1
    // public HealthBar healthBar2; // Reference to the HealthBar script for Player 2 (if needed)

    AudioManager audioManager;

    protected virtual void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        currentHealth = maxHealth;  // Set initial health

        // // Initialize the health bar UI
        // healthBar.UpdateHealthBar(currentHealth, maxHealth);
        // if (healthBar2 != null)
        // {
        //     healthBar2.UpdateHealthBar(currentHealth, maxHealth);
        // }
    }

    void FixedUpdate()
    {
        if (dying)
        {
            graceTimer -= Time.fixedDeltaTime;
            if (graceTimer < 0)
            {
                Die();
            }
        }
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);

        // // Update the health bar UI
        // healthBar.UpdateHealthBar(currentHealth, maxHealth);
        // if (healthBar2 != null)
        // {
        //     healthBar2.UpdateHealthBar(currentHealth, maxHealth);
        // }

        if (currentHealth <= 0)
        {
            dying = true;
        }
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " died!");
        audioManager.PlaySFX(audioManager.die);
        Destroy(gameObject);  // Remove the player from the game
    }
}
