﻿using UnityEngine;

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
            movement.Jump(1f);
        }
    }

    public override void OnBButton()
    {
        Debug.Log("Attack in air");
    }

    public override void OnYButton()
    {
        movement.SetState(new Dash(movement));
    }
}