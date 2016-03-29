using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour
{
    public int lastStandingCount = -1;
    public Text pinNumberText;
    public float ballResetTime = 3.0f;
    public float distanceToRaise = 60.0f;
    public GameObject pinSet;

    private const string tidyTriggerAnimationName = "tidyTrigger";
    private const string resetTriggerAnimationName = "resetTrigger";

    private Ball ball;
    private bool ballEnteredBox = false;
    private float lastChangeTime = 0f;
    private ActionMaster actionMaster;
    private int lastSettledCount;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        ball = GameObject.FindObjectOfType<Ball>();
        animator = this.GetComponent<Animator>();
        lastSettledCount = CountStanding();
        actionMaster = new ActionMaster();
    }

    // Update is called once per frame
    void Update()
    {
        if(ballEnteredBox)
        {
            UpdateStandingPins();
        }
    }

    public void RaisePins()
    {
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
        GameObject pins = GameObject.Instantiate(pinSet, new Vector3(0, distanceToRaise, 1829), Quaternion.identity) as GameObject;
        
        // TODO: Review this code to stop pins from having gravity when they are renewed
        //  It isn't perfect, the best way might be to have the prefabbed pins to be automatically
        //  set to have no gravity and have a Startup Animation sub-state to generate and lower pins.
        for(int i = 0; i < pins.transform.childCount; i++)
        {
            Pin pin = pins.transform.GetChild(i).gameObject.GetComponent<Pin>();
            pin.SetUseGravity(false);
        }

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
        ActionMaster.Action action = actionMaster.Bowl(lastSettledCount - CountStanding());

        switch(action)
        {
            case ActionMaster.Action.Tidy:
                animator.SetTrigger(tidyTriggerAnimationName);
                break;
            case ActionMaster.Action.Reset:
            case ActionMaster.Action.EndTurn:
                animator.SetTrigger(resetTriggerAnimationName);
                lastSettledCount = CountStanding();
                break;
        }

        lastSettledCount = CountStanding();
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
}
