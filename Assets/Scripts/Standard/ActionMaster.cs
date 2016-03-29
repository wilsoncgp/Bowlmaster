using UnityEngine;
using System.Collections;

public class ActionMaster
{

    public enum Action { Tidy, Reset, EndTurn, EndGame }

    public Action Bowl(int pins)
    {
        if(pins < 0)
        {
            throw new UnityException("Number of pins cannot be less than 0.");
        }
        else if(pins > 10)
        {
            throw new UnityException("Number of pins cannot be greater than 10.");
        }

        // Bowled a strike
        if(pins == 10)
        {
            return Action.EndTurn;
        }

        throw new UnityException("No action specified by given pin count.");
    }
}
