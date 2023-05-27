using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameremove : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    
    public float leftBound;
    public float rightBound;
    public float bottomBound;
    public float topBound;

    void FixedUpdate()
    {
        transform.position = new Vector3(Math.Clamp(transform.position.x,leftBound,rightBound), Math.Clamp(transform.position.y,bottomBound,topBound), transform.position.z);
        var desiredPosition = target.position + offset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        
    }
}
