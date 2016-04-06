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

        foreach(int frame in frameScores)
        {
            currentCumulativeScore += frame;
            scoreList.Add(currentCumulativeScore);
        }

        return scoreList;
    }

    // Return a list of individual frame scores, not cumulative
    public static List<int> ScoreFrames(List<int> rolls)
    {
        var frameList = new List<int>();

        bool strikeFlag = true;
        bool bowledStrike = false;
        int movesSinceStrike = 0;
        bool bowledSpare = false;

        for (int i = 0; i < rolls.Count; i++)
        {
            if(bowledStrike)
            {
                movesSinceStrike++;

                if(movesSinceStrike == 2)
                {
                    frameList.Add(rolls[i] + rolls[i - 1] + 10);
                    
                    if (rolls[i - 1] == 10)
                    {
                        bowledStrike = true;
                        movesSinceStrike = 1;
                        strikeFlag = !strikeFlag;
                        continue;
                    }

                    bowledStrike = false;
                }
                else
                {
                    continue;
                }
            }
            else if(bowledSpare)
            {
                frameList.Add(rolls[i] + 10);
                bowledSpare = false;
            }

            if (rolls[i] == 10)
            {
                bowledStrike = true;
                movesSinceStrike = 0;
                strikeFlag = !strikeFlag;
                continue;
            }

            if ((i % 2 == 1) == strikeFlag)
            {
                int combinedScore = rolls[i] + rolls[i - 1];
                if(combinedScore == 10)
                {
                    bowledSpare = true;
                }
                else if(frameList.Count < 10)
                {
                    frameList.Add(rolls[i] + rolls[i - 1]);
                }
            }
        }

        return frameList;
    }
}
