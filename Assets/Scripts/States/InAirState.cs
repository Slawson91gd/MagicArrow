using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : State
{
    public InAirState(PlayerControllerData playerData) : base(playerData)
    {

    }

    public override void Tick()
    {
        Debug.Log("Current State: " + this);
        HandleMovement(PlayerData.MoveInputX);
        HandleLanding(PlayerData.MoveInputX);
    }

    public override void OnStateEnter()
    {
        Debug.Log("Now ENTERING the INAIR state.");
    }

    public override void OnStateExit()
    {
        Debug.Log("Now EXITING the INAIR state.");
    }

    protected override void HandleMovement(float inputX)
    {
        inputX = Input.GetAxis("Horizontal");

        if (inputX != 0)
        {
            Vector3 movement = new Vector3(inputX * PlayerData.MoveSpeed, PlayerData.PlayerRB.velocity.y, 0);
            PlayerData.PlayerRB.velocity = movement;
        }
    }

    private void HandleLanding(float moveInput)
    {
        if (PlayerData.IsGrounded())
        {
            if (moveInput != 0)
            {
                PlayerData.SetState(PlayerData.Movement);
            }
            else
            {
                PlayerData.SetState(PlayerData.Idle);
            }
        }
    }
}
