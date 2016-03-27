using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour
{
    public int lastStandingCount = -1;
    public Text pinNumberText;
    public float ballResetTime = 3.0f;
    public float distanceToRaise = 60.0f;

    private Ball ball;
    private bool ballEnteredBox = false;
    private float lastChangeTime = 0f;

    // Use this for initialization
    void Start()
    {
        ball = GameObject.FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ballEnteredBox)
        {
            CheckStanding();
        }
    }

    public void RaisePins()
    {
        // Raise standing pins by given distance
        Debug.Log("Raising pins");

        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if(pin.IsStanding())
            {
                pin.SetUseGravity(false);
                pin.transform.Translate(0, distanceToRaise, 0);
            }
        }
    }

    public void LowerPins()
    {
        Debug.Log("Lowering pins");

        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                pin.transform.Translate(0, -distanceToRaise, 0);
                pin.SetUseGravity(true);
            }
        }
    }

    public void RenewPins()
    {
        Debug.Log("Renewing pins");
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

    void CheckStanding()
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
            if(lastChangeTime >= ballResetTime)
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
        ball.Reset();
        pinNumberText.color = Color.green;
        ballEnteredBox = false;
        lastStandingCount = -1;
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.GetComponent<Ball>())
        {
            pinNumberText.color = Color.red;
            ballEnteredBox = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        // Destroy pins exiting the Pin Setter's collider
        Pin pin = collider.GetComponentInParent<Pin>();
        if(pin)
        {
            Destroy(pin.gameObject);
        }
    }

    
}
