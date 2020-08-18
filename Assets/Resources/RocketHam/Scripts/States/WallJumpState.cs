using UnityEngine;

public class WallJumpState : State
{
    public WallJumpState(PlayerControllerData playerData) : base(playerData)
    {

    }

    public override void Tick()
    {
        Debug.Log("Current State: " + this);
    }

    public override void OnStateEnter()
    {
        Debug.Log("Now ENTERING the 'WallJumpState'.");
    }

    public override void OnStateExit()
    {
        Debug.Log("Now EXITING the 'WallJumpState'.");
    }

    
}
