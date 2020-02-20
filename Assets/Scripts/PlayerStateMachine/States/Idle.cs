using UnityEngine;

public class Idle : State
{
    public Idle(Movement movement) : base(movement) { }

    public override void OnStart()
    {
        Debug.Log("Im in Idle State");
    }

    public override void OnUpdate()
    {
        movement.Rigidbody2d.velocity = new Vector2(0f, movement.Rigidbody2d.velocity.y);
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
        movement.Jump(1f);
    }

    public override void OnBButton()
    {
        Debug.Log("Attacking");
    }

    public override void OnYButton()
    {
        movement.SetState(new Dash(movement));
    }
}
