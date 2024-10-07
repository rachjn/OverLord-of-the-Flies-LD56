using UnityEngine;
using UnityEngine.SceneManagement;


public class ColorSelector : MonoBehaviour
{
    // Array of color boxes in the scene
    public GameObject[] colorBoxes;

    // Sprites for the different colors
    public Sprite[] player1Sprites; // Array for Player 1 color sprites
    public Sprite[] player2Sprites; // Array for Player 2 color sprites

    // Index variables for Player 1 and Player 2
    private int player1Index = 0; // Index for Player 1's selected sprite
    private int player2Index = 1; // Index for Player 2's selected sprite (default)

    // Sprite renderers for Player 1 and Player 2
    public SpriteRenderer player1SpriteRenderer;
    public SpriteRenderer player2SpriteRenderer;

    private void Start()
    {
        // Set default sprites
        UpdateSpriteColors(); // Set initial sprite colors
        UpdateIndicators(); // Update indicators on start
    }

    private void Update()
    {
        // Handle input for Player 1 (A and D keys)
        if (Input.GetKeyDown(KeyCode.A))
        {
            CyclePlayer1Color(-1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            CyclePlayer1Color(1);
        }

        // Handle input for Player 2 (J AND L)
        if (Input.GetKeyDown(KeyCode.J))
        {
            CyclePlayer2Color(-1);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            CyclePlayer2Color(1);
        }

        // Debug output for Player colors
        if (Input.GetKeyDown(KeyCode.Return)) // Enter key
        {
            Debug.Log($"Player 1 Index: {player1Index}");
            Debug.Log($"Player 2 Index: {player2Index}");
            SceneManager.LoadScene("RulesScene");  // uncomment for build
        }
    }

    private void CyclePlayer1Color(int direction)
    {
        // Continuously cycle until an available sprite is found
        do
        {
            player1Index = (player1Index + direction + player1Sprites.Length) % player1Sprites.Length;
        } while (player1Index == player2Index); // Skip if it's the same as Player 2's sprite

        UpdateIndicators();
        UpdateSpriteColors(); // Update Player 1 sprite color
    }

    private void CyclePlayer2Color(int direction)
    {
        // Continuously cycle until an available sprite is found
        do
        {
            player2Index = (player2Index + direction + player2Sprites.Length) % player2Sprites.Length;
        } while (player2Index == player1Index); // Skip if it's the same as Player 1's sprite

        UpdateIndicators();
        UpdateSpriteColors(); // Update Player 2 sprite color
    }

    private void UpdateIndicators()
    {
        // Loop through each color box and update the indicators
        for (int i = 0; i < colorBoxes.Length; i++)
        {
            GameObject p1Indicator = colorBoxes[i].transform.Find("Player1Indicator").gameObject;
            GameObject p2Indicator = colorBoxes[i].transform.Find("Player2Indicator").gameObject;

            // Deactivate both indicators initially
            p1Indicator.SetActive(false);
            p2Indicator.SetActive(false);

            // Activate indicators based on selected colors
            if (i == player1Index)
            {
                p1Indicator.SetActive(true); // Activate Player 1 indicator
            }
            if (i == player2Index)
            {
                p2Indicator.SetActive(true); // Activate Player 2 indicator
            }
        }
    }

    private void UpdateSpriteColors()
    {
        // Update the sprite renderers with the currently selected sprites
        player1SpriteRenderer.sprite = player1Sprites[player1Index];
        player2SpriteRenderer.sprite = player2Sprites[player2Index];
    }
}
