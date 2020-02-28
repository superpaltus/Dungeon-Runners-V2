using UnityEngine;

public class Stunned : State
{
    private float stunTime;

    public Stunned(Movement movement, float stunTime = 0.3f) : base(movement) { }

    public override void OnStart()
    {
        Debug.Log("Im stunned!");
        movement.StartRestoreAfterStun(stunTime);
    }
}
