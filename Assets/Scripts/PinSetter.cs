using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour
{
    public Text pinNumberText;
    
    private bool ballEnteredBox = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        pinNumberText.text = CountStanding().ToString();
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
        Pin pin = collider.GetComponentInParent<Pin>();
        if(pin)
        {
            Destroy(pin.gameObject);
        }
    }
}
