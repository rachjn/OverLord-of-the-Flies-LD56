using Unity.VisualScripting;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public float maxHealth;          // Starting health, customizable for each fly
    public float graceTimer = 0.25f; // small delay before death
    private bool dying = false;
    AudioManager audioManager;

    // private void Awake()
    // {
    //     audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    // }

    protected float currentHealth;

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
    
    protected virtual void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        currentHealth = maxHealth;  // Set initial health when the fly spawns
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        // audioManager.PlaySFX(audioManager.hit);
        if (currentHealth <= 0)
        {
            dying = true;
        }
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " died!");
        audioManager.PlaySFX(audioManager.die);
        Destroy(gameObject);  // Remove the fly from the game
    }
}
