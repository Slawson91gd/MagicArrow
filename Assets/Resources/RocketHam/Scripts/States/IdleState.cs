using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(PlayerControllerData playerData) : base(playerData)
    {

    }

    public override void Tick()
    {
        Debug.Log("Current State: " + this);
        HandleMovement(PlayerData.MoveInputX);
        base.TransitionToJump();
        base.TransitionToAim();
    }

    public override void OnStateEnter()
    {
        Debug.Log("Now entering the IDLE state.");
    }

    public override void OnStateExit()
    {
        Debug.Log("Now exiting the IDLE state.");
    }

    protected override void HandleMovement(float inputX)
    {
        inputX = Input.GetAxis("Horizontal");

        if (inputX != 0.0f)
        {
            PlayerData.SetState(PlayerData.Movement);
        }
    }
}
