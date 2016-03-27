using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour
{
    public float standingThreshold = 5.0f;

    private Rigidbody rigidBody;

    // Use this for initialization
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
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

    public void SetUseGravity(bool useGravity)
    {
        this.rigidBody.useGravity = useGravity;
    }
}
