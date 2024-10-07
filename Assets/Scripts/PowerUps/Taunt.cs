using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Taunt : PowerUps
{
    // Start is called before the first frame update
    [SerializeField] float tauntAnimationLength;
    [SerializeField] private float stunLength;
    [SerializeField] private GameObject StunCutScene;
    void Start()
    {
        powerUpType = PowerUpType.Taunt;
    }
    
    public override IEnumerator activatePowerUp(string self, string enemy)
    {
        Debug.Log("activate taunt powerup");

        // Pause game by disabling all objects except Taunt-related objects
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        playerT.GetComponent<PlayerController>().DisablePlayerMovement(tauntAnimationLength);
        enemyT.GetComponent<PlayerController>().DisablePlayerMovement(tauntAnimationLength+stunLength);
        // foreach (GameObject obj in allObjects)
        // {
        //     if (obj.GetComponent<Taunt>())
        //     {
        //         continue;  // Skip Taunt objects
        //     }
        //     obj.SetActive(false);  // Disable all other objects
        // }

        // Start the cutscene coroutine
        yield return StartCoroutine(handleTauntCutscene(allObjects));
    }

    private IEnumerator handleTauntCutscene(GameObject[] allObjects)
    {
        float elapsedTime = 0f;

        // Load the cutscene scene additively
        GameObject scene = Instantiate(StunCutScene, Vector2.zero, quaternion.identity);
        
        // Run the timer for the duration of the taunt animation
        while (elapsedTime < tauntAnimationLength)
        {
            elapsedTime += Time.deltaTime;
            yield return null;  // Wait for the next frame
        }

        // Unload the cutscene after the timer is complete
        Destroy(scene);
    }

    
}
