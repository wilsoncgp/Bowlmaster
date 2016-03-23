using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour
{
    public float standingThreshold = 5.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Returns true if the Pin hasn't tilted the number of degrees
    //  defined by the standingThreshold value
    public bool IsStanding()
    {
        return Mathf.Abs(Vector3.Angle(Vector3.up, this.transform.up)) < standingThreshold;
    }
}
