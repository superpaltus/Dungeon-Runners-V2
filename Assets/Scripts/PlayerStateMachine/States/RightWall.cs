using UnityEngine;

public class RightWall : State
{
    public RightWall(Movement movement) : base(movement) { }

    public override void OnStart()
    {
        Debug.Log("Im on Right wall");
    }

    public override void OnUpButton()
    {
        movement.YAxisMove(1f);
    }

    public override void OnLeftButton()
    {
        Debug.Log("Unhook wall");
        Vector2 jumpDirection = new Vector2(-1f, 0f);
        movement.WallJump(jumpDirection);
    }
    public override void OnDownButton()
    {
        movement.YAxisMove(-1f);
    }
    public override void OnAButton()
    {
        Vector2 jumpDirection = new Vector2(-1f, 1f);
        jumpDirection = jumpDirection.normalized;
        movement.SetState(new Jump(movement));
        movement.WallJump(jumpDirection);
    }
    public override void OnBButton()
    {
        Debug.Log("Attacking on the wall");
    }
}
