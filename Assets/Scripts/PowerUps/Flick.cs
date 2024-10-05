using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flick : PowerUps
{
    // Start is called before the first frame update
    void Start()
    {
        powerUpType = PowerUpType.Flick;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void activatePowerUp(string self, string enemy)

    {
        Debug.Log("activate flick powerup");
        //get a random direction vector to push opponent in 
        //add velocity/force in that direction
        
    }
}
