using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    // Start is called before the first frame update
    // public List<PowerUpType> testPlayerPowerUps;
    public Queue<PowerUps> playerPowerUps;
    private int numPowerUps = 0;
    private int maxPowerUps = 3;
    [SerializeField] private string player;
    [SerializeField] private string enemy;
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private int playerControlScheme = 0;
    void Start()
    {
        playerPowerUps = new Queue<PowerUps>();
    }

    public List<PowerUpType> getPowerUps()
    {
        List<PowerUpType> powerUpNames = new List<PowerUpType>();
        foreach (PowerUps p in playerPowerUps)
        {
            powerUpNames.Add(p.getPowerUpType());
        }

        return powerUpNames;
    }
    public bool addPowerUp(PowerUps p)
    {
        if (numPowerUps >= maxPowerUps)
        {
            return false;
        }
        else
        {
            p.setPlayerObject(gameObject);
            p.setEnemyObject(enemyObject);
            playerPowerUps.Enqueue(p);
            numPowerUps++;
            
        }

        return true;
    }

    public void usePowerUp()
    {
        if (numPowerUps == 0)
        {
            return;
        }
        else
        {
            PowerUps powerUpUsed = playerPowerUps.Dequeue();

            // Start coroutine to handle the power-up activation
            StartCoroutine(HandlePowerUpActivation(powerUpUsed));

            // Reduce the number of power-ups
            numPowerUps--;
        }
    }

    private IEnumerator HandlePowerUpActivation(PowerUps powerUpUsed)
    {
        // Wait for the coroutine to finish, if it's valid
        yield return StartCoroutine(powerUpUsed.activatePowerUp(player, enemy));
        Destroy(powerUpUsed.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControlScheme == 1)
        {
            if (Input.GetButtonDown("Power1"))
            {
                usePowerUp();
            }
        }
        else if (playerControlScheme == 2)
        {
            if (Input.GetButtonDown("Power2"))
            {
                usePowerUp();
            }
        }

    }
}
