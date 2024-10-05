using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TauntAnimation : MonoBehaviour
{
    public float speed = 2f;  // Speed at which the object moves
    public bool startMoving = false;  // Control when the movement begins

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Reference the Animator
        if (animator != null)
        {
            animator.Play("YourAnimationName"); // Play the animation
        }
    }

    void Update()
    {
        // Check if the cutscene has started (when startMoving is true)
        if (startMoving)
        {
            // Move the GameObject in the x-direction over time
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    // You can call this function to start the cutscene/movement
    public void StartCutscene()
    {
        startMoving = true;
    }

    // Optionally, stop the movement if needed
    public void StopCutscene()
    {
        startMoving = false;
    }
}
