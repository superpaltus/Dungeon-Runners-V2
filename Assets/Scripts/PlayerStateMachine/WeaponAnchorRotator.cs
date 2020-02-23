using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnchorRotator : MonoBehaviour
{
    [SerializeField] private float speed = 200f;
    [SerializeField] private float maxZAngleRight = 250f;
    [SerializeField] private float maxZAngleLeft = 110f;
    
    private Vector3 rotation = Vector3.back;

    public void SetRotation(Vector3 rotation)
    {
        this.rotation = rotation;
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void FixedUpdate()
    {
        transform.Rotate(rotation * Time.fixedDeltaTime * speed);

        if (rotation == Vector3.back && transform.localRotation.eulerAngles.z <= maxZAngleRight
            || rotation == Vector3.forward && transform.localRotation.eulerAngles.z >= maxZAngleLeft)
        {
            gameObject.SetActive(false);
        }
    }
}
