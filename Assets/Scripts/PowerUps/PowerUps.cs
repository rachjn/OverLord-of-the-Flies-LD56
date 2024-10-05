using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected PowerUpType powerUpType;

    public PowerUpType getPowerUpType()
    {
        return powerUpType;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player1") ||col.CompareTag("Player2") )
        {
            if (col.GetComponent<PlayerItems>().addPowerUp(this))
            {
                Destroy(gameObject);  
            }
        }
    }

    public virtual void activatePowerUp(string self, string enemy)
    {
        Debug.Log("activating powerup");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
