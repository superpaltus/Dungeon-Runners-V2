using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform anchor;
    private Vector3 anchorPoint;

    private void Awake()
    {
        anchorPoint = anchor.position;
        Destroy(anchor.gameObject);
    }

    private void FixedUpdate()
    {
        transform.RotateAround(anchorPoint, Vector3.forward, rotationSpeed * Time.fixedDeltaTime);
    }
}