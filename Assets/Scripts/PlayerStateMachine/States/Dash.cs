using UnityEngine;
using System.Collections;

public class Dash : State
{
    public Dash(Movement movement) : base(movement) { }

    public override void OnStart()
    {
        movement.StartDash();
        movement.SetState(new Stunned(movement));
    }
}
