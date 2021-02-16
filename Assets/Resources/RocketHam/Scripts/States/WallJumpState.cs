using UnityEngine;

public class WallJumpState : State
{
    private float wallTimer;
    private float wallDuration;

    public WallJumpState(PlayerControllerData playerData) : base(playerData)
    {
        wallTimer = 0;
        wallDuration = 3.0f;
    }

    public override void Tick()
    {
        WallJump(PlayerData.JumpForce);
    }

    public override void OnStateEnter()
    {
        PlayerData.CanMove = false;
        PlayerData.PlayerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        Debug.Log("Just ENTERED the WALLJUMPSTATE.");
    }

    public override void OnStateExit()
    {
        PlayerData.CanMove = true;
        PlayerData.PlayerRB.constraints = RigidbodyConstraints2D.None;
        PlayerData.PlayerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        wallTimer = 0;
    }

    private void WallJump(float jumpForce)
    {
        Vector2 jumpDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 1);
        
        if (wallTimer < wallDuration)
        {
            wallTimer += Time.fixedDeltaTime;

            if (Input.GetButtonDown("Jump"))
            {
                PlayerData.PlayerRB.velocity = jumpDirection * (jumpForce * 0.75f);
                PlayerData.CurrentWallJumps++;
                PlayerData.SetState(PlayerData.InAir);
            }
        }
        else
        {
            PlayerData.SetState(PlayerData.InAir);
        }
    }
}
