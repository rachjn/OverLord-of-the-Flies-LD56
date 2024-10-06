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
    
    public override IEnumerator activatePowerUp(string self, string enemy)
    {
        Debug.Log("activate taunt powerup");

        //taunt animation plays for a few seconds
        // disable enemy movement for a second
        
        enemyT.GetComponent<PlayerMovementTest>().DisablePlayerMovement(1.5f);
        return null;
    }
    
}
