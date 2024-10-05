using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    // Start is called before the first frame update
    // public List<PowerUpType> testPlayerPowerUps;
    public Queue<PowerUpType> playerPowerUps;
    private int numPowerUps = 0;
    private int maxPowerUps = 3;
    [SerializeField] private int playerControlScheme = 0;

    void Start()
    {
        playerPowerUps = new Queue<PowerUpType>();
    }

    public List<PowerUpType> getPowerUps()
    {
        return playerPowerUps.ToList();
    }
    public bool addPowerUp(PowerUpType p)
    {
        if (numPowerUps >= maxPowerUps)
        {
            return false;
        }
        else
        {
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
            playerPowerUps.Dequeue();
            //call powerup invojking funct8ion 
            numPowerUps--;
        }
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
