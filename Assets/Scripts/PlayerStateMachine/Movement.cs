using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Stamina))]
[RequireComponent(typeof(PlyerGoldCollector))]
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
    [SerializeField] private KeyCode y; // Dash key

    private Vector2 inputDirection;

    [Header("Sensetive")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    [Header("Stun")]
    [SerializeField] private float stunTime = 1f;

    [Header("Dash")]
    [SerializeField] private float dashTime = 0.3f;
    [SerializeField] private float dashForce = 8f;

    [Header("Stamina")]
    [SerializeField] private float staminaDrianSpeed = 5f;
    [SerializeField] private float staminaForJump = 15f;

    private State currentState;
    private HookTile hookTile;

    private WeaponAnchorRotator weaponAnchorRotator;
    private Vector3 faceDirection = Vector3.forward;

    public Rigidbody2D Rigidbody2d { get; private set; }
    public SpringJoint2D SpringJoint2d { get; private set; }
    public Stamina Stamina { get; private set; }
    public bool CanDash { get; set; } = true;

    #region Public
    public void SetState(State state)
    {
        var settingType = state?.GetType();
        var currentType = currentState?.GetType();

        if (currentType == typeof(Stunned)) return;
        if (currentType == typeof(Hook)) return;

        if (settingType != currentType)
        {
            currentState?.OnEnd();
            currentState = state;
            state.OnStart();
        }
    }

    public void ForceSetState(State state)
    {
        currentState.OnEnd();
        currentState = state;
        state.OnStart();
    }

    public void XAxisMove(float magnitude)
    {
        Rigidbody2d.velocity = new Vector2(speed * magnitude, Rigidbody2d.velocity.y);
    }

    public void YAxisMove(float magnitude)
    {
        Rigidbody2d.velocity = new Vector2(Rigidbody2d.velocity.x, speed * magnitude);
        Stamina.Spend(Time.deltaTime * staminaDrianSpeed);
    }

    public void Jump()
    {
        if (Stamina.Spend(staminaForJump))
        {
            Rigidbody2d.velocity = new Vector2(Rigidbody2d.velocity.x, jumpForce);
        }
    }

    public void WallJump(Vector2 direction)
    {
        if (direction.y == 0 || Stamina.Spend(staminaForJump))
        {
            Rigidbody2d.velocity = new Vector2(direction.x * jumpForce, direction.y * jumpForce);
        }
    }

    public void StartRestoreAfterStun(float stunTime)
    {
        StartCoroutine(RestoreAfterStun(stunTime));
    }

    public void StartDash()
    {
        StartCoroutine(Dash());
    }

    public void ThrowHook()
    {
        if (!hookTile.isActiveAndEnabled)
        {
            hookTile.gameObject.SetActive(true);
            hookTile.MoveDirection = inputDirection;
        }
    }

    public void RetriveHook()
    {
        if (hookTile.isActiveAndEnabled)
        {
            hookTile.RetriveHook();
        }
    }

    public void Attack()
    {
        if (!weaponAnchorRotator.isActiveAndEnabled)
        {
            weaponAnchorRotator.gameObject.SetActive(true);
            weaponAnchorRotator.SetRotation(faceDirection);
        }
    }
    #endregion

    #region MonoBehaviour
    void Start()
    {
        Rigidbody2d = GetComponent<Rigidbody2D>();
        SpringJoint2d = GetComponent<SpringJoint2D>();

        hookTile = GetComponentInChildren<HookTile>();
        hookTile.gameObject.SetActive(false);

        weaponAnchorRotator = GetComponentInChildren<WeaponAnchorRotator>();
        weaponAnchorRotator.gameObject.SetActive(false);

        Stamina = GetComponent<Stamina>();
        Stamina.StaminaExpired += OnStaminaExpired;
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
        Vector2 thisFrameInputDirection = Vector2.zero;

        if (Input.GetKey(up))
        {
            currentState?.OnUpButton();
            thisFrameInputDirection += Vector2.up;
        }
        if (Input.GetKey(left))
        {
            currentState?.OnLeftButton();
            thisFrameInputDirection += Vector2.left;
            faceDirection = Vector3.forward;
        }
        if (Input.GetKey(down))
        {
            currentState?.OnDownButton();
            thisFrameInputDirection += Vector2.down;
        }
        if (Input.GetKey(right))
        {
            currentState?.OnRightButton();
            thisFrameInputDirection += Vector2.right;
            faceDirection = Vector3.back;
        }

        inputDirection = thisFrameInputDirection.normalized;

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
        if (Input.GetKeyUp(x))
        {
            currentState?.OnXButtonUp();
        }
        if (Input.GetKeyDown(y))
        {
            currentState?.OnYButton();
        }
    }

    private IEnumerator RestoreAfterStun(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        Debug.Log("setting state after stun");
        ForceSetState(new Jump(this));
    }

    private IEnumerator Dash()
    {
        if (faceDirection == Vector3.forward)
        {
            Rigidbody2d.velocity = new Vector2(-1f, 0f);
        }
        else if (faceDirection == Vector3.back)
        {
            Rigidbody2d.velocity = new Vector2(1f, 0f);
        }

        Rigidbody2d.velocity *= dashForce;
        yield return new WaitForSeconds(dashTime);
        ForceSetState(new Jump(this));
    }
    
    private void OnStaminaExpired()
    {
        ForceSetState(new Stunned(this, 3f));
    }

    #endregion
}
