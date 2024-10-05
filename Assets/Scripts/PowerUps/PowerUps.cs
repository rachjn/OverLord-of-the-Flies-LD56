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
        if (col.CompareTag("Player"))
        {
            if (col.GetComponent<PlayerItems>().addPowerUp(powerUpType))
            {
                Destroy(gameObject);  
            }
        }
    }

    public virtual void activatePowerUp()
    {
        Debug.Log("activating powerup");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
