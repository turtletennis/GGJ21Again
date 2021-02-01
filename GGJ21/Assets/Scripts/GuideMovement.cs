using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideMovement : MonoBehaviour
{
    public float range; //Distance between player and guide before guide moves
    public float maxSpeed;
    public float acceleration;
    public Vector3[] guidePoints;

    private int currentPoint = 0;
    private Transform player;
    private bool moving;
    private float currentSpeed;

    void Start()
    {
        player = GameObject.Find("character").transform;

        gameObject.transform.position = guidePoints[0];
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, gameObject.transform.position);
        if (distance <= range && !moving)
        {
            currentPoint++;
            if(currentPoint < guidePoints.Length)
            {
                moving = true;
            }
            else
            {
                currentPoint = guidePoints.Length - 1;
            }
        }

        if (moving)
        {
            moveToPoint();
        }
    }

    void moveToPoint()
    {
        Vector3 moveVector = guidePoints[currentPoint] - gameObject.transform.position;
        if (moveVector.magnitude <= maxSpeed * Time.deltaTime)
        {
            gameObject.transform.Translate(moveVector);
            moving = false;
            currentSpeed = 0;
        }
        else
        {
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed > maxSpeed) currentSpeed = maxSpeed;
            gameObject.transform.Translate(moveVector.normalized * currentSpeed * Time.deltaTime);
        }
    }
}