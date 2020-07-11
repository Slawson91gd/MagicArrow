using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    public JumpState(PlayerControllerData playerData) : base(playerData)
    {

    }

    public override void Tick()
    {
        Debug.Log("Current State: " + this);
        HandleMovement(PlayerData.MoveInputX);
        TransitionToInAir();
    }

    public override void OnStateEnter()
    {
        Debug.Log("Now ENTERING the JUMP state.");
        PlayerData.PlayerRB.AddForce(Vector2.up * PlayerData.JumpForce, ForceMode2D.Force);
    }

    public override void OnStateExit()
    {
        Debug.Log("Now LEAVING the JUMP state.");
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
}
