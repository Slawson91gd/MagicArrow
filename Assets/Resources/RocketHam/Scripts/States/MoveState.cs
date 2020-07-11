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
        HandleMovement(PlayerData.MoveInputX);
        TransitionToJump();
        TransitionToAim();
    }

    public override void OnStateEnter()
    {
        Debug.Log("Now entering the MOVE state.");
    }

    public override void OnStateExit()
    {
        Debug.Log("Now exiting the MOVE state.");
    }

    protected override void TransitionToAim()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (PlayerData.CanThrow && !PlayerData.BoomerangDeployed)
            {
                PlayerData.PlayerRB.velocity = Vector3.zero;
                PlayerData.SetState(PlayerData.Aim);
            }
            else
            {
                Debug.Log("Cannot currently throw boomerang.");
            }
        }
    }
}
