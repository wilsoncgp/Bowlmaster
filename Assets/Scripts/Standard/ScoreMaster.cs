using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ScoreMaster
{
    // Returns a list of cumulative scores
    // like a normal score card
    public static List<int> ScoreCumulative(List<int> rolls)
    {
        var scoreList = new List<int>();

        var frameScores = ScoreFrames(rolls);
        int currentCumulativeScore = 0;

        foreach (int frame in frameScores)
        {
            currentCumulativeScore += frame;
            scoreList.Add(currentCumulativeScore);
        }

        return scoreList;
    }

    // Return a list of individual frame scores, not cumulative
    public static List<int> ScoreFrames(List<int> rolls)
    {
        var frameScores = new List<int>();

        // Loop through by default two rolls at a time
        for (int i = 1; i < rolls.Count && frameScores.Count < 10; i += 2)
        {
            // Grab what could be a full frame's score
            int score = rolls[i] + rolls[i - 1];

            // If the score is greater than or equal to 10, a strike or spare has been scored over the two rolls
            if (score >= 10)
            {
                // Add the frame score if the next roll exists
                if (rolls.Count > i + 1)
                {
                    frameScores.Add(score + rolls[i + 1]);
                }

                // If the previous roll was a strike, set the counter back 1
                //  to account for the missing roll.
                if (rolls[i - 1] == 10)
                {
                    i--;
                }
            }
            else
            {
                // If the score is less than 10, it's a regular frame so just add score
                frameScores.Add(score);
            }
        }

        return frameScores;
    }
}
