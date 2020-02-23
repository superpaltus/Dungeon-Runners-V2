using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class HookTile : MonoBehaviour
{
    [Header("Fly properties")]
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] private float speed = 5f;

    private float currentLifeTime;
    private Vector3 moveDirection;

    private Movement movement;

    public Vector2 MoveDirection
    {
        get
        {
            return moveDirection;
        }
        set
        {
            if (value != Vector2.zero)
            {
                moveDirection = value.normalized;
            }
            else
            {
                moveDirection = Vector3.up;
            }
        }
    }

    private void Start()
    {
        movement = GetComponentInParent<Movement>();
    }

    private void OnEnable()
    {
        currentLifeTime = lifeTime;
        transform.position = transform.parent.position;
    }

    private void Update()
    {
        transform.localPosition += moveDirection * Time.deltaTime * speed;
        currentLifeTime -= Time.deltaTime;
        if (currentLifeTime <= 0f)
        {
            moveDirection = -moveDirection;
            currentLifeTime = 2 * lifeTime;
        }
        if (transform.localPosition.y <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hookAnchor = collision.GetComponent<HookAnchor>();

        if (hookAnchor != null)
        {
            movement.SetState(new Hook(movement, hookAnchor.Rigidbody2d));
        }

        gameObject.SetActive(false);
    }
}
