﻿using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour
{
    public float standingThreshold = 25.0f;

    private Rigidbody rigidBody;
    //private Vector3 initialPosition;

    // Use this for initialization
    void Start()
    {
        if (!this.rigidBody)
        {
            rigidBody = this.GetComponent<Rigidbody>();
        }

        //initialPosition = this.transform.position;
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
        if(!this.rigidBody)
        {
            rigidBody = this.GetComponent<Rigidbody>();
        }

        this.rigidBody.useGravity = useGravity;
    }

    public void ResetVelocity()
    {
        if (!this.rigidBody)
        {
            rigidBody = this.GetComponent<Rigidbody>();
        }

        this.rigidBody.velocity = Vector3.zero;
        this.rigidBody.angularVelocity = Vector3.zero;
    }
}
