using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreDisplay : MonoBehaviour
{
    public Text[] rollScoreTexts;
    public Text[] frameScoreTexts;

    public void FillRollCard(List<int> rolls)
    {
        string formattedRolls = FormatRolls(rolls);
        for (int i = 0; i < formattedRolls.Length; i++)
        {
            rollScoreTexts[i].text = formattedRolls[i].ToString();
        }
    }

    public void FillFrameCard(List<int> frames)
    {
        for (int i = 0; i < frames.Count; i++)
        {
            frameScoreTexts[i].text = frames[i].ToString();
        }
    }

    // Returns a string format of the balls bowled in the game
    public static string FormatRolls(List<int> rolls)
    {
        string rollsString = string.Empty;

        for (int i = 0; i < rolls.Count; i += 2)
        {
            // Bowled a strike
            if (rolls[i] == 10)
            {
                // Add a space if not in last frame
                rollsString += rollsString.Length < 18 ? "X " : "X";
                i--;
            }
            else
            {
                // Add the first bowl
                rollsString += FormatSingleBowl(rolls[i]);

                // If second bowl has been played
                if (rolls.Count > i + 1)
                {
                    // Add spare or second bowl
                    rollsString += rolls[i] + rolls[i + 1] == 10 ? "/" : FormatSingleBowl(rolls[i + 1]);
                }
            }
        }

        return rollsString;
    }

    // Formats a single Bowl to "-" if zero, else returns string equivalent of number.
    private static string FormatSingleBowl(int bowl)
    {
        return bowl != 0 ? bowl.ToString() : "-";
    }
}
