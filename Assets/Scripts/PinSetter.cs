using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PinSetter : MonoBehaviour
{
    public Text pinNumberText;

    private Pin[] pins;

    // Use this for initialization
    void Start()
    {
        pins = GameObject.FindObjectsOfType<Pin>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pinNumberText)
        {
            pinNumberText.text = CountStanding().ToString();
        }
    }

    // Count the standing pins
    int CountStanding()
    {
        int count = 0;
        foreach(Pin pin in pins)
        {
            count += pin.IsStanding() ? 1 : 0;
        }

        return count;
    }
}
