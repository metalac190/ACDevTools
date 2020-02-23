using System.Collections;

public abstract class State
{
    protected StateMachine StateMachine;

    public virtual IEnumerator Enter()
    {
        yield return null;
    }

    public virtual void Tick()
    {

    }

    public virtual IEnumerator Exit()
    {
        yield return null;
    }
}