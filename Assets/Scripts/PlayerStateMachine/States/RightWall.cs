using UnityEngine;

public class RightWall : State
{
    public RightWall(Movement movement) : base(movement) { }

    private bool isHooked;
    private float startLinearDrag = 5f;
    private float standardGravityScale;

    public override void OnStart()
    {
        Debug.Log("Im on Right wall");
        standardGravityScale = movement.Rigidbody2d.gravityScale;
        //movement.Rigidbody2d.velocity = Vector2.zero;
        movement.Rigidbody2d.drag = startLinearDrag;
        isHooked = false;
    }

    public override void OnUpdate()
    {
        float drag = movement.Rigidbody2d.drag;
        
        if (!isHooked && drag > 0f)
        {
            Mathf.Clamp(movement.Rigidbody2d.drag -= Time.deltaTime, 0f, startLinearDrag);
        }

        if (isHooked)
        {
            movement.Stamina.Spend(Time.deltaTime);
        }
    }

    public override void OnUpButton()
    {
        if (isHooked)
        {
            movement.YAxisMove(1f);
        }
    }

    public override void OnLeftButton()
    {
        Debug.Log("Unhook wall");
        Vector2 jumpDirection = new Vector2(-1f, 0f);
        movement.WallJump(jumpDirection);
    }
    public override void OnDownButton()
    {
        if (isHooked)
        {
            movement.YAxisMove(-1f);
        }
    }
    public override void OnAButton()
    {
        Vector2 jumpDirection = new Vector2(-1f, 1f);
        jumpDirection = jumpDirection.normalized;
        movement.SetState(new Stunned(movement));
        movement.WallJump(jumpDirection);
    }
    public override void OnBButton()
    {
        Debug.Log("Attacking on the wall");
    }

    public override void OnXButton()
    {
        if (movement.Stamina.Spend(Time.deltaTime))
        {
            isHooked = true;
            movement.Rigidbody2d.gravityScale = 0f;
            movement.Rigidbody2d.drag = startLinearDrag;
        }
    }

    public override void OnXButtonUp()
    {
        isHooked = false;
        movement.Rigidbody2d.gravityScale = standardGravityScale;
    }

    public override void OnEnd()
    {
        movement.Rigidbody2d.drag = 0f;
        movement.Rigidbody2d.gravityScale = standardGravityScale;
    }
}
