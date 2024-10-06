using UnityEngine;

public class ColorSelector : MonoBehaviour
{
    // Array of color boxes in the scene
    public GameObject[] colorBoxes;

    // Define the colors you want to cycle through
    private Color[] colors = new Color[]
    {
        new Color(239f / 255f, 78f / 255f, 110f / 255f), // Custom Red (RGB: 239, 78, 110)
        new Color(246f / 255f, 201f / 255f, 100f / 255f), // Custom Green (RGB: 246, 201, 100)
        new Color(112f / 255f, 127f / 255f, 235f / 255f), // Custom Blue (RGB: 112, 127, 235)
        new Color(108f / 255f, 216f / 255f, 106f / 255f), // Custom Yellow (RGB: 108, 216, 106)
    };

    // Variables to store selected colors for Player 1 and Player 2
    public Color player1Color;
    public Color player2Color;

    private int player1Index = 0; // Index for Player 1's selected color
    private int player2Index = 1; // Index for Player 2's selected color (default)

    // Sprite renderers for Player 1 and Player 2
    public SpriteRenderer player1SpriteRenderer;
    public SpriteRenderer player2SpriteRenderer;

    private void Start()
    {
        // Set default colors
        player1Color = colors[player1Index];
        player2Color = colors[player2Index];

        // Update indicators on start
        UpdateIndicators();
        UpdateSpriteColors(); // Set initial sprite colors
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

        // Handle input for Player 2 (Arrow keys)
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CyclePlayer2Color(-1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CyclePlayer2Color(1);
        }

        // Debug output for Player colors
        if (Input.GetKeyDown(KeyCode.Return)) // Enter key
        {
            Debug.Log($"Player 1 Color: {player1Color}");
            Debug.Log($"Player 2 Color: {player2Color}");
        }
    }

    private void CyclePlayer1Color(int direction)
    {
        // Continuously cycle until an available color is found
        do
        {
            player1Index = (player1Index + direction + colors.Length) % colors.Length;
        } while (colors[player1Index] == player2Color); // Skip if it's the same as Player 2's color

        // Assign new color
        player1Color = colors[player1Index];
        UpdateIndicators();
        UpdateSpriteColors(); // Update Player 1 sprite color
    }

    private void CyclePlayer2Color(int direction)
    {
        // Continuously cycle until an available color is found
        do
        {
            player2Index = (player2Index + direction + colors.Length) % colors.Length;
        } while (colors[player2Index] == player1Color); // Skip if it's the same as Player 1's color

        // Assign new color
        player2Color = colors[player2Index];
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
            if (colors[i] == player1Color)
            {
                p1Indicator.SetActive(true); // Activate Player 1 indicator
            }
            if (colors[i] == player2Color)
            {
                p2Indicator.SetActive(true); // Activate Player 2 indicator
            }
        }
    }

    private void UpdateSpriteColors()
    {
        // Update the sprite colors based on the selected colors
        player1SpriteRenderer.color = player1Color;
        player2SpriteRenderer.color = player2Color;
    }
}
