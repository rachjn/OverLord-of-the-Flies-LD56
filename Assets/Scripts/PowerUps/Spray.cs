using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spray : PowerUps
{
    // Start is called before the first frame update
    void Start()
    {
        powerUpType = PowerUpType.Spray;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void activatePowerUp(string self, string enemy)
    {
        Debug.Log("activate spray powerup");

        // get vector from fly to player
        //inverse vector so it points radially away from player
        //push flies away from player (outside flies get pushed away more closer flies less)
    }
}
