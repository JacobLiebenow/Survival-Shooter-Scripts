﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothing = 5f;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetCamPosition = target.position + offset;

        //Lerps smoothing move objects between positions
        transform.position = Vector3.Lerp(transform.position, targetCamPosition, smoothing * Time.deltaTime);
    }

}
