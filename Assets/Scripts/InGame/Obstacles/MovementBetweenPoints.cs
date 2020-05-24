using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementBetweenPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> pointObjects;
    [SerializeField] private float speed;
    [SerializeField] private bool setPositionToFirstPoint;

    private List<Vector3> points;
    private int pointIndex;

    private void Awake()
    {
        points = pointObjects.Select(x => x.position).ToList();
        foreach (var pointObject in pointObjects)
        {
            Destroy(pointObject.gameObject);
        }

        pointObjects.Clear();
        if (setPositionToFirstPoint) transform.position = points[0];
    }

    void FixedUpdate()
    {
        if (transform.position == points[pointIndex])
        {
            pointIndex = pointIndex + 1 < points.Count ? pointIndex + 1 : 0;
        }
        
        transform.position = Vector3.MoveTowards(transform.position, points[pointIndex], speed * Time.fixedDeltaTime);
    }
}