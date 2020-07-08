using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public MoveState(PlayerControllerData playerData) : base(playerData)
    {

    }

    public override void Tick()
    {
        Debug.Log("Current State: " + this);
        base.HandleMovement(PlayerData.MoveInputX);
        base.TransitionToJump();
    }

    public override void OnStateEnter()
    {
        Debug.Log("Now entering the MOVE state.");
    }

    public override void OnStateExit()
    {
        Debug.Log("Now exiting the MOVE state.");
    }

    
}
