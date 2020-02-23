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

    private bool isRetriving;

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

    public void RetriveHook()
    {
        currentLifeTime = 0f;
    }

    private void Start()
    {
        movement = GetComponentInParent<Movement>();
    }

    private void OnEnable()
    {
        isRetriving = false;
        currentLifeTime = lifeTime;
        transform.position = transform.parent.position;
    }

    private void LateUpdate()
    {
        transform.localPosition += moveDirection * Time.deltaTime * speed;
        currentLifeTime -= Time.deltaTime;

        if (!isRetriving && currentLifeTime <= 0f)
        {
            isRetriving = true;
            moveDirection = -moveDirection;
        }

        if (transform.localPosition.y <= 0.3f)
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
