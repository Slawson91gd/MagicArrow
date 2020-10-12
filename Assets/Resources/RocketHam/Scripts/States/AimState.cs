﻿using UnityEngine;

public class AimState : State
{
    private SpriteRenderer PlayerSprite { get; set; }
    
    private Color startingColor;
    private Color aimColor;


    public AimState(PlayerControllerData playerData) : base(playerData)
    {
        PlayerSprite = PlayerData.Player.GetComponent<SpriteRenderer>();
        startingColor = PlayerSprite.color;
        aimColor = Color.blue;
    }

    public override void Tick()
    {
        HandleAim();
        HandleMovement(PlayerData.MoveInputX);
        TransitionToJump();
        Debug.Log("Now in the " + this);
    }

    public override void OnStateEnter()
    {
        PlayerSprite.color = aimColor;
    }

    public override void OnStateExit()
    {
        PlayerSprite.color = startingColor;
    }

    private void HandleAim()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = PlayerData.PlayerCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            Vector3 playerPos = PlayerData.Player.transform.position;
            Vector3 direction = mousePos - playerPos;
            float distance = direction.magnitude;
            float aimAngle = Vector2.Angle(Vector2.right, direction);


            //---------------------------------------------------------------
            // If aiming within 90 degree angle
            if (aimAngle <= 45.0f || aimAngle >= 135.0f)
            {
                // if distance between player and mousePos is less than or equal to BoomerangDistance
                if (distance <= PlayerData.BoomerangDistance)
                {
                    PlayerData.BoomerangTarget = mousePos;
                    Debug.DrawRay(playerPos, direction, Color.red);
                }
                // otherwise
                else
                {
                    // target is equal to a distance between the player and the mouse position
                    PlayerData.BoomerangTarget = playerPos + direction.normalized * PlayerData.BoomerangDistance;
                    // Draw a line from the players position to a specified distance between player and mouse position
                    Debug.DrawRay(playerPos, (PlayerData.BoomerangTarget - playerPos).normalized * (PlayerData.BoomerangTarget - playerPos).magnitude, Color.blue);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            PlayerData.SetState(PlayerData.Throw);
        }
    }

    protected override void HandleMovement(float inputX)
    {
        inputX = Input.GetAxisRaw("Horizontal");
        Vector3 movement;

        if(inputX != 0)
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
            PlayerData.PlayerAnimator.SetBool("IsMoving", true);
        }
        else
        {
            PlayerData.PlayerAnimator.SetBool("IsMoving", false);
        }

        if (!PlayerData.IsGrounded())
        {
            if (PlayerData.PlayerRB.velocity.y < 0)
            {
                PlayerData.PlayerRB.velocity += Vector2.up * Physics2D.gravity.y * (PlayerData.FallMultiplyer - 1) * Time.deltaTime;
            }
            else if (PlayerData.PlayerRB.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                PlayerData.PlayerRB.velocity += Vector2.up * Physics2D.gravity.y * (PlayerData.LowJumpMultiplyer - 1) * Time.deltaTime;
            }

            movement = new Vector3(inputX * (PlayerData.MoveSpeed * 0.7f), PlayerData.PlayerRB.velocity.y, 0);
        }
        else
        {
            movement = new Vector3(inputX * PlayerData.MoveSpeed, PlayerData.PlayerRB.velocity.y, 0);
        }
        PlayerData.PlayerRB.velocity = movement;
    }

    protected override void TransitionToJump()
    {
        if (Input.GetButtonDown("Jump") && PlayerData.OnGround)
        {
            PlayerData.PlayerRB.velocity = Vector2.up * PlayerData.JumpForce;
        }
    }
}
