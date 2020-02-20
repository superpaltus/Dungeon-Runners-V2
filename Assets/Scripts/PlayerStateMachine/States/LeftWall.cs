using UnityEngine;
using System.Collections;

public class LeftWall : State
{
    public LeftWall(Movement movement) : base(movement) { }

    public override void OnStart()
    {
        Debug.Log("Im on Left wall");
    }

    public override void OnUpButton()
    {
        movement.YAxisMove(1f);
    }

    public override void OnRightButton()
    {
        Debug.Log("Unhook wall");
        Vector2 jumpDirection = new Vector2(1f, 0f);
        movement.WallJump(jumpDirection);
    }
    public override void OnDownButton()
    {
        movement.YAxisMove(-1f);
    }
    public override void OnAButton()
    {
        Vector2 jumpDirection = new Vector2(1f, 1f);
        jumpDirection = jumpDirection.normalized;
        movement.SetState(new Stunned(movement));
        movement.WallJump(jumpDirection);
    }
    public override void OnBButton()
    {
        Debug.Log("Attacking on the wall");
    }
}
