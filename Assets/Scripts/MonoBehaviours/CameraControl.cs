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
        //      1. The ball's position in the Z dimension
        //      2. The radius of the ball
        if(ball.transform.position.z + (ball.transform.localScale.z / 2.0f) < 1829.0f)
        {
            this.transform.position = ball.transform.position - offset;
        }
    }
}
