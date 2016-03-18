using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public float initialSpeed;

    private Rigidbody rigidBody;
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        audioSource = this.GetComponent<AudioSource>();

        Launch();
    }

    public void Launch()
    {
        rigidBody.velocity = Vector3.forward * initialSpeed;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
