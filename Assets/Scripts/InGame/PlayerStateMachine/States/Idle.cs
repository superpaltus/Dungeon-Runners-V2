using UnityEngine;

public class Idle : State
{
    private float stopCoefficient = 1.4f;

    public Idle(Movement movement) : base(movement) { }

    public override void OnStart()
    {
        movement.CanDash = true;
        movement.Stamina.Regain();
        Debug.Log("Im in Idle State");
    }

    public override void OnUpdate()
    {
        movement.Rigidbody2d.velocity = new Vector2(movement.Rigidbody2d.velocity.x / stopCoefficient, movement.Rigidbody2d.velocity.y);
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
        movement.Jump();
    }

    public override void OnBButton()
    {
        Debug.Log("Attacking");
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
