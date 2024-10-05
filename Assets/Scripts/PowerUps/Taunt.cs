using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taunt : PowerUps
{
    // Start is called before the first frame update
    void Start()
    {
        powerUpType = PowerUpType.Taunt;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void activatePowerUp(string self, string enemy)
    {
        Debug.Log("activate taunt powerup");

      //play animation
      //disable opponnent movement and actions
    }
}
