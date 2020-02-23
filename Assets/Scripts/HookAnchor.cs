using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class HookAnchor : MonoBehaviour
{
    public Rigidbody2D Rigidbody2d { get; private set; }

    private void Start()
    {
        Rigidbody2d = GetComponent<Rigidbody2D>();
    }
}
