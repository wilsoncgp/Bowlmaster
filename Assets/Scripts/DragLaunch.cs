﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour
{
    private Ball ball;
    private float dragStartTime;
    private Vector2 dragStartPosition;

    // Use this for initialization
    void Start()
    {
        ball = GetComponent<Ball>();
    }

    public void DragStart()
    {
        // Capture time and position of drag start
        dragStartTime = Time.timeSinceLevelLoad;
        dragStartPosition = Input.mousePosition;
    }

    public void DragEnd()
    {
        // Calculate time and position differences
        float dragEndTime = Time.timeSinceLevelLoad;
        float dragTimeDifference = dragEndTime - dragStartTime;

        Vector2 dragEndPosition = Input.mousePosition;
        Vector2 dragPositionDifference = dragEndPosition - dragStartPosition;

        // Launch the ball
        ball.Launch(new Vector3(dragPositionDifference.x, 0, dragPositionDifference.y) / dragTimeDifference);
    }
}
