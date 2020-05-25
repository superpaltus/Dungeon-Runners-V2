using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Collision : MonoBehaviour
{
    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask objectLayer;

    [Header("Collision")]
    [SerializeField] private float collisionRadius = 0.25f;
    [SerializeField] private Vector2 bottomOffset, rightOffset, leftOffset;

    private Color debugCollisionColor = Color.red;

    private Movement movement;

    public bool OnGround { get; private set; }
    public bool OnWall { get; private set; }
    public bool OnRightWall { get; private set; }
    public bool OnLeftWall { get; private set; }
    public bool OnSomething { get; private set; }

    void Start()
    {
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        DetectCollisions();
        CalculateState();
    }

    private void DetectCollisions()
    {
        OnGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        OnRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        OnLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        OnWall = OnRightWall || OnLeftWall;
        OnSomething = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, objectLayer);
    }

    private void CalculateState()
    {
        bool onAir = !OnGround && !OnWall;
        bool onRightWall = !OnGround && !OnLeftWall && OnRightWall;
        bool onLeftWall = !OnGround && !OnRightWall && OnLeftWall;
        bool onGround = OnGround;

        if (onAir)
        {
            movement.SetState(new Jump(movement));
        }
        if (onGround)
        {
            movement.SetState(new Idle(movement));
        }
        if (onRightWall && movement.Rigidbody2d.velocity.y <= 0)
        {
            movement.SetState(new RightWall(movement));
        }
        if (onLeftWall && movement.Rigidbody2d.velocity.y <= 0)
        {
            movement.SetState(new LeftWall(movement));
        }
        if (OnSomething)
        {
            Debug.Log("ON SOMETHING");
            if (movement.CurrentState is Jump)
            {
                Debug.Log("Restore jump");
                movement.CurrentState.OnStart();
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }
}
