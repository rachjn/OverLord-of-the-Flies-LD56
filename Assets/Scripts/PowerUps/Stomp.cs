using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : PowerUps
{
    // Start is called before the first frame update
    void Start()
    {
        powerUpType = PowerUpType.Stomp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void activatePowerUp(string self, string enemy)
    {
        Debug.Log("activate stomp powerup");

        //start an indicator on map 
        //drop object down
        //on impact stuns everything in impact circle
    }
}
