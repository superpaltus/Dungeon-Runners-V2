using UnityEngine;
using System.Collections;

public class Hook : State
{
    private Rigidbody2D hookAnchor;

    public Hook(Movement movement, Rigidbody2D hookAnchor) : base(movement) 
    {
        this.hookAnchor = hookAnchor;
    }

    public override void OnStart()
    {
        Debug.Log("I hooked smth");
        movement.SpringJoint2d.enabled = true;
        movement.SpringJoint2d.connectedBody = hookAnchor;

        // Remove next line comment for x-moving disable when in Hook state (not fun)
        // movement.Rigidbody2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    public override void OnXButtonUp()
    {
        movement.SpringJoint2d.enabled = false;
        movement.Rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        movement.ForceSetState(new Jump(movement));
    }
}
