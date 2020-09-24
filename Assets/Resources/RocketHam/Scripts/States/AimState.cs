using UnityEngine;

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
            Vector2 direction = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f)) - PlayerData.Player.transform.position;
            PlayerData.angle = Mathf.Clamp(Vector2.SignedAngle(Vector2.right, direction), -45.0f, 45.0f);

            //PlayerData.BoomerangTarget = 

            //---------------------------------------------------------------
            if (PlayerData.angle > -45.0f && PlayerData.angle < 45.0f)
            {
                Debug.Log("The player is aiming to the right.");
                PlayerData.BoomerangTarget = PlayerData.PlayerCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            }
            //else if(PlayerData.angle < 135.0f && PlayerData.angle > -135.0f)
            //{
            //    PlayerData.BoomerangTarget = PlayerData.PlayerCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            //}


            //PlayerData.BoomerangTarget = PlayerData.PlayerCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            Debug.DrawRay(PlayerData.Player.transform.position, (PlayerData.BoomerangTarget - PlayerData.Player.transform.position).normalized *
                                                                 (PlayerData.BoomerangTarget - PlayerData.Player.transform.position).magnitude, Color.red);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            PlayerData.SetState(PlayerData.Throw);
        }
    }

    protected override void HandleMovement(float inputX)
    {
        inputX = Input.GetAxisRaw("Horizontal");

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
            Vector2 movement = new Vector2(inputX * PlayerData.MoveSpeed, PlayerData.PlayerRB.velocity.y);
            PlayerData.PlayerRB.velocity = movement;
        }
        else
        {
            PlayerData.PlayerRB.velocity = Vector2.zero;
        }
    }

    protected override void TransitionToJump()
    {
        if (Input.GetButtonDown("Jump") && PlayerData.OnGround)
        {
            PlayerData.PlayerRB.velocity = Vector2.up * PlayerData.JumpForce;
        }
    }
}
