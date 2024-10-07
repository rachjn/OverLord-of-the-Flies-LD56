using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public HealthManager healthManager1; // Reference to Player 1's HealthManager
    public HealthManager healthManager2; // Reference to Player 2's HealthManager
    public Image healthBarImage1; // Reference to the Image component for Player 1's health bar
    public Image healthBarImage2; // Reference to the Image component for Player 2's health bar

    private float player1Health;
    private float player2Health;

   void Start()
{
    // Check if health managers are assigned; if not, find them by tag
    if (healthManager1 == null)
    {
        GameObject player1Object = GameObject.FindGameObjectWithTag("Player1");
        if (player1Object != null)
        {
            healthManager1 = player1Object.GetComponent<HealthManager>();
            Debug.Log("Player 1 Health Manager found!");
        }
        else
        {
            Debug.LogError("Player 1 GameObject not found with tag 'Player1'");
        }
    }

    if (healthManager2 == null)
    {
        GameObject player2Object = GameObject.FindGameObjectWithTag("Player2");
        if (player2Object != null)
        {
            healthManager2 = player2Object.GetComponent<HealthManager>();
            if (healthManager2 != null)
            {
                Debug.Log("Player 2 Health Manager found!");
            }
            else
            {
                Debug.LogError("HealthManager component not found on Player 2 GameObject!");
            }
        }
        else
        {
            Debug.LogError("Player 2 GameObject not found with tag 'Player2'");
        }
    }
}


    void Update()
    {
        // Access the current health of both players every frame
        if (healthManager1 != null)
        {
            player1Health = healthManager1.CurrentHealth;
            healthBarImage1.fillAmount = player1Health / healthManager1.maxHealth; // Assuming you have maxHealth in HealthManager
            Debug.Log("Player 1 Health: " + player1Health);
        }
        else
        {
            Debug.LogWarning("Health Manager 1 is null!");
        }

        if (healthManager2 != null)
        {
            player2Health = healthManager2.CurrentHealth;
            healthBarImage2.fillAmount = player2Health / healthManager2.maxHealth; // Assuming you have maxHealth in HealthManager
            Debug.Log("Player 2 Health: " + player2Health);
        }
        else
        {
            Debug.LogWarning("Health Manager 2 is null!");
        }
    }
}
