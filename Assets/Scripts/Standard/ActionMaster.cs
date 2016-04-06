using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionMaster
{
    public enum Action { Tidy, Reset, EndTurn, EndGame }

    private int[] bowls = new int[21];
    private int bowl = 1;

    public static Action NextAction(List<int> pinFalls)
    {
        ActionMaster actionMaster = new ActionMaster();
        Action currentAction = Action.EndGame;

        foreach(int pinFall in pinFalls)
        {
            currentAction = actionMaster.Bowl(pinFall);
        }

        return currentAction;
    }

    private Action Bowl(int pins)
    {
        // Throw exception if an invalid number of pins for a bowl
        if(pins < 0)
        {
            throw new UnityException("Number of pins cannot be less than 0.");
        }
        else if(pins > 10)
        {
            throw new UnityException("Number of pins cannot be greater than 10.");
        }

        // Store value for bowl
        bowls[bowl - 1] = pins;
        
        // If first bowl of last frame and bowled a strike
        //  it must reset the pins
        if(bowl == 19 && pins == 10)
        {
            bowl++;
            return Action.Reset;
        }

        // If it's the second bowl of the last frame
        if(bowl == 20)
        {
            // If a strike or 0,10 spare was bowled, reset the pins
            if(pins == 10)
            {
                bowl++;
                return Action.Reset;
            }
            // If a strike was bowled in previous bowl
            //  but not this bowl, tidy the pins
            else if (bowls[bowl - 2] == 10)
            {
                bowl++;
                return Action.Tidy;
            }
            // If a strike wasn't bowled in this or the last bowl,
            //  but a spare was bowled across the first and second bowl
            //  of the last frame, reset the pins
            else if(bowls[bowl - 2] + pins == 10)
            {
                bowl++;
                return Action.Reset;
            }
            else 
            {
                return Action.EndGame;
            }
        }

        // If third bowl of final frame, must be final bowl
        //  so end the game
        if(bowl == 21)
        {
            return Action.EndGame;
        }
        
        // If in first bowl of frame
        if(bowl % 2 == 1)
        {
            // Bowled a strike
            if(pins == 10)
            {
                bowl += 2;
                return Action.EndTurn;
            }
            else
            {
                bowl++;
                return Action.Tidy;
            }
        }
        else if(bowl % 2 == 0)
        {
            bowl++;
            return Action.EndTurn;
        }

        throw new UnityException("No action specified by given pin count for specific bowl.");
    }
}
