using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected PlayerControllerData PlayerData { get; set; }

    protected State(PlayerControllerData playerData)
    {
        PlayerData = playerData;
    }

    public abstract void Tick();

    public virtual void OnStateEnter()
    {
        Debug.Log("This is the base state entry method.");
    }

    public virtual void OnStateExit()
    {
        Debug.Log("This is the base state exit method.");
    }

    protected virtual void HandleMovement(float inputX)
    {
        inputX = Input.GetAxis("Horizontal");

        if (inputX != 0)
        {
            Vector3 movement = new Vector3(inputX * PlayerData.MoveSpeed, PlayerData.PlayerRB.velocity.y, 0);
            PlayerData.PlayerRB.velocity = movement;
        }
        else
        {
            PlayerData.SetState(PlayerData.Idle);
        }
    }

    protected virtual void TransitionToJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(PlayerData.CanJump && PlayerData.IsGrounded())
            {
                PlayerData.SetState(PlayerData.Jump);
            }
            else
            {
                Debug.Log("You cannot jump at this moment because 'CanJump' = " + PlayerData.CanJump + " and 'IsGrounded' = " + PlayerData.IsGrounded());
            }
        }
    }

    protected void TransitionToInAir()
    {
        if (!PlayerData.IsGrounded() && PlayerData.CurrentState != PlayerData.InAir)
        {
            PlayerData.SetState(PlayerData.InAir);
        }
    }
}
