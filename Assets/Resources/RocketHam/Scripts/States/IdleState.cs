using UnityEngine;

public class IdleState : State
{
    public IdleState(PlayerControllerData playerData) : base(playerData)
    {

    }

    public override void Tick()
    {
        //Debug.Log("Current State: " + this);
        HandleMovement(PlayerData.MoveInputX);
        base.TransitionToJump();
        base.TransitionToAim();
    }

    public override void OnStateEnter()
    {
        //Debug.Log("Now entering the IDLE state.");
    }

    public override void OnStateExit()
    {
        //Debug.Log("Now exiting the IDLE state.");
    }

    protected override void HandleMovement(float inputX)
    {
        inputX = Input.GetAxisRaw("Horizontal");

        if (PlayerData.IsGrounded())
        {
            if(inputX != 0)
            {
                PlayerData.SetState(PlayerData.Movement);
            }
            else
            {
                Vector3 movement = new Vector3(inputX * PlayerData.MoveSpeed, PlayerData.PlayerRB.velocity.y, 0);
                PlayerData.PlayerRB.velocity = movement;
            }
        }
        else
        {
            PlayerData.SetState(PlayerData.InAir);
        }
    }
}
