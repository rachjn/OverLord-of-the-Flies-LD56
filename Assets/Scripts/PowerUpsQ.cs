using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PowerUpsQ : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PlayerItems player;
    [SerializeField] private List<GameObject> powerUpSlots;
    [SerializeField] private List<GameObject> powerUpsList;
    public static Dictionary<PowerUpType, GameObject> powerUpSprites = new Dictionary<PowerUpType, GameObject>();
    public List<PowerUpType> playerPowerUps;
    
    
    void Start()
    {
        foreach (GameObject p in powerUpsList)
        {
            powerUpSprites[p.GetComponent<PowerUps>().getPowerUpType()] = p;
        }
    }
    void Update()
    {
        playerPowerUps = player.getPowerUps();
        for (int i = 0; i < powerUpSlots.Count; i++)
        {
            SpriteRenderer renderer = powerUpSlots[i].GetComponent<SpriteRenderer>();
            if (i < playerPowerUps.Count)
            {
                SpriteRenderer sourceRenderer = powerUpSprites[playerPowerUps[i]].GetComponent<SpriteRenderer>();

                // Set the sprite and color
                renderer.sprite = sourceRenderer.sprite;
                renderer.color = sourceRenderer.color;              
            }
            else
            {
                renderer.sprite = null;
            }
        }
    }
}
