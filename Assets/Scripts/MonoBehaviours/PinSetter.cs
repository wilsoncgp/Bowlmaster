using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour
{
    public float distanceToRaise = 60.0f;
    public GameObject pinSet;

    private const string tidyTriggerAnimationName = "tidyTrigger";
    private const string resetTriggerAnimationName = "resetTrigger";
    
    private Animator animator;
    private PinCounter pinCounter;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        pinCounter = GameObject.FindObjectOfType<PinCounter>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RaisePins()
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if(pin.IsStanding())
            {
                pin.SetUseGravity(false);
                pin.ResetVelocity();
                pin.transform.Translate(0, distanceToRaise, 0);
                pin.transform.rotation = Quaternion.identity;
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
        GameObject pins = GameObject.Instantiate(pinSet, new Vector3(0, 0, 1829), Quaternion.identity) as GameObject;
        
        // TODO: Review this code to stop pins from having gravity when they are renewed
        //  It isn't perfect, the best way might be to have the prefabbed pins to be automatically
        //  set to have no gravity and have a Startup Animation sub-state to generate and lower pins.
        for(int i = 0; i < pins.transform.childCount; i++)
        {
            Pin pin = pins.transform.GetChild(i).gameObject.GetComponent<Pin>();
            pin.SetUseGravity(false);
            pin.transform.Translate(0, distanceToRaise, 0);
        }

    }

    public void PerformAction(ActionMaster.Action action)
    {

        switch (action)
        {
            case ActionMaster.Action.Tidy:
                animator.SetTrigger(tidyTriggerAnimationName);
                break;
            case ActionMaster.Action.Reset:
            case ActionMaster.Action.EndTurn:
                animator.SetTrigger(resetTriggerAnimationName);
                pinCounter.Reset();
                break;
        }
    }

}
