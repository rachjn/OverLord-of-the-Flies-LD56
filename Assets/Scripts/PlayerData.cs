
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class PlayerData
{
    public static Color player1Color;
    public static Color player2Color;

    public static void ResetColors(Color initialPlayer1Color, Color initialPlayer2Color)
    {
        player1Color = initialPlayer1Color;
        player2Color = initialPlayer2Color;
    }
}
