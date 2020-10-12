using UnityEngine;

public class InAirState : State
{
    private readonly float fallMultiplyer = 2.5f;
    private readonly float lowJumpMultiplyer = 2.0f;

    public InAirState(PlayerControllerData playerData) : base(playerData)
    {

    }

    public override void Tick()
    {
        //Debug.Log("Current State: " + this);
        HandleInAir(PlayerData.MoveInputX);
        HandleMovement(PlayerData.MoveInputX);
        base.TransitionToAim();
    }

    public override void OnStateEnter()
    {
        //Debug.Log("Now ENTERING the INAIR state.");
    }

    public override void OnStateExit()
    {
        //Debug.Log("Now EXITING the INAIR state.");
    }

    private void HandleInAir(float moveInput)
    {
        if (!PlayerData.IsGrounded())
        {
            if(PlayerData.PlayerRB.velocity.y < 0)
            {
                PlayerData.PlayerRB.velocity += Vector2.up * Physics2D.gravity.y * (PlayerData.FallMultiplyer - 1) * Time.deltaTime;
            }
            else if(PlayerData.PlayerRB.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                PlayerData.PlayerRB.velocity += Vector2.up * Physics2D.gravity.y * (PlayerData.LowJumpMultiplyer - 1) * Time.deltaTime;
            }
        }
        else
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

    protected override void HandleMovement(float inputX)
    {
        inputX = Input.GetAxis("Horizontal");

        if (inputX != 0)
        {
            if (inputX > 0)
            {
                if (PlayerData.Player.GetComponent<SpriteRenderer>().flipX != false)
                    PlayerData.Player.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                if (PlayerData.Player.GetComponent<SpriteRenderer>().flipX != true)
                    PlayerData.Player.GetComponent<SpriteRenderer>().flipX = true;
            }
            Vector3 movement = new Vector3(inputX * (PlayerData.MoveSpeed * 0.7f), PlayerData.PlayerRB.velocity.y, 0);
            PlayerData.PlayerRB.velocity = movement;
        }
    }
}
