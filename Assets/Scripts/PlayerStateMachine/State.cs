using System;

public abstract class State
{
    protected readonly Movement movement;
    public State(Movement movement)
    {
        this.movement = movement;
    }

    public virtual void OnStart() { }
    public virtual void OnUpdate() { }
    public virtual void OnUpButton() { }
    public virtual void OnLeftButton() { }
    public virtual void OnDownButton() { }
    public virtual void OnRightButton() { }
    public virtual void OnAButton() { }
    public virtual void OnBButton() { }
    public virtual void OnXButton() { }
    public virtual void OnXButtonUp() { }
    public virtual void OnYButton() { }
}
