using UnityEngine;

public class Jump : State
{
    private bool canExtraJump;

    public Jump(Movement movement) : base(movement) { }

    public override void OnStart()
    {
        Debug.Log("Im in Jump State");
        canExtraJump = true;
    }

    public override void OnLeftButton()
    {
        movement.XAxisMove(-1f);
    }

    public override void OnRightButton()
    {
        movement.XAxisMove(1f);
    }

    public override void OnAButton()
    {
        if (canExtraJump)
        {
            Debug.Log("Extra jump");
            canExtraJump = false;
            movement.Jump();
        }
    }

    public override void OnBButton()
    {
        Debug.Log("Attack in air");
        movement.Attack();
    }

    public override void OnXButton()
    {
        movement.ThrowHook();
    }

    public override void OnXButtonUp()
    {
        movement.RetriveHook();
    }

    public override void OnYButton()
    {
        if (movement.CanDash)
        {
            movement.CanDash = false;
            movement.SetState(new Dash(movement));
        }
    }
}