using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [Header("Keys")]
    [SerializeField] private KeyCode up;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode down;
    [SerializeField] private KeyCode right;

    [SerializeField] private KeyCode a; // Jump key
    [SerializeField] private KeyCode b; // Attack key
    [SerializeField] private KeyCode x; // Hook key
    [SerializeField] private KeyCode y;

    [Header("Sensetive")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    [Header("Stun")]
    [SerializeField] private float stunTime = 1f;

    private State currentState;

    public Rigidbody2D Rigidbody2d { get; private set; }

    #region Public
    public void SetState(State state)
    {
        if (currentState?.GetType() != typeof(Stunned) && state?.GetType() != currentState?.GetType())
        {
            currentState = state;
            state.OnStart();
        }
    }

    public void XAxisMove(float magnitude)
    {
        Rigidbody2d.velocity = new Vector2(speed * magnitude, Rigidbody2d.velocity.y);
    }

    public void YAxisMove(float magnitude)
    {
        Rigidbody2d.velocity = new Vector2(Rigidbody2d.velocity.x, speed * magnitude);
    }

    public void Jump(float magnitude)
    {
        Rigidbody2d.velocity = new Vector2(Rigidbody2d.velocity.x, magnitude * jumpForce);
    }

    public void WallJump(Vector2 direction)
    {
        Rigidbody2d.velocity = new Vector2(direction.x * jumpForce, direction.y * jumpForce);
    }

    public void StartRestoreAfterStun()
    {
        StartCoroutine(RestoreAfterStun());
    }
    #endregion

    #region MonoBehaviour
    void Start()
    {
        Rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        currentState?.OnUpdate();

        InputDetect();

        FallingWithGravity();
    }
    #endregion

    #region Private
    private void FallingWithGravity()
    {
        if (currentState is Jump)
        {
            if (Rigidbody2d.velocity.y < 0)
            {
                Rigidbody2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (Rigidbody2d.velocity.y > 0 && !Input.GetKey(a))
            {
                Rigidbody2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }

    private void InputDetect()
    {
        if (Input.GetKey(up))
        {
            currentState?.OnUpButton();
        }
        if (Input.GetKey(left))
        {
            currentState?.OnLeftButton();
        }
        if (Input.GetKey(down))
        {
            currentState?.OnDownButton();
        }
        if (Input.GetKey(right))
        {
            currentState?.OnRightButton();
        }

        if (Input.GetKeyDown(a))
        {
            currentState?.OnAButton();
        }
        if (Input.GetKeyDown(b))
        {
            currentState?.OnBButton();
        }
        if (Input.GetKeyDown(x))
        {
            currentState?.OnXButton();
        }
        if (Input.GetKeyDown(y))
        {
            currentState?.OnYButton();
        }
    }
    #endregion

    private IEnumerator RestoreAfterStun()
    {
        yield return new WaitForSeconds(stunTime);
        Debug.Log("setting state after stun");
        currentState = new Jump(this);
        currentState.OnStart();
    }
}
