using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinCounter : MonoBehaviour
{
    public Text pinNumberText;
    public float ballResetTime = 3.0f;

    private GameManager gameManager;
    private bool ballLeftBox = false;
    private int lastStandingCount = -1;
    private int lastSettledCount = 10;
    private float lastChangeTime = 0f;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ballLeftBox)
        {
            UpdateStandingPins();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.GetComponent<Ball>())
        {
            pinNumberText.color = Color.red;
            ballLeftBox = true;
        }
    }

    public void Reset()
    {
        lastSettledCount = 10;
    }

    // Count the standing pins
    int CountStanding()
    {
        int count = 0;
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            count += pin.IsStanding() ? 1 : 0;
        }

        return count;
    }

    void UpdateStandingPins()
    {
        // Grab the current standing count
        int standingCount = CountStanding();

        // If the count remains the same since the last frame,
        //  increase the change time by the delta
        if (lastStandingCount == standingCount)
        {
            lastChangeTime += Time.deltaTime;

            // If the number of standing pins hasn't changed since
            //  'ballResetTime' seconds ago, settle the number of pins.
            if (lastChangeTime >= ballResetTime)
            {
                PinsHaveSettled();
            }
        }
        else
        {
            // If the count has changed, change the count on the UI,
            //  reset the change time and set the last standing count
            //  to this frame's standing count.
            pinNumberText.text = standingCount.ToString();
            lastChangeTime = 0f;
            lastStandingCount = standingCount;
        }
    }

    // Method to settle the number of pins
    // Will reset the ball and make the PinSetter
    //  ready for the next roll of the bowling ball.
    void PinsHaveSettled()
    {
        int standing = CountStanding();
        int pinFall = lastSettledCount - standing;
        lastSettledCount = standing;

        gameManager.Bowl(pinFall);

        pinNumberText.color = Color.green;
        ballLeftBox = false;
        lastStandingCount = -1;
    }
}
