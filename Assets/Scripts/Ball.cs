using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public Vector3 initialVelocity;

    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private bool inPlay;
    private Rigidbody rigidBody;
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        startingPosition = this.transform.position;
        startingRotation = this.transform.rotation;
        rigidBody = this.GetComponent<Rigidbody>();
        audioSource = this.GetComponent<AudioSource>();

        Reset();
        //Launch(initialVelocity);
    }

    public void Launch(Vector3 velocity)
    {
        if(!inPlay)
        {
            inPlay = true;
            rigidBody.useGravity = true;
            rigidBody.velocity = velocity;
            audioSource.Play();
        }
    }

    public bool IsInPlay()
    {
        return inPlay;
    }

    // Reset the ball to the starting point
    public void Reset()
    {
        inPlay = false;
        this.transform.position = startingPosition;
        this.transform.rotation = startingRotation;
        rigidBody.useGravity = false;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }
}
