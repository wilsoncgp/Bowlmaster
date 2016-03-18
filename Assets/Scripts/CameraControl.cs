using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public Ball ball;

    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = ball.transform.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Camera distance to the position of the head pin calculated by:
        //      1. It's own position in the Z dimension
        //      2. The offset vector's Z value
        //      3. A value derived from the diameter of the ball
        // The course said to have the camera stop at the z position of the
        //  head pin but I figured that was too far forward.
        if(this.transform.position.z + offset.z + (ball.transform.localScale.z / 1.5f) < 1829)
        {
            this.transform.position = ball.transform.position - offset;
        }
    }
}
