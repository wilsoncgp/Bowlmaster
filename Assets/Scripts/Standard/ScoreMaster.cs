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

        //for(int i= 0; i < rolls.Count; i++)
        //{

        //}

        return frameList;
    }
}
