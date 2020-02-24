using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GizmoCube : MonoBehaviour
{
    [Header("Rect Seetings")]
    public float width;
    public float height;
    [Header("Label Seetings")]
    public string Text = "Put text here";
    public Vector3 TextPosition;
    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.yellow;
        Handles.Label(transform.position+TextPosition, Text);
        Gizmos.DrawWireCube(transform.position,new Vector3(width,height,1));
    }
}
