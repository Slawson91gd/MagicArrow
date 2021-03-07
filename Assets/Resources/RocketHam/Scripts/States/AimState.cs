using UnityEngine;

public class AimState : State
{
    private SpriteRenderer PlayerSprite { get; set; }
    
    private Color startingColor;
    private Color aimColor;
    private Vector3 targetPos;


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
            Vector3 rangPos = PlayerData.PlayerBoomerang.transform.position;
            Vector3 direction = mousePos - rangPos;
            float distance = direction.magnitude;

            // if distance between player and mousePos is less than or equal to BoomerangDistance
            if (distance <= PlayerData.PlayerBoomerang.CurrentBoomerang.BoomerangDistance)
            {
                //PlayerData.PlayerBoomerang.CurrentBoomerang.SetTarget(mousePos);
                targetPos = mousePos;
                Debug.DrawRay(rangPos, direction, Color.red);
            }
            // otherwise
            else
            {
                // target is equal to a distance between the player and the mouse position
                //PlayerData.PlayerBoomerang.CurrentBoomerang.SetTarget(rangPos + direction.normalized * PlayerData.PlayerBoomerang.CurrentBoomerang.BoomerangDistance);
                targetPos = rangPos + direction.normalized * PlayerData.PlayerBoomerang.CurrentBoomerang.BoomerangDistance;
                // Draw a line from the players position to a specified distance between player and mouse position
                Debug.DrawRay(rangPos, (PlayerData.PlayerBoomerang.CurrentBoomerang.BoomerangTarget - rangPos).normalized *
                                        (PlayerData.PlayerBoomerang.CurrentBoomerang.BoomerangTarget - rangPos).magnitude, Color.blue);
            }

            PlayerData.PlayerBoomerang.CurrentBoomerang.SetTarget(targetPos);
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
            //PlayerData.PlayerAnimator.SetBool("IsMoving", true);
        }
        else
        {
            //PlayerData.PlayerAnimator.SetBool("IsMoving", false);
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
