using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Flick : PowerUps
{
    // Start is called before the first frame update
    public float force;
    void Start()
    {
        powerUpType = PowerUpType.Flick;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator activatePowerUp(string self, string enemy)

    {
        Debug.Log("activate flick powerup");
        //get a random direction vector to push opponent in 
        //add velocity/force in that direction
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag(enemy);

        // Loop through all found GameObjects and access their Transform component
        Vector2 dir = (enemyT.transform.position - playerT.transform.position).normalized;
        foreach (GameObject enemyObj in enemyObjects)
        {
            Rigidbody2D enemyRb = enemyObj.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                // Apply a force to the enemy in the direction of 'dir'
                enemyRb.AddForce(dir * force, ForceMode2D.Impulse);
            }
        }
        yield break; 
    }
}
