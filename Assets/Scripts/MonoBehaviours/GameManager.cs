using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private List<int> rolls = new List<int>();

    private PinSetter pinSetter;
    private ScoreDisplay scoreDisplay;
    private Ball ball;

    // Use this for initialization
    void Start()
    {
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
        scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
        ball = GameObject.FindObjectOfType<Ball>();
    }

    public void Bowl(int pinFall)
    {
        try
        {
            rolls.Add(pinFall);
            ball.Reset();
            pinSetter.PerformAction(ActionMaster.NextAction(rolls));
        }
        catch
        {
            Debug.LogWarning("Something went wrong inside of GameManager.Bowl()");
        }

        try
        {
            scoreDisplay.FillRollCard(rolls);
            scoreDisplay.FillFrameCard(ScoreMaster.ScoreCumulative(rolls));
        }
        catch
        {
            Debug.LogWarning("ScoreDisplay.FillRollCard() has failed to execute.");
        }
    }
}
