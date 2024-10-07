using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Spray : PowerUps
{
    // Start is called before the first frame update
    public float force;
    void Start()
    {
        powerUpType = PowerUpType.Spray;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override IEnumerator activatePowerUp(string self, string enemy)
    {
        Debug.Log("activate spray powerup");

        // get vector from fly to player
        //inverse vector so it points radially away from player
        //push flies away from player (outside flies get pushed away more closer flies less)
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag(enemy);

        // Loop through all found GameObjects and access their Transform component
        foreach (GameObject enemyObj in enemyObjects)
        {
            if (enemyObj.GetComponent<PlayerController>())
            {
                continue; //ignore enemy player, only scatter their flies
            }

            Vector2 displacement = enemyObj.transform.position - enemyT.transform.position;
            Vector2 dir = displacement.normalized;
            float distance = displacement.magnitude;
            Rigidbody2D enemyRb = enemyObj.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                // Apply a force to the enemy in the direction of 'dir'
                enemyRb.AddForce(dir * distance * force , ForceMode2D.Impulse);
            }
        }

        yield break;
    }
}
