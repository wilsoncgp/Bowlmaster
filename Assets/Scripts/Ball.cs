using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public Vector3 initialVelocity;

    private Rigidbody rigidBody;
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        audioSource = this.GetComponent<AudioSource>();

        rigidBody.useGravity = false;

        //Launch(initialVelocity);
    }

    public void Launch(Vector3 velocity)
    {
        rigidBody.useGravity = true;
        rigidBody.velocity = velocity;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
