using UnityEngine;

public class Stunned : State
{
    public Stunned(Movement movement) : base(movement) { }

    public override void OnStart()
    {
        Debug.Log("Im stunned!");
        movement.StartRestoreAfterStun();
    }
}
