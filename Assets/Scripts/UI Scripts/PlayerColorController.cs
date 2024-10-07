using UnityEngine;

public class PlayerColorController : MonoBehaviour
{
    public SpriteRenderer player1SpriteRenderer; // Reference to Player 1's SpriteRenderer
    public SpriteRenderer player2SpriteRenderer; // Reference to Player 2's SpriteRenderer

    private int currentPlayer1Index = 0;
    private int currentPlayer2Index = 0;

    private Color[] colors = new Color[]
    {
        Color.red,    // Color for Box 0
        Color.blue,   // Color for Box 1
        Color.green,  // Color for Box 2
        Color.yellow   // Color for Box 3
    };

    private void Update()
    {
        // Player 1 Controls
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangePlayer1Color(-1); // Move left
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ChangePlayer1Color(1); // Move right
        }

        // Player 2 Controls
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangePlayer2Color(-1); // Move left
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangePlayer2Color(1); // Move right
        }

        // Set the SpriteRenderer colors based on the selected colors
        UpdateSpriteRendererColors();
    }

    private void ChangePlayer1Color(int direction)
    {
        // Change the current player 1 index with bounds check
        currentPlayer1Index = (currentPlayer1Index + direction + colors.Length) % colors.Length;
        UpdateSpriteRendererColors();
    }

    private void ChangePlayer2Color(int direction)
    {
        // Change the current player 2 index with bounds check
        currentPlayer2Index = (currentPlayer2Index + direction + colors.Length) % colors.Length;
        UpdateSpriteRendererColors();
    }

    private void UpdateSpriteRendererColors()
    {
        // Update the sprite renderers to the selected colors
        player1SpriteRenderer.color = colors[currentPlayer1Index];
        GameManager.Instance.Player1Color = colors[currentPlayer1Index];
        player2SpriteRenderer.color = colors[currentPlayer2Index];
        GameManager.Instance.Player2Color = colors[currentPlayer2Index];
    }
}
