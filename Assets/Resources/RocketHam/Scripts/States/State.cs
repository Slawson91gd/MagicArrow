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
        inputX = Input.GetAxisRaw("Horizontal");

        if (PlayerData.IsGrounded())
        {
            if (inputX != 0)
            {
                Vector3 movement = new Vector3(inputX * PlayerData.MoveSpeed, PlayerData.PlayerRB.velocity.y, 0);
                PlayerData.PlayerRB.velocity = movement;
            }
            else
            {
                PlayerData.PlayerRB.velocity = Vector2.zero;
                PlayerData.SetState(PlayerData.Idle);
            }
        }
        else
        {
            PlayerData.SetState(PlayerData.InAir);
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

    protected virtual void TransitionToAim()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (PlayerData.CanThrow && !PlayerData.BoomerangDeployed)
            {
                PlayerData.SetState(PlayerData.Aim);
            }
            else
            {
                Debug.Log("Cannot currently throw boomerang.");
            }
        }
    }
}
