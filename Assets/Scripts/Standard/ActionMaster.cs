using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ActionMaster
{
    public enum Action { Tidy, Reset, EndTurn, EndGame, Undefined };

    public static Action NextAction(List<int> rolls)
    {
        // Create temporary rolls variable to avoid writing out
        //  the 'virtual' 0s.
        var tempRolls = new List<int>(rolls);

        Action nextAction = Action.Undefined;

        // Step through rolls
        for (int i = 0; i < tempRolls.Count; i++)
        {
            if (i == 20)
            {
                nextAction = Action.EndGame;
            }
            // Handle last-frame special cases
            else if (i >= 18 && tempRolls[i] == 10)
            {
                nextAction = Action.Reset;
            }
            else if (i == 19)
            {
                if (tempRolls[18] == 10 && tempRolls[19] == 0)
                {
                    nextAction = Action.Tidy;
                }
                else if (tempRolls[18] + tempRolls[19] == 10)
                {
                    nextAction = Action.Reset;
                }
                // Roll 21 awarded
                else if (tempRolls[18] + tempRolls[19] >= 10)
                {
                    nextAction = Action.Tidy;
                }
                else
                {
                    nextAction = Action.EndGame;
                }
            }
            // First bowl of frame
            else if (i % 2 == 0)
            {
                if (tempRolls[i] == 10)
                {
                    // Insert virtual 0 after strike
                    tempRolls.Insert(i, 0);
                    nextAction = Action.EndTurn;
                }
                else
                {
                    nextAction = Action.Tidy;
                }
            }
            // Second bowl of frame
            else
            {
                nextAction = Action.EndTurn;
            }
        }

        return nextAction;
    }
}