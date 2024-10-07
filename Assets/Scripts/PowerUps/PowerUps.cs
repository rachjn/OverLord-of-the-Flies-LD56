using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected PowerUpType powerUpType;
    protected GameObject playerT;
    protected GameObject enemyT;

    public void setPlayerObject(GameObject p)
    {
        playerT = p;
    }
    public void setEnemyObject(GameObject e)
    {
        enemyT = e;
    }
    
    public PowerUpType getPowerUpType()
    {
        return powerUpType;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.tag);
        if (col.GetComponent<PlayerController>() && (col.CompareTag("Player1") ||col.CompareTag("Player2")))
        {
            // Debug.Log(col.GetComponent<PlayerItems>() != null);
            if (col.GetComponent<PlayerItems>().addPowerUp(this))
            {
                GetComponent<SpriteRenderer>().enabled = false;

                // Disable the collider to stop further interaction
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    public virtual IEnumerator activatePowerUp(string self, string enemy)
    {
        Debug.Log("activating powerup");
        return null; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
