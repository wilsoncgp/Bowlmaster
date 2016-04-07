using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreMaster
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
            {
                movesSinceStrike++;

                if(movesSinceStrike == 2)
                {
                }
                {
                }
            }

            {
                bowledStrike = true;
                movesSinceStrike = 0;
                strikeFlag = !strikeFlag;
                continue;
            }

            if ((i % 2 == 1) == strikeFlag)
            {
            }
        }

    }
}
